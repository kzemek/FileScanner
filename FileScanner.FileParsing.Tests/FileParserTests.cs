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
        private string htmlTest =
@"<HTML>
<HEAD>
<TITLE>Your Title Here</TITLE>
</HEAD>
<BODY>
<script>asdasdasd</script>
<H1>This is a Header</H1>
<H2>This is a Medium Header</H2>
<P>This is a new paragraph!
<P><B>This is a new paragraph!</B>
<BR><B><I>This is a new sentence without a paragraph break, in bold italics.</I></B>
<HR>
</BODY>
</HTML>";

        private string htmlTestResult =
@"

This is a Header
This is a Medium Header
This is a new paragraph!
This is a new paragraph!
This is a new sentence without a paragraph break, in bold italics.

";
        [TestMethod]
        public void ParseFile_GivenFilePath_UTF8Encoding_AndLeaveUnchangedParseMode()
        {
            string filePath = "test.txt";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            foreach (string s in testStrings)
                sw.WriteLine(s);
            sw.Close();

            FileParserBuilder fileParserBuilder = new FileParserBuilder(filePath, Encoding.UTF8);
            IFileParser fileParser = fileParserBuilder.Create();
            StreamReader sr = fileParser.ParseFile();

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

            FileParserBuilder fileParserBuilder = new FileParserBuilder(filePath, Encoding.UTF8, ParseStrategy.ReplaceNonASCII());
            IFileParser fileParser = fileParserBuilder.Create();
            StreamReader sr = fileParser.ParseFile();

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

            FileParserBuilder fileParserBuilder = new FileParserBuilder(filePath, Encoding.UTF8, ParseStrategy.ReplaceCapitalLetters());
            IFileParser fileParser = fileParserBuilder.Create();
            StreamReader sr = fileParser.ParseFile();

            int i = 0;
            while (!sr.EndOfStream)
            {
                Assert.IsTrue(sr.ReadLine() == lowercaseResultStrings[i]);
                i++;
            }

            sr.Close();
        }
        [TestMethod]
        public void ParseFile_GivenHtmlFilePath_UTF8Encoding_AndLeaveUnchangedParseStrategy()
        {
            string filePath = "test.html";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            sw.Write(htmlTest);
            sw.Close();

            FileParserBuilder fileParserBuilder = new FileParserBuilder(filePath, Encoding.UTF8);
            IFileParser fileParser = fileParserBuilder.Create();
            StreamReader sr = fileParser.ParseFile();

            Assert.IsTrue(sr.ReadToEnd() == htmlTestResult);

            sr.Close();
        }
        [TestMethod]
        public void ParseFile_GivenHtmFilePath_UTF8Encoding_AndLeaveUnchangedParseStrategy()
        {
            string filePath = "test.htm";
            StreamWriter sw = new StreamWriter(File.Create(filePath));
            sw.Write(htmlTest);
            sw.Close();

            FileParserBuilder fileParserBuilder = new FileParserBuilder(filePath, Encoding.UTF8);
            IFileParser fileParser = fileParserBuilder.Create();
            StreamReader sr = fileParser.ParseFile();

            Assert.IsTrue(sr.ReadToEnd() == htmlTestResult);

            sr.Close();
        }
    }
}
