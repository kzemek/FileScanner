using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScanner.Preprocessing.Tests
{
    /// <summary>
    /// Tests for class Normalizer
    /// </summary>
    [TestClass]
    public class NormalizerTest
    {
        private static List<string> removePolishCharactersInstance = new List<string> {
            "łódź", "gżegżółka", "mąka", "ćma", "ręka", "koń", "śmieć", "źdźbło"
        };
        private static List<string> removePolishCharactersExpected = new List<string> {
            "lodz", "gzegzolka", "maka", "cma", "reka", "kon", "smiec", "zdzblo"
        };
        private static List<string> getBasicFormInstance = new List<string> {
            "statkami", "piotrkowi", "programisci", "kacu", "piotrkiem", "marii", "strzalem", "rykiem",
            "markiem i programistami"
        };
        private static List<string> getBasicFormExpected = new List<string> {
            "statek", "piotrek", "programista", "kac", "piotrek", "maria", "strzal", "ryk",
            "marek i programist"
        };

        private Normalizer _normalizer;

        [TestInitialize]
        public void TestInitialize()
        {
            _normalizer = new Normalizer();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestRemovePolishCharacters()
        {
            var inputWords = removePolishCharactersInstance;
            var expectedWords = removePolishCharactersExpected;
            var outputWords = new List<string>();
            foreach (string inputWord in inputWords)
            {
                string preprocessedWord = _normalizer.RemovePolishCharacters(inputWord);
                outputWords.Add(preprocessedWord);
            }

            Assert.IsTrue(Enumerable.SequenceEqual(expectedWords, outputWords));
        }

        [TestMethod]
        public void TestGetBasicForm()
        {
            var outputWords = new List<string>();
            foreach (string inputWord in getBasicFormInstance)
            {
                string basicForm = _normalizer.GetBasicForm(inputWord);
                outputWords.Add(basicForm);
            }
            foreach (string word in outputWords)
                Console.WriteLine(word);
            Assert.IsTrue(Enumerable.SequenceEqual(getBasicFormExpected, outputWords));
        }
    }
}
