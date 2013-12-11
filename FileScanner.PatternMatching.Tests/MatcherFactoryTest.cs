using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FileScanner.PatternMatching.Tests
{
    [TestFixture]
    public class MatcherFactoryTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = "No patterns were given.")]
        public void Construct_GivenNoPatterns_ThrowsArgumentException()
        {
            FileScanner.PatternMatching.MatcherFactory factory = new MatcherFactory();
            factory.Create(new List<string>());
        }

        [Test]
        public void Construct_GivenNoAlgorithm_CreatesDefaultMatcher()
        {
            FileScanner.PatternMatching.MatcherFactory factory = new MatcherFactory();
            var patterns = new List<string>{ "a", "b", "c" };

            Assert.AreEqual(factory.Create(patterns).GetType().Name, "AhoMatcher");
        }

        [Test]
        public void Construct_GivenAlgorithm_CreatesTheRightMatcher()
        {
            FileScanner.PatternMatching.MatcherFactory factory = new MatcherFactory();
            var patterns = new List<string>{ "a", "b", "c" };

            Assert.AreEqual(factory.Create(patterns, MatcherFactory.MatchAlgorithm.AhoCorasick).GetType().Name, "AhoMatcher");
            Assert.AreEqual(factory.Create(patterns, MatcherFactory.MatchAlgorithm.Regex).GetType().Name, "RegexMatcher");
        }
    }
}
