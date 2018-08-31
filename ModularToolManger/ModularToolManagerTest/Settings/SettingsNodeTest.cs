using System;
using JSONSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModularToolManagerTest.Settings
{
    [TestClass]
    public class SettingsNodeTest
    {
        private static SettingsNode settingsNode;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ClassInit " + context.TestName);
            settingsNode = new SettingsNode("Test");
        }

        [TestMethod]
        public void SimpleSetupTest()
        {
            string name = "GetTested!";
            SettingsNode testNode = new SettingsNode(name);
            Assert.AreEqual(name, testNode.Name);
            Assert.IsNotNull(testNode.Settings);
        }

        [TestMethod]
        public void SimpleKeyAddString()
        {
            string nodeName = "Test";
            string nodeValue = "This is a test value";


            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            Assert.AreEqual(nodeValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.String, typeOfValue);
        }

        [TestMethod]
        public void SimpleKeyAddInt()
        {
            string nodeName = "Test";
            int nodeValue = 123;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            Assert.AreEqual(nodeValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Int, typeOfValue);
        }

        [TestMethod]
        public void SimpleKeyAddDecimal()
        {
            string nodeName = "Test";
            float nodeValue = 1.57f;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            Assert.AreEqual(nodeValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Float, typeOfValue);
        }

        [TestMethod]
        public void SimpleKeyAddBool()
        {
            string nodeName = "Test";
            bool nodeValue = true;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            Assert.AreEqual(nodeValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Bool, typeOfValue);
        }

        [TestMethod]
        public void TryUpdateString()
        {
            string nodeName = "Test";
            string nodeValue = "This is a test value";
            string updatedValue = "Well i'm updated now!";

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            settingsNode.AddOrChangeKeyValue(nodeName, updatedValue);
            Assert.AreEqual(updatedValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.String, typeOfValue);
        }

        [TestMethod]
        public void TryUpdateInt()
        {
            string nodeName = "Test";
            int nodeValue = 123;
            int updatedValue = 56181;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            settingsNode.AddOrChangeKeyValue(nodeName, updatedValue);
            Assert.AreEqual(updatedValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Int, typeOfValue);
        }

        [TestMethod]
        public void TryUpdateDecimal()
        {
            string nodeName = "Test";
            float nodeValue = 1.47539284f;
            float updatedValue = 5.235748f;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            settingsNode.AddOrChangeKeyValue(nodeName, updatedValue);
            Assert.AreEqual(updatedValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Float, typeOfValue);
        }

        [TestMethod]
        public void TryUpdatebool()
        {
            string nodeName = "Test";
            bool nodeValue = false;
            bool updatedValue = true;

            settingsNode.AddOrChangeKeyValue(nodeName, nodeValue);
            settingsNode.AddOrChangeKeyValue(nodeName, updatedValue);
            Assert.AreEqual(updatedValue.ToString(), settingsNode.GetKeyValue(nodeName, out SettingsType typeOfValue));
            Assert.AreEqual(SettingsType.Bool, typeOfValue);
        }

    }
}
