using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModularToolManger.Core;

namespace ModularToolManagerTest
{
    [TestClass]
    public class PasswordHasherTest
    {
        private static PasswordHasher hasher;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ClassInit " + context.TestName);
            hasher = new PasswordHasher();
        }

        [TestMethod()]
        public void PasswordHasherSetupTest()
        {
            Assert.AreEqual(typeof(PasswordHasher), hasher.GetType());
        }

        [TestMethod()]
        public void GetHashedPasswordNoSaltTest()
        {
            string myPassword = "SuperSafePassword";
            string hashedPassword = hasher.GetHashedPassword(myPassword);
            Assert.IsTrue(hasher.CheckPassword(myPassword, hashedPassword));
        }

        [TestMethod()]
        public void GetHashedPasswordSaltTest()
        {
            string myPassword = "SuperSafePassword";
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            string myHash = new string(Enumerable.Repeat(allowedChars, 16).Select(s => s[rnd.Next(s.Length)]).ToArray());

            string hashedPassword = hasher.GetHashedPassword(myPassword, Encoding.UTF8.GetBytes(myHash));
            Assert.IsTrue(hasher.CheckPassword(myPassword, hashedPassword));
        }


    }
}
