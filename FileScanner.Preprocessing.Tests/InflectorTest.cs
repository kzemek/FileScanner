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
    public class InflectorTest
    {
        private static List<string> getVariationsInstance = new List<string> {
            "łódź", "gżegżółka", "mąka", "ćma", "ręka", "koń", "śmieć", "źdźbło"
        };
        private static List<string> getVariationsExpected = new List<List<string>> {
            "lodz", "gzegzolka", "maka", "cma", "reka", "kon", "smiec", "zdzblo"
        };
        private static List<string> getVariationsInstance = new List<string> {
            "maka", "kon", "oko", "monitor", "pies", "mysz", "zeglarz", "drzewo", "czlowiek"
        };
        private static List<string> getVariationsExpected = new List<HashSet<string>> {
            {"maka", "maki", "mace"}, //bez wołacza, bez liczby mnogiej
            {"kon", "konia", "koniowi", "koniem", "koniu"},
            {"oko", "oka", "oku", "okiem"},
            {"monitor", "monitora", "monitorowi", "monitorem", "monitorze"},
            {"pies", "psa", "psu", "psem", "psie"},
            {"mysz", "myszy", "mysza"},
            {"zeglarz", "zeglarza", "zeglarzowi", "zeglarzem", "zeglarzu"},
            {"drzewo", "drzewa", "drzewu", "drzewem", "drzewie"},
            {"czlowiek", "czlowieka", "czlowiekowi", "czlowieka", "czlowiekiem", "czlowieku"},
            {"kot", "kota", "kotu", "kota", "kotem", "kocie"},
            {"ziemia", "ziemi", "ziemi", "ziemie", "ziemia", "ziemi"},
            {"pogoda", "pogody", "pogodzie", "pogode", "pogoda", "pogodzie"}
            {"droga", "drogi", "drodze", "droge", "droga", "drodze"}
        };

        private Inflector inflector;

        [TestInitialize]
        public void TestInitialize()
        {
            inflector = new Inflector();
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

        [TestMethod]
        public void TestGetVariations()
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
    }
}

