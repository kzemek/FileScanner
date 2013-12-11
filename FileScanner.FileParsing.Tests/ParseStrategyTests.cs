using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing.Tests
{
    [TestClass]
    public class ParseStrategyTests
    {
        private string[] testStrings = { "ĘÓĄŚŁŻŹĆŃ", "ęóąśłżźćń", "żÓłĆ" };
        private string[] lowercaseResultStrings = { "ęóąśłżźćń", "ęóąśłżźćń", "żółć" };
        private string[] asciiResultStrings = { "EOASLZZCN", "eoaslzzcn", "zOlC" };

        [TestMethod]
        public void ParseStrategy_LeaveUnchanged()
        {
            IParseStrategy parseStrategy = ParseStrategy.LeaveUnchanged();
            for (int i = 0; i < testStrings.Length; i++)
            {
                Assert.IsTrue(parseStrategy.Parse(testStrings[i]) == testStrings[i]);
            }
        }
        [TestMethod]
        public void ParseStrategy_ReplaceNonASCII()
        {
            IParseStrategy parseStrategy = ParseStrategy.ReplaceNonASCII();
            for (int i = 0; i < testStrings.Length; i++)
            {
                Assert.IsTrue(parseStrategy.Parse(testStrings[i]) == asciiResultStrings[i]);
            }
        }
        [TestMethod]
        public void ParseStrategy_ReplaceCapitalLetters()
        {
            IParseStrategy parseStrategy = ParseStrategy.ReplaceCapitalLetters();
            for (int i = 0; i < testStrings.Length; i++)
            {
                Assert.IsTrue(parseStrategy.Parse(testStrings[i]) == lowercaseResultStrings[i]);
            }
        }
    }
}
