using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class PositionAwareStreamReaderTest
    {
        private string TestString;

        [TestInitialize]
        public void Setup()
        {
            TestString = "zażółć gęślą jaźń";
        }

        void Read_Fragment_Position(Encoding encoding)
        {
            MemoryStream stream = new MemoryStream(encoding.GetBytes(TestString));
            PositionAwareStreamReader reader = new PositionAwareStreamReader(stream, encoding);
            int charsToRead = TestString.Length / 2;
            char[] buffer = new char[charsToRead];

            Assert.AreEqual(charsToRead, reader.Read(buffer, 0, charsToRead));
            Assert.AreEqual(TestString.Substring(0, charsToRead), new string(buffer));
            Assert.AreEqual(charsToRead, reader.Position);
        }

        void Read_All_Position(Encoding encoding)
        {
            MemoryStream stream = new MemoryStream(encoding.GetBytes(TestString));
            PositionAwareStreamReader reader = new PositionAwareStreamReader(stream, encoding);
            char[] buffer = new char[TestString.Length];

            Assert.AreEqual(TestString.Length, reader.Read(buffer, 0, buffer.Length));
            Assert.AreEqual(TestString, new string(buffer));
            Assert.AreEqual(TestString.Length, reader.Position);
        }

        [TestMethod]
        public void Position_ReadFragment_Encoding_UTF8()
        {
            Read_Fragment_Position(Encoding.UTF8);
        }

        [TestMethod]
        public void Position_ReadFragment_Encoding_Default()
        {
            Read_Fragment_Position(Encoding.Default);
        }

        [TestMethod]
        public void Position_ReadAll_Encoding_UTF8()
        {
            Read_All_Position(Encoding.UTF8);
        }

        [TestMethod]
        public void Position_ReadAll_Encoding_Default()
        {
            Read_All_Position(Encoding.Default);
        }
    }
}
