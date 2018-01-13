using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManger.Core
{
    public class PasswordHasher
    {
        public PasswordHasher()
        {

        }

        /// <summary>
        /// Creates a hashed password out of a string
        /// </summary>
        /// <param name="password">The string password</param>
        /// <param name="salt">If wanted a fixed salt</param>
        /// <returns>Returns a Rfc2898 + base64 hashed password</returns>
        public string GetHashedPassword(string password, byte[] salt = null)
        {
            string hasedPassword = Convert.ToBase64String(CreatePassword(password));
            return hasedPassword;
        }

        private byte[] CreatePassword(string password, byte[] salt = null)
        {

            if (salt == null)
            {
                RNGCryptoServiceProvider CryptoService = new RNGCryptoServiceProvider();
                CryptoService.GetBytes(salt = new byte[16]);
            }

            Rfc2898DeriveBytes pbkdfs = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdfs.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, 20);


            return hashBytes;
        }

        /// <summary>
        /// Checks if a password is valid
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <param name="savedBase64Password">The already stored password</param>
        /// <returns>The result of the checking</returns>
        public bool CheckPassword(string password, string savedBase64Password)
        {
            byte[] hashBytes = Convert.FromBase64String(savedBase64Password);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);


            byte[] checkPasssword = CreatePassword(password, salt);
            for (int i = 0; i < hashBytes.Length - salt.Length; i++)
            {
                if (hashBytes[i + salt.Length] != checkPasssword[i + salt.Length])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
