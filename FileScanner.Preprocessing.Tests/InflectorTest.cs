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
            //"maka", "kon", "oko", "monitor", "pies", "mysz", "zeglarz", "drzewo", "czlowiek"
          "sadza"
        };
        private static List<HashSet<string>> getVariationsExpected = new List<HashSet<string>> {
            //new HashSet<string>(){"maka", "maki", "mace"}, //bez wołacza, bez liczby mnogiej
            //new HashSet<string>(){"kon", "konia", "koniowi", "koniem", "koniu"},
            //new HashSet<string>(){"oko", "oka", "oku", "okiem"},
            //new HashSet<string>(){"monitor", "monitora", "monitorowi", "monitorem", "monitorze"},
            //new HashSet<string>(){"pies", "psa", "psu", "psem", "psie"},
            //new HashSet<string>(){"mysz", "myszy", "mysza"},
            //new HashSet<string>(){"zeglarz", "zeglarza", "zeglarzowi", "zeglarzem", "zeglarzu"},
            //new HashSet<string>(){"drzewo", "drzewa", "drzewu", "drzewem", "drzewie"},
            //new HashSet<string>(){"czlowiek", "czlowieka", "czlowiekowi", "czlowieka", "czlowiekiem", "czlowieku"},
            //new HashSet<string>(){"kot", "kota", "kotu", "kota", "kotem", "kocie"},
            //new HashSet<string>(){"ziemia", "ziemi", "ziemi", "ziemie", "ziemia", "ziemi"},
            //new HashSet<string>(){"pogoda", "pogody", "pogodzie", "pogode", "pogoda", "pogodzie"},
            //new HashSet<string>(){"droga", "drogi", "drodze", "droge", "droga", "drodze"},
            //new HashSet<string>(){"stajnia", "stajni", "stajni", "stajnie", "stajni"},//r. żeński, grupa I
            new HashSet<string>(){"sadza", "sadzy", "sadzy", "sadzę", "sadzą", "sadzy", "sadzo"}, //r. żeński, grupa II
            //new HashSet<string>(){"kurz", "kurzu", "kurzowi", "kurz", "kurzem", "kurzu", "kurzu"}
            //new HashSet<string>(){"krol","krola","krolowi","krola","krolem","krolu","krolu"}
            //new HashSet<string>(){"grosz","grosza","groszowi","grosz","groszem","groszu","groszu"}
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
            var inputWords = getVariationsInstance;
            var expectedWords = getVariationsExpected;
            var outputWords = new List<string>();
            for (int i=0; i<inputWords.Count;++i)
            {
                var inputWord = inputWords[i];
                HashSet<string> variations = (HashSet<string>) inflector.GetVariations(inputWord);
                Console.Write(string.Join(" ",variations));
                Assert.IsTrue(variations.SetEquals(getVariationsExpected[i]));
            }

        }
    }
}

