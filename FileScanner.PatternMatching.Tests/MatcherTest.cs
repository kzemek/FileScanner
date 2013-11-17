using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileScanner.PatternMatching.Tests
{
    [TestClass]
    public class MatcherTest
    {
        private Stream StringToStream(String text)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return new MemoryStream(encoding.GetBytes(text));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "No patterns were given.")]
        public void ParamsConstructor_GivenNoPatterns_ThrowsArgumentException()
        {
            var m = new Matcher();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "No patterns were given.")]
        public void ListConstructor_GivenNoPatterns_ThrowsArgumentException()
        {
            var m = new Matcher(new List<String>());
        }

        [TestMethod]
        public void IsMatch_GivenOnePattern_AndTextWithNoMatches_ReturnsFalse()
        {
            var m = new Matcher("p1");
            var testString = "1234abcd1p";

            Assert.IsFalse(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsFalse(m.IsMatch(stream));
        }

        [TestMethod]
        public void IsMatch_GivenMultiplePatterns_AndTextWithNoMatches_ReturnsFalse()
        {
            var m = new Matcher("p1", "p2", "p3", "p4");
            var testString = "1234abcd1pts[9ertk54";

            Assert.IsFalse(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsFalse(m.IsMatch(stream));
        }

        [TestMethod]
        public void IsMatch_GivenOnePattern_AndTextWithSingleMatch_ReturnsTrue()
        {
            var m = new Matcher("p1");
            var testString = "1234abp1cd1p";

            Assert.IsTrue(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(m.IsMatch(stream));
        }

        [TestMethod]
        public void IsMatch_GivenMultiplePatterns_AndTextWithSingleMatch_ReturnsTrue()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "1234567p2890";

            Assert.IsTrue(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(m.IsMatch(stream));
        }

        [TestMethod]
        public void IsMatch_GivenOnePattern_AndTextWithMultipleMatches_ReturnsTrue()
        {
            var m = new Matcher("p1");
            var testString = "12p134567p189p10";

            Assert.IsTrue(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(m.IsMatch(stream));
        }

        [TestMethod]
        public void IsMatch_GivenMultiplePatterns_AndTextWithMultipleMatches_ReturnsTrue()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "12p334567p289p20";

            Assert.IsTrue(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(m.IsMatch(stream));
        }

        [TestMethod]
        public void Match_GivenOnePattern_AndTextWithNoMatches_ReturnsNull()
        {
            var m = new Matcher("p1");
            var testString = "1234567p2890";

            Assert.IsNull(m.Match(testString));
            using (var stream = StringToStream(testString))
                Assert.IsNull(m.Match(stream));
        }

        [TestMethod]
        public void Match_GivenMultiplePatterns_AndTextWithNoMatches_ReturnsNull()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "1234567pe890";

            Assert.IsNull(m.Match(testString));
            using (var stream = StringToStream(testString))
                Assert.IsNull(m.Match(stream));
        }

        [TestMethod]
        public void Match_GivenOnePattern_AndTextWithSingleMatch_ReturnsTheMatch()
        {
            var m = new Matcher("p1");
            var testString = "0123456p1890";
            var expectedMatch = new Match(7, "p1");

            Assert.AreEqual(m.Match(testString), expectedMatch);
            using (var stream = StringToStream(testString))
                Assert.AreEqual(m.Match(stream), expectedMatch);
        }

        [TestMethod]
        public void Match_GivenMultiplePatterns_AndTextWithSingleMatch_ReturnsTheMatch()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "012345678p290";
            var expectedMatch = new Match(9, "p2");

            Assert.AreEqual(m.Match(testString), expectedMatch);
            using (var stream = StringToStream(testString))
                Assert.AreEqual(m.Match(stream), expectedMatch);
        }

        [TestMethod]
        public void Match_GivenOnePattern_AndTextWithMultipleMatches_ReturnsTheFirstMatch()
        {
            var m = new Matcher("p1");
            var testString = "0p1345p18p190";
            var expectedMatch = new Match(1, "p1");
            
            Assert.AreEqual(m.Match(testString), expectedMatch);
            using (var stream = StringToStream(testString))
                Assert.AreEqual(m.Match(stream), expectedMatch);
        }

        [TestMethod]
        public void Match_GivenMultiplePatterns_AndTextWithMultipleMatches_ReturnsTheFirstMatch()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "0p3345p28p190";
            var expectedMatch = new Match(1, "p3");

            Assert.AreEqual(m.Match(testString), expectedMatch);
            using (var stream = StringToStream(testString))
                Assert.AreEqual(m.Match(stream), expectedMatch);
        }

        [TestMethod]
        public void Matches_GivenOnePattern_AndTextWithNoMatches_ReturnsEmptyList()
        {
            var m = new Matcher("p1");
            var testString = "0p3345p28p290";
            var expectedMatches = new List<Match>();

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void Matches_GivenMultiplePatterns_AndTextWithNoMatches_ReturnsEmptyList()
        {
            var m = new Matcher("p1", "p2", "p3");
            var testString = "0pa3345pd28pz290";
            var expectedMatches = new List<Match>();

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void Matches_GivenOnePattern_AndTextWithSingleMatch_ReturnsListWithTheMatch()
        {
            var m = new Matcher("p1");
            var testString = "0p3345p18p290";
            var expectedMatches = new List<Match>();
            expectedMatches.Add(new Match(6, "p1"));

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void Matches_GivenMultiplePatterns_AndTextWithSingleMatch_ReturnsListWithTheMatch()
        {
            var m = new Matcher("p1", "p2", "p3", "p4");
            var testString = "0pw345p48ps290";
            var expectedMatches = new List<Match>();
            expectedMatches.Add(new Match(6, "p4"));

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void Matches_GivenOnePattern_AndTextWithMultipleMatches_ReturnsListOfAllMatches()
        {
            var m = new Matcher("p1");
            var testString = "0p1345p18p290";
            var expectedMatches = new List<Match>();
            expectedMatches.Add(new Match(1, "p1"));
            expectedMatches.Add(new Match(6, "p1"));
            
            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void Matches_GivenMultiplePatterns_AndTextWithMultipleMatches_ReturnsListOfAllMatches()
        {
            var m = new Matcher("p1", "p2", "p3", "p4");
            var testString = "0p3345p48p290";
            var expectedMatches = new List<Match>();
            expectedMatches.Add(new Match(1, "p3"));
            expectedMatches.Add(new Match(6, "p4"));
            expectedMatches.Add(new Match(9, "p2"));

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [TestMethod]
        public void IsMatch_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsTrue()
        {
            var m = new Matcher("p1");

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                Assert.IsTrue(m.IsMatch(stream));
            }
        }

        [TestMethod]
        public void Match_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                Assert.AreEqual(m.Match(stream), new Match(4, "p1"));
            }
        }

        [TestMethod]
        public void Matches_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsListWithTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                
                var expectedMatches = new List<Match> { new Match(4, "p1"), new Match(13, "p1"), new Match(17, "p1") };
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
            }
        }

        [TestMethod]
        public void IsMatch_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTrue()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            Assert.IsTrue(m.IsMatch(text));
        }

        [TestMethod]
        public void Match_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            Assert.AreEqual(m.Match(text), new Match(4, "p1"));
        }

        [TestMethod]
        public void Matches_GivenMultipleLinesSring_AndTextWithAMatch_ReturnsListWithTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            var expectedMatches = new List<Match> { new Match(4, "p1"), new Match(13, "p1"), new Match(17, "p1") };
            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(text), expectedMatches));
        }
    }
}
