using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileScanner.FileParsing;
using System.Text;

namespace FileScanner.FileParsing.Tests
{
    [TestClass]
    public class FileParserTests
    {
        private string[] testStrings = { "ĘÓĄŚŁŻŹĆŃ", "ęóąśłżźćń", "żÓłĆ" };
        private string[] defaultResultStrings = { "ĘÓĄŚŁŻŹĆŃ", "ęóąśłżźćń", "żÓłĆ" };
        private string[] lowercaseResultStrings = { "ęóąśłżźćń", "ęóąśłżźćń", "żółć" };
        private string[] asciiResultStrings = { "EOASLZZCN", "eoaslzzcn", "zOlC" };
        [TestMethod]
        public void ParseFile_GivenFilePath_UTF8Encoding_AndDefaultParseMode()
        {
            string filePath = "test.txt";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            foreach (string s in testStrings)
                sw.WriteLine(s);
            sw.Close();

            StreamReader sr = FileParser.ParseFile(filePath, Encoding.UTF8);

            int i = 0;
            while(!sr.EndOfStream)
            {
                Assert.IsTrue(sr.ReadLine() == defaultResultStrings[i]);
                i++;
            }

            sr.Close();
        }
        [TestMethod]
        public void ParseFile_GivenFilePath_UTF8Encoding_AndReplaceNonASCIIParseMode()
        {
            string filePath = "test.txt";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            foreach (string s in testStrings)
                sw.WriteLine(s);
            sw.Close();

            StreamReader sr = FileParser.ParseFile(filePath, ParseMode.ReplaceNonASCII(), Encoding.UTF8);

            int i = 0;
            while (!sr.EndOfStream)
            {
                Assert.IsTrue(sr.ReadLine() == asciiResultStrings[i]);
                i++;
            }

            sr.Close();
        }
        [TestMethod]
        public void ParseFile_GivenFilePath_UTF8Encoding_AndReplaceCapitalParseMode()
        {
            string filePath = "test.txt";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            foreach (string s in testStrings)
                sw.WriteLine(s);
            sw.Close();

            StreamReader sr = FileParser.ParseFile(filePath, ParseMode.ReplaceCapitalLetters(), Encoding.UTF8);

            int i = 0;
            while (!sr.EndOfStream)
            {
                Assert.IsTrue(sr.ReadLine() == lowercaseResultStrings[i]);
                i++;
            }

            sr.Close();
        }
    }
}
