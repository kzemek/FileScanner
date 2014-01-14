using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using FileScanner.Preprocessing;
using FileScanner.PatternMatching;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace FileScanner.Tests
{
    [TestClass]
    public class SearcherTest
    {
        private MockFactory _mockFactory = new MockFactory();

        private Searcher _searcher;

        private Mock<IPreprocessor> _preprocessor;
        private Mock<MatcherFactory> _matcherFactory;


        [TestInitialize]
        public void SetUp()
        {
            _preprocessor = _mockFactory.CreateMock<IPreprocessor>(MockStyle.Stub);
            _matcherFactory = _mockFactory.CreateMock<MatcherFactory>(MockStyle.RecursiveStub);

            _searcher = new Searcher(_preprocessor.MockObject, _matcherFactory.MockObject);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _mockFactory.ClearExpectations();
        }


        [TestMethod]
        public void Search_SearchPhrases_AllPhrasesPreprocessedOnce()
        {
            // Arrange
            var searchPhrases = new[] { "p1", "p2", "p3" };
            var normalizedPhrases = new[] { "1", "2", "3" };

            for (int i = 0; i < searchPhrases.Length; ++i)
            {
                _preprocessor.Expects.One.MethodWith(x => x.GetNormalizedPhrase(searchPhrases[i])).WillReturn(normalizedPhrases[i]);
                _preprocessor.Expects.One.MethodWith(x => x.GetVariations(normalizedPhrases[i])).WillReturn(new[] { normalizedPhrases[i] });
            }

            // Act
            _searcher.Search(Enumerable.Empty<ISearchee>(), searchPhrases);

            // Assert
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void Search_Searchees_AllSearcheesProcessedOnce()
        {
            // Arrange
            const int SearcheesCount = 3;

            var searcheeMocks = new List<Mock<ISearchee>>();

            for (int i = 0; i < SearcheesCount; ++i)
            {
                var mock = _mockFactory.CreateMock<ISearchee>();
                mock.Expects.AtMostOne.GetProperty(x => x.Reader).WillReturn(new StreamReader(new MemoryStream()));
                searcheeMocks.Add(mock);
            }

            var searchees = from mock in searcheeMocks select mock.MockObject;
            var phrases = new string[] { "phrase1", "phrase2", "phrase3" };

            _preprocessor.Expects.Any.Method(x => x.GetNormalizedPhrase(null)).WithAnyArguments().WillReturn(string.Empty);
            _preprocessor.Expects.Any.Method(x => x.GetVariations(null)).WithAnyArguments().WillReturn(phrases);

            // Act
            var results = _searcher.Search(searchees, phrases);

            // Assert
            Assert.AreEqual(searchees.Count(), results.ProcessedSearcheesCount);
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }


        // Matcher factory throws exception when it receives empty collection of patterns
        // so one must ensure MatcherFactory.Create(...) is not called in that cases
        [TestMethod]
        public void Search_ZeroPreprocessedPhrases_MatcherNotCalled()
        {
            // Arrange
            var searcheeMock = _mockFactory.CreateMock<ISearchee>();
            searcheeMock.Expects.Any
                .GetProperty(x => x.Reader)
                .WillReturn(new StreamReader(new MemoryStream()));

            var searchees = new[] { searcheeMock.MockObject };

            _preprocessor.Expects.Any
                .Method(x => x.GetVariations(null))
                .WithAnyArguments()
                .WillReturn(Enumerable.Empty<string>());

            _matcherFactory.Expects.No
                .Method(x => x.Create(null, MatcherFactory.MatchAlgorithm.AhoCorasick))
                .WithAnyArguments();

            // Act
            _searcher.Search(searchees);

            // Assert
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void Search_SearcheeAndNoMatches_EmptyResult()
        {
            // Arrange
            var matcherMock = _mockFactory.CreateMock<IMatcher>();
            matcherMock.Expects.Any
                .Method(x => x.Matches(null))
                .WillReturn(Enumerable.Empty<Match>());

            _matcherFactory.Expects.Any
                .Method(x => x.Create(null, MatcherFactory.MatchAlgorithm.AhoCorasick))
                .WithAnyArguments()
                .WillReturn(matcherMock.MockObject);

            var searcheeMock = _mockFactory.CreateMock<ISearchee>();
            searcheeMock.Expects.Any
                .GetProperty(x => x.Reader)
                .WillReturn(new StreamReader(new MemoryStream()));

            var searchees = new[] { searcheeMock.MockObject };

            // Act
            var results = _searcher.Search(searchees);

            // Assert
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
            Assert.IsFalse(results.Searchees.Any());
        }


        [TestMethod]
        public void Search_SearcheesAndMatches_MatchedSearcheesInResult()
        {
            // Arrange
            var matchingReader = new StreamReader(new MemoryStream());
            var matchingSearcheeMock = _mockFactory.CreateMock<ISearchee>();
            matchingSearcheeMock.Expects.Any
                    .GetProperty(x => x.Reader).WillReturn(matchingReader);

            var notMatchingReader = new StreamReader(new MemoryStream());
            var notMatchingSearcheeMock = _mockFactory.CreateMock<ISearchee>();
            notMatchingSearcheeMock.Expects.Any
                .GetProperty(x => x.Reader).WillReturn(notMatchingReader);

            const string SearchPhrase = "s";
            var match = new Match(0, SearchPhrase);

            var matcherMock = _mockFactory.CreateMock<IMatcher>(MockStyle.RecursiveStub);
            matcherMock.Expects.Any
                .MethodWith(x => x.Matches(matchingReader)).WillReturn(new[] { match });

            _matcherFactory.Expects.Any
                .Method(x => x.Create(null, MatcherFactory.MatchAlgorithm.AhoCorasick))
                .WithAnyArguments().WillReturn(matcherMock.MockObject);

            var searchees = new[] { matchingSearcheeMock.MockObject, notMatchingSearcheeMock.MockObject };

            var phrases = new[] { "phrase" };

            _preprocessor.Expects.Any.Method(x => x.GetNormalizedPhrase(null)).WithAnyArguments().WillReturn(string.Empty);
            _preprocessor.Expects.Any.Method(x => x.GetVariations(null)).WithAnyArguments().WillReturn(phrases);

            // Act
            var results = _searcher.Search(searchees, phrases);

            // Assert
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
            Assert.AreEqual(searchees.Count(), results.ProcessedSearcheesCount);
            Assert.AreEqual(1, results.Searchees.Count());
            Assert.AreSame(matchingSearcheeMock.MockObject, results.Searchees.First().Searchee);
            Assert.AreEqual(1, results.Searchees.First().Matches.Count());
            Assert.AreSame(match, results.Searchees.First().Matches.First());
        }


        [TestMethod]
        public void Search_AnySearch_SearchPhrasesAreIncludedInSearchResults()
        {
            // Arrange
            var phrases = new[] { "phrase1", "phrase2", "phrase3" };

            // Act
            var results = _searcher.Search(Enumerable.Empty<ISearchee>(), phrases);

            // Assert
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
            Assert.IsTrue(phrases.SequenceEqual(results.Phrases));
        }


        [TestMethod]
        public void Search_AnySearch_SearchDatesAreAuthentic()
        {
            // Arrange
            var before = DateTime.Now;

            // Act
            var results = _searcher.Search(Enumerable.Empty<ISearchee>());
            var after = DateTime.Now;

            // Assert

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
            Assert.IsTrue(before <= results.StartDate);
            Assert.IsTrue(results.StartDate <= results.EndDate);
            Assert.IsTrue(results.EndDate <= after);
        }
    }
}

