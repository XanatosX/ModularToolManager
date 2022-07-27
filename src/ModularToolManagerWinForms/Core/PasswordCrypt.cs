using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManger.Core
{
    public class PasswordManager
    {
        private int _keysize;
        private int _blocksize;
        private int _derivationIterations;

        public PasswordManager()
        {
            _keysize = 256;
            _blocksize = 256;
            _derivationIterations = 1000;
        }

        private SecureString createSecureString(ref string baseString)
        {
            SecureString returnString = new SecureString();
            foreach (char curChar in baseString)
            {
                returnString.AppendChar(curChar);
            }
            baseString = String.Empty;
            return returnString;
        }

        public SecureString CreateSecureString(ref string StringToSecure)
        {
            return createSecureString(ref StringToSecure);
        }

        public string EncryptPassword(string Data, string passPhrase)
        {
            byte[] saltStringBytes = Generate256BitsOfRandomEntropy();
            byte[] ivStringBytes = Generate256BitsOfRandomEntropy();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Data);

            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, _derivationIterations))
            {
                byte[] keyBytes = password.GetBytes(_keysize / 8);
                using (RijndaelManaged symetricKey = new RijndaelManaged())
                {
                    symetricKey.BlockSize = _blocksize;
                    symetricKey.Mode = CipherMode.CBC;
                    symetricKey.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform encyptor = symetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encyptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                byte[] cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }

                }
            }
        }

        public string DecryptPassword(string cryptedText, string Password)
        {
            byte[] cipherTextBytesWithSaltAndIV = Convert.FromBase64String(cryptedText);
            byte[] saltStringBytes = cipherTextBytesWithSaltAndIV.Take(_keysize / 8).ToArray();
            byte[] ivStringBytes = cipherTextBytesWithSaltAndIV.Skip(_keysize / 8).Take(_keysize / 8).ToArray();
            byte[] cipherTextBytes = cipherTextBytesWithSaltAndIV.Skip((_keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIV.Length - ((_keysize / 8) * 2)).ToArray();

            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(Password, saltStringBytes, _derivationIterations))
            {
                byte[] keyBytes = password.GetBytes(_keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = _keysize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rngCps = new RNGCryptoServiceProvider())
            {
                rngCps.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
