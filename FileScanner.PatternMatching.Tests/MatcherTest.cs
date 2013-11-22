using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FileScanner.PatternMatching.Tests
{
    [TestFixture]
    public class MatcherTest
    {
        private Stream StringToStream(String text)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return new MemoryStream(encoding.GetBytes(text));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "No patterns were given.")]
        public void ParamsConstructor_GivenNoPatterns_ThrowsArgumentException()
        {
            var m = new Matcher();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "No patterns were given.")]
        public void ListConstructor_GivenNoPatterns_ThrowsArgumentException()
        {
            var m = new Matcher(new List<String>());
        }

        static object[] TextWithNoMatches_TestCaseFactory = 
        {   //           { Matcher,                             Input text }
            new object[] { new Matcher("p1"),                   "1234abcd1p" },
            new object[] { new Matcher("p1", "p2"),             "1234pabcd1p"},
            new object[] { new Matcher("p1", "ab", "b3", "d4"), "1234papbcd1p"}
        };
        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void IsMatch_GivenTextWithNoMatches_ReturnsFalse(Matcher m, String testString)
        {
            Assert.IsFalse(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsFalse(m.IsMatch(stream));
        }

        static object[] TextWithMatches_TestCaseFactory = 
        {   //           { Matcher,                             Input text,          Expected result }
            // Single match test cases:
            new object[] { new Matcher("p1"),                   "1234abp1cd1p",      new Match(6, "p1") },                
            new object[] { new Matcher("p1", "p2"),             "1p234pabcd1p",      new Match(1, "p2")},
            new object[] { new Matcher("p1", "p2"),             "1234pabcdp1",       new Match(9, "p1")},
            new object[] { new Matcher("p1", "p2"),             "p234pabcd1p",       new Match(0, "p2")},
            new object[] { new Matcher("p1", "46", "b3", "p2"), "1234567p2890",      new Match(7, "p2")},
            // Multiple matches test cases:
            new object[] { new Matcher("p1"),                   "12p134567p189p10" , new Match(2, "p1")},
            new object[] { new Matcher("p2", "33"),             "12p3334567p289p20", new Match(3, "33")},
            new object[] { new Matcher("p1", "46", "b3", "p2"), "12p334567p289p20",  new Match(9, "p2")}
        };
        [Test, TestCaseSource("TextWithMatches_TestCaseFactory")]
        public void IsMatch_GivenTextWithMatches_ReturnsTrue(Matcher m, String testString, Match _expectedMatch)
        {
            Assert.IsTrue(m.IsMatch(testString));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(m.IsMatch(stream));
        }

        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void Match_GivenTextWithNoMatches_ReturnsNull(Matcher m, String testString)
        {
            Assert.IsNull(m.Match(testString));
            using (var stream = StringToStream(testString))
                Assert.IsNull(m.Match(stream));
        }

        [Test, TestCaseSource("TextWithMatches_TestCaseFactory")]
        public void Match_GivenTextWithMatches_ReturnsMatch(Matcher m, String testString, Match expectedMatch)
        {
            Assert.AreEqual(m.Match(testString), expectedMatch);
            using (var stream = StringToStream(testString))
                Assert.AreEqual(m.Match(stream), expectedMatch);
        }

        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void Matches_GivenTextWithNoMatches_ReturnsEmptyList(Matcher m, String testString)
        {
            var expectedMatches = new List<Match>();

            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        static object[] TextWithMatches_ResultAsList_TestCaseFactory = 
        {   //           { Matcher,                             Input text,          
            //             Expected result }
            // Single match test cases:
            new object[] { new Matcher("p1"),                   "1234abp1cd1p",      
                           new List<Match>(new Match[] { new Match(6, "p1") }) },                
            new object[] { new Matcher("p1", "46", "b3", "p2"), "1234567p2890",      
                           new List<Match>(new Match[] { new Match(7, "p2") }) },
            // Multiple matches test cases:
            new object[] { new Matcher("p1"),                   "0p1345p18p292" , 
                           new List<Match>(new Match[] { new Match(1, "p1"),  new Match(6, "p1") }) },
            new object[] { new Matcher("p3", "33"),             "p30p3345p48p291", 
                           new List<Match>(new Match[] { new Match(0, "p3"),  new Match(3, "p3"),  new Match(4, "33") }) },
            new object[] { new Matcher("p1", "p2", "p3", "p4"), "0p3345p48p290",  
                           new List<Match>(new Match[] { new Match(1, "p3"), new Match(6, "p4"),  new Match(9, "p2") }) }
        };
        [Test, TestCaseSource("TextWithMatches_ResultAsList_TestCaseFactory")]
        public void Matches_GivenTextWithMatches_ReturnsListWithMatches(Matcher m, String testString, List<Match> expectedMatches)
        {
            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(testString), expectedMatches));
            using (var stream = StringToStream(testString))
                Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(stream), expectedMatches));
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void IsMatch_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTrue()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            Assert.IsTrue(m.IsMatch(text));
        }

        [Test]
        public void Match_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            Assert.AreEqual(m.Match(text), new Match(4, "p1"));
        }

        [Test]
        public void Matches_GivenMultipleLinesSring_AndTextWithAMatch_ReturnsListWithTheMatch_CountingTheNewline()
        {
            var m = new Matcher("p1");
            var text = "12\r\np13456\r\n7p189p10";
            var expectedMatches = new List<Match> { new Match(4, "p1"), new Match(13, "p1"), new Match(17, "p1") };
            Assert.IsTrue(Enumerable.SequenceEqual(m.Matches(text), expectedMatches));
        }
    }
}
