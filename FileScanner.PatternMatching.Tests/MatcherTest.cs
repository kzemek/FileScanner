using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileScanner.PatternMatching.Tests
{
    [TestFixture]
    public class MatcherTest
    {
        private MatcherFactory factory = new MatcherFactory();

        static object[] TextWithNoMatches_TestCaseFactory = 
        {   //           { MatcherAlgorithm,                          Patterns,                                 Input text    }
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1"},                   "1234abcd1p"  },
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "p2"},             "1234pabcd1p" },
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "ab", "b3", "d4"}, "1234papbcd1p"},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1"},                   "1234abcd1p"  },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "p2"},             "1234pabcd1p" },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "ab", "b3", "d4"}, "1234papbcd1p"},
        };

        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void IsMatch_GivenTextWithNoMatches_ReturnsFalse(MatcherFactory.MatchAlgorithm algorithm,
                                                                List<string> patterns, String testString)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.IsFalse(matcher.IsMatch(new StringReader(testString)));
        }

        static object[] TextWithMatches_TestCaseFactory = 
        {   //           { MatcherAlgorithm,                          Patterns,                                 Input text,          Expected result   }
            // Single match test cases:
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1"},                   "1234abp1cd1p",      new Match(6, "p1")},                
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "p2"},             "1p234pabcd1p",      new Match(1, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "p2"},             "1234pabcdp1",       new Match(9, "p1")},
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "p2"},             "p234pabcd1p",       new Match(0, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "46", "b3", "p2"}, "1234567p2890",      new Match(7, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1"},                   "1234abp1cd1p",      new Match(6, "p1")},                
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "p2"},             "1p234pabcd1p",      new Match(1, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "p2"},             "1234pabcdp1",       new Match(9, "p1")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "p2"},             "p234pabcd1p",       new Match(0, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "46", "b3", "p2"}, "1234567p2890",      new Match(7, "p2")},

            // Multiple matches test cases:
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1"},                   "12p134567p189p10",  new Match(2, "p1")},
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p2", "33"},             "12p3334567p289p20", new Match(3, "33")},
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "46", "b3", "p2"}, "12p334567p289p20",  new Match(9, "p2")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1"},                   "12p134567p189p10",  new Match(2, "p1")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p2", "33"},             "12p3334567p289p20", new Match(3, "33")},
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "46", "b3", "p2"}, "12p334567p289p20",  new Match(9, "p2")}
        };

        [Test, TestCaseSource("TextWithMatches_TestCaseFactory")]
        public void IsMatch_GivenTextWithMatches_ReturnsTrue(MatcherFactory.MatchAlgorithm algorithm,
                                                             List<string> patterns, String testString,
                                                             Match expectedMatch)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.IsTrue(matcher.IsMatch(new StringReader(testString)));
        }

        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void Match_GivenTextWithNoMatches_ReturnsNull(MatcherFactory.MatchAlgorithm algorithm,
                                                             List<string> patterns, String testString)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.IsNull(matcher.Match(new StringReader(testString)));
        }

        [Test, TestCaseSource("TextWithMatches_TestCaseFactory")]
        public void Match_GivenTextWithMatches_ReturnsMatch(MatcherFactory.MatchAlgorithm algorithm,
                                                            List<string> patterns, String testString,
                                                            Match expectedMatch)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.AreEqual(matcher.Match(new StringReader(testString)), expectedMatch);
        }

        [Test, TestCaseSource("TextWithNoMatches_TestCaseFactory")]
        public void Matches_GivenTextWithNoMatches_ReturnsEmptyList(MatcherFactory.MatchAlgorithm algorithm,
                                                                    List<string> patterns, String testString)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.IsEmpty(matcher.Matches(new StringReader(testString)));
        }

        static object[] TextWithMatches_ResultAsList_TestCaseFactory = 
        {   //           { MatchAlgorithm,                            Patterns,                                 Input text,         Expected result }
            // Single match test cases:
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1"},                   "1234abp1cd1p",     new List<Match>{new Match(6, "p1")} },
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "46", "b3", "p2"}, "1234567p2890",     new List<Match>{new Match(7, "p2")} },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1"},                   "1234abp1cd1p",     new List<Match>{new Match(6, "p1")} },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "46", "b3", "p2"}, "1234567p2890",     new List<Match>{new Match(7, "p2")} },
            // Multiple matches test cases:
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1"},                   "0p1345p18p292",    new List<Match>{new Match(1, "p1"), new Match(6, "p1")} },
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p3", "33"},             "p30p33345p48p291", new List<Match>{new Match(0, "p3"), new Match(3, "p3"), new Match(5, "33")} },
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick, new List<string>{"p1", "p2", "p3", "p4"}, "0p3345p48p290",    new List<Match>{new Match(1, "p3"), new Match(6, "p4"), new Match(9, "p2")} },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1"},                   "0p1345p18p292",    new List<Match>{new Match(1, "p1"), new Match(6, "p1")} },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p3", "33"},             "p30p33345p48p291", new List<Match>{new Match(0, "p3"), new Match(3, "p3"), new Match(5, "33")} },
            new object[] { MatcherFactory.MatchAlgorithm.Regex,       new List<string>{"p1", "p2", "p3", "p4"}, "0p3345p48p290",    new List<Match>{new Match(1, "p3"), new Match(6, "p4"), new Match(9, "p2")} }
        };

        [Test, TestCaseSource("TextWithMatches_ResultAsList_TestCaseFactory")]
        public void Matches_GivenTextWithMatches_ReturnsListWithMatches(MatcherFactory.MatchAlgorithm algorithm,
                                                                        List<string> patterns, String testString,
                                                                        List<Match> expectedMatches)
        {
            var matcher = factory.Create(patterns, algorithm);
            Assert.IsTrue(Enumerable.SequenceEqual(matcher.Matches(new StringReader(testString)), expectedMatches));
        }

        static object[] MiscellaneousTests_VariousAlgorithms = 
        {   
            new object[] { MatcherFactory.MatchAlgorithm.AhoCorasick },
            new object[] { MatcherFactory.MatchAlgorithm.Regex }
        };

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void IsMatch_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsTrue(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                Assert.IsTrue(matcher.IsMatch(new StreamReader(stream)));
            }
        }

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void Match_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsTheMatch_CountingTheNewline(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                Assert.AreEqual(matcher.Match(new StreamReader(stream)), new Match(4, "p1"));
            }
        }

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void Matches_GivenMultipleLinesStream_AndTextWithAMatch_ReturnsListWithTheMatch_CountingTheNewline(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.WriteLine("12");
                writer.WriteLine("p13456");
                writer.WriteLine("7p189p10");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                var expectedMatches = new List<Match> { new Match(4, "p1"), new Match(13, "p1"), new Match(17, "p1") };
                Assert.IsTrue(Enumerable.SequenceEqual(matcher.Matches(new StreamReader(stream)), expectedMatches));
            }
        }

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void IsMatch_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTrue(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);
            var text = "12\r\np13456\r\n7p189p10";
            Assert.IsTrue(matcher.IsMatch(new StringReader(text)));
        }

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void Match_GivenMultipleLinesString_AndTextWithAMatch_ReturnsTheMatch_CountingTheNewline(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);
            var text = "12\r\np13456\r\n7p189p10";
            Assert.AreEqual(matcher.Match(new StringReader(text)), new Match(4, "p1"));
        }

        [Test, TestCaseSource("MiscellaneousTests_VariousAlgorithms")]
        public void Matches_GivenMultipleLinesSring_AndTextWithAMatch_ReturnsListWithTheMatch_CountingTheNewline(MatcherFactory.MatchAlgorithm algorithm)
        {
            var matcher = factory.Create(new List<string> { "p1" }, algorithm);
            var text = "12\r\np13456\r\n7p189p10";
            var expectedMatches = new List<Match> { new Match(4, "p1"), new Match(13, "p1"), new Match(17, "p1") };
            Assert.IsTrue(Enumerable.SequenceEqual(matcher.Matches(new StringReader(text)), expectedMatches));
        }
    }
}
