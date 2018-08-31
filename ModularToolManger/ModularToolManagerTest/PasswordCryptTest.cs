using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModularToolManger.Core;

namespace ModularToolManagerTest
{
    [TestClass]
    public class PasswordCryptTest
    {
        private static PasswordManager manager;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ClassInit " + context.TestName);
            manager = new PasswordManager();
        }

        [TestMethod()]
        public void PasswordCryptTestSetupTest()
        {
            Assert.AreEqual(typeof(PasswordManager), manager.GetType());
        }

        [TestMethod()]
        public void EncryptDecryptPasswordTest()
        {
            string password = "SomeStrongPassword";
            string initialData = "Some text to encrypt";
            string cryptData = manager.EncryptPassword(initialData, password);
            string afterConvert = manager.DecryptPassword(cryptData, password);

            Assert.AreEqual(initialData, afterConvert);
        }
    }
}
