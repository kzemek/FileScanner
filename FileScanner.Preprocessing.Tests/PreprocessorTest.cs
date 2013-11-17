using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScanner.Preprocessing.Tests
{
    [TestClass]
    public class PreprocessorTest
    {
        [TestMethod]
        public void GetNormalizedPhraseTest()
        {
            var preprocessor = new Preprocessor();
            var inputWords = new List<string> { "łódź", "gżegżółka", "mąka", "ćma", "ręka", "koń", "śmieć", "źdźbło" };
            var expectedWords = new List<string> { "lodz", "gzegzolka", "maka", "cma", "reka", "kon", "smiec", "zdzblo" };
            var outputWords = new List<string>();
            foreach (string inputWord in inputWords)
            {
                string preprocessedWord = preprocessor.GetNormalizedPhrase(inputWord);
                outputWords.Add(preprocessedWord);
            }
            Assert.IsTrue(Enumerable.SequenceEqual(expectedWords, outputWords));
        }
    }
}
