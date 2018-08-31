using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSONSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ModularToolManagerTest
{
    [TestClass()]
    public class SettingsTests
    {
        private static string filePath;

        private const string TEST_APP_NAME = "TestApp";
        private const string TEST_APP_NAME2 = "TestApp2";

        private const string BOOL_TEST_KEY = "boolTest";
        private const string STRING_TEST_KEY = "stringTest";
        private const string FLOAT_TEST_KEY = "floatTest";
        private const string INT_TEST_KEY = "intTest";


        private const bool BOOL_TEST_VALUE = true;
        private const string STRING_TEST_VALUE = "stringValueTest";
        private const float FLOAT_TEST_VALUE = 452.3045f;
        private const int INT_TEST_VALUE = 543862;

        private const bool BOOL_TEST_VALUE2 = true;
        private const string STRING_TEST_VALUE2 = "stringValueTest2";
        private const float FLOAT_TEST_VALUE2 = 545.3045f;
        private const int INT_TEST_VALUE2 = 16186;

        private void createSimpleTestFile()
        {
            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(TEST_APP_NAME);

            testSettings.AddOrChangeKeyValue(BOOL_TEST_KEY, BOOL_TEST_VALUE);
            testSettings.AddOrChangeKeyValue(STRING_TEST_KEY, STRING_TEST_VALUE);
            testSettings.AddOrChangeKeyValue(FLOAT_TEST_KEY, FLOAT_TEST_VALUE);
            testSettings.AddOrChangeKeyValue(INT_TEST_KEY, INT_TEST_VALUE);

            testSettings.AddNewField(TEST_APP_NAME2);

            testSettings.AddOrChangeKeyValue(TEST_APP_NAME2, BOOL_TEST_KEY, BOOL_TEST_VALUE2);
            testSettings.AddOrChangeKeyValue(TEST_APP_NAME2, STRING_TEST_KEY, STRING_TEST_VALUE2);
            testSettings.AddOrChangeKeyValue(TEST_APP_NAME2, FLOAT_TEST_KEY, FLOAT_TEST_VALUE2);
            testSettings.AddOrChangeKeyValue(TEST_APP_NAME2, INT_TEST_KEY, INT_TEST_VALUE2);

            testSettings.Save();
        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ClassInit " + context.TestName);
            filePath = context.TestDir + "\\TestFile.json";
        }

        [TestMethod()]
        public void SettingsTest()
        {
            Settings testSettings = new Settings(filePath);
            Assert.IsNotNull(testSettings);
        }

        [TestMethod()]
        public void AddNewFieldTest()
        {
            string testAppName = "TestApp";
            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);

            Assert.AreEqual(testAppName, testSettings.DefaultApp);
        }

        [TestMethod()]
        public void AddOrChangeKeyValueTestDefaultApp()
        {
            string testAppName = "TestApp";
            string testKey = "TestKey";
            string testValue = "TestValue";

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddOrChangeKeyValue(testKey, testValue);

            string returnVal = testSettings.GetValue(testKey, out SettingsType type);

            Assert.AreEqual(testValue, returnVal);
            Assert.AreEqual(SettingsType.String, type);
        }

        [TestMethod()]
        public void AddOrChangeKeyValueTestOtherApp()
        {
            string testAppName = "TestApp";
            string otherAppName = "ChangedTestAppName";
            string testKey = "TestKey";
            string testValue = "TestValue";

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddNewField(otherAppName);
            testSettings.AddOrChangeKeyValue(otherAppName, testKey, testValue);

            string returnVal = testSettings.GetValue(otherAppName, testKey, out SettingsType type);

            Assert.AreEqual(testValue, returnVal);
            Assert.AreEqual(SettingsType.String, type);
        }

        [TestMethod()]
        public void SimpleSaveLoadTestNoCompression()
        {
            string testAppName = "TestApp";
            string otherAppName = "ChangedTestAppName";

            string testKey1 = "TestKey";
            string testValue1 = "TestValue";

            string testKey2 = "TestKey";
            bool testValue2 = true;

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddNewField(otherAppName);

            testSettings.AddOrChangeKeyValue(otherAppName, testKey1, testValue1);
            testSettings.AddOrChangeKeyValue(testKey2, testValue2);

            testSettings.Save();
            testSettings = null;

            Settings testSettings2 = new Settings(filePath);

            string returnVal1 = testSettings2.GetValue(otherAppName, testKey1, out SettingsType type1);
            string returnVal2 = testSettings2.GetValue(testKey2, out SettingsType type2);

            Assert.AreEqual(testValue1, returnVal1);
            Assert.AreEqual(SettingsType.String, type1);

            Assert.AreEqual(testValue2.ToString(), returnVal2);
            Assert.AreEqual(SettingsType.Bool, type2);
        }

        [TestMethod()]
        public void SimpleSaveLoadTestCompression()
        {
            string testAppName = "TestApp";
            string otherAppName = "ChangedTestAppName";

            string testKey1 = "TestKey";
            string testValue1 = "TestValue";

            string testKey2 = "TestKey";
            bool testValue2 = true;

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddNewField(otherAppName);

            testSettings.AddOrChangeKeyValue(otherAppName, testKey1, testValue1);
            testSettings.AddOrChangeKeyValue(testKey2, testValue2);

            testSettings.Save(true);

            Settings testSettings2 = new Settings(filePath);

            string returnVal1 = testSettings2.GetValue(otherAppName, testKey1, out SettingsType type1);
            string returnVal2 = testSettings2.GetValue(testKey2, out SettingsType type2);

            Assert.AreEqual(testValue1, returnVal1);
            Assert.AreEqual(SettingsType.String, type1);

            Assert.AreEqual(testValue2.ToString(), returnVal2);
            Assert.AreEqual(SettingsType.Bool, type2);
        }

        [TestMethod()]
        public void SimpleSaveLoadTestCompression2()
        {
            string testAppName = "TestApp";
            string otherAppName = "ChangedTestAppName";

            string testKey1 = "TestKey";
            int testValue1 = 1234;

            string testKey2 = "TestKey";
            float testValue2 = 1234.4246f;

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddNewField(otherAppName);

            testSettings.AddOrChangeKeyValue(otherAppName, testKey1, testValue1);
            testSettings.AddOrChangeKeyValue(testKey2, testValue2);

            testSettings.Save(true);
            testSettings = null;

            Settings testSettings2 = new Settings(filePath);

            string returnVal1 = testSettings2.GetValue(otherAppName, testKey1, out SettingsType type1);
            string returnVal2 = testSettings2.GetValue(testKey2, out SettingsType type2);

            Assert.AreEqual(testValue1.ToString(), returnVal1);
            Assert.AreEqual(SettingsType.Int, type1);

            Assert.AreEqual(testValue2.ToString(), returnVal2);
            Assert.AreEqual(SettingsType.Float, type2);
        }

        [TestMethod()]
        public void GetBoolValueTest()
        {
            string testAppName = "TestApp";
            string testKey = "TestKey";
            bool testValue = false;

            Settings testSettings = new Settings(filePath);
            testSettings.AddNewField(testAppName);
            testSettings.AddOrChangeKeyValue(testKey, testValue);

            bool returnVal = testSettings.GetBoolValue(testKey);

            Assert.AreEqual(testValue, returnVal);
        }

        [TestMethod()]
        public void GetBoolValueTest2()
        {
            createSimpleTestFile();
            Settings testSettings = new Settings(filePath);
            bool returnVal = testSettings.GetBoolValue(TEST_APP_NAME2, BOOL_TEST_KEY);

            Assert.AreEqual(BOOL_TEST_VALUE2, returnVal);
        }

        [TestMethod()]
        public void GetIntValueTest()
        {
            createSimpleTestFile();
            Settings testSettings = new Settings(filePath);
            int returnVal = testSettings.GetIntValue(INT_TEST_KEY);

            Assert.AreEqual(INT_TEST_VALUE, returnVal);
        }

        [TestMethod()]
        public void GetIntValueTest2()
        {
            createSimpleTestFile();
            Settings testSettings = new Settings(filePath);
            int returnVal = testSettings.GetIntValue(TEST_APP_NAME2, INT_TEST_KEY);

            Assert.AreEqual(INT_TEST_VALUE2, returnVal);
        }

        [TestMethod()]
        public void GetFloatValueTest()
        {
            createSimpleTestFile();
            Settings testSettings = new Settings(filePath);
            float returnVal = testSettings.GetFloatValue(FLOAT_TEST_KEY);

            Assert.AreEqual(FLOAT_TEST_VALUE, returnVal, 0.001);
        }

        [TestMethod()]
        public void GetFloatValueTest1()
        {
            createSimpleTestFile();
            Settings testSettings = new Settings(filePath);
            float returnVal = testSettings.GetFloatValue(TEST_APP_NAME2, FLOAT_TEST_KEY);

            Assert.AreEqual(FLOAT_TEST_VALUE2, returnVal, 0.001);
        }

        [TestMethod()]
        public void CleanupTest()
        {
            createSimpleTestFile();
            Settings beforeCleanupTest = new Settings(filePath);
            beforeCleanupTest.Cleanup();

            Settings afterCleanupTest = new Settings(filePath);

            bool boolTest = afterCleanupTest.GetBoolValue(BOOL_TEST_KEY);
            string stringTest = afterCleanupTest.GetValue(STRING_TEST_KEY);
            float floatTest = afterCleanupTest.GetFloatValue(FLOAT_TEST_KEY);
            int intTest = afterCleanupTest.GetIntValue(INT_TEST_KEY);

            Assert.AreEqual(BOOL_TEST_VALUE, boolTest);
            Assert.AreEqual(STRING_TEST_VALUE, stringTest);
            Assert.AreEqual(FLOAT_TEST_VALUE, floatTest);
            Assert.AreEqual(INT_TEST_VALUE, intTest);
        }

        [TestMethod()]
        public void CleanupTest2()
        {
            createSimpleTestFile();
            Settings beforeCleanupTest = new Settings(filePath);
            beforeCleanupTest.Cleanup();

            Settings afterCleanupTest = new Settings(filePath);

            bool boolTest = afterCleanupTest.GetBoolValue(TEST_APP_NAME2, BOOL_TEST_KEY);
            string stringTest = afterCleanupTest.GetValue(TEST_APP_NAME2, STRING_TEST_KEY);
            float floatTest = afterCleanupTest.GetFloatValue(TEST_APP_NAME2, FLOAT_TEST_KEY);
            int intTest = afterCleanupTest.GetIntValue(TEST_APP_NAME2, INT_TEST_KEY);

            Assert.AreEqual(BOOL_TEST_VALUE2, boolTest);
            Assert.AreEqual(STRING_TEST_VALUE2, stringTest);
            Assert.AreEqual(FLOAT_TEST_VALUE2, floatTest);
            Assert.AreEqual(INT_TEST_VALUE2, intTest);
        }

        [TestMethod()]
        public void ClearTest()
        {
            createSimpleTestFile();
            Settings beforeClearTest = new Settings(filePath);
            beforeClearTest.Clear();

            Settings afterClearTest = new Settings(filePath);
            Assert.IsNull(afterClearTest.DefaultApp);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}