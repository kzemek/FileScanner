using FileScanner.PatternMatching;
using FileScanner.Preprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileScanner
{
    public class Searcher
    {
        private IPreprocessor _preprocessor;
        private MatcherFactory _matcherFactory;

        public Searcher(IPreprocessor preprocessor, MatcherFactory matcherFactory)
        {
            _preprocessor = preprocessor;
            _matcherFactory = matcherFactory;
        }


        public ISearchResult Search(IEnumerable<ISearchee> searchees, params string[] searchPhrases)
        {
            var start = DateTime.Now;

            var preprocessedPhrases = PreprocessPhrases(searchPhrases);
            var searcheeResults = PerformSearch(searchees, preprocessedPhrases);

            var end = DateTime.Now;

            return new SearchResult(searchPhrases, searcheeResults, searchees.Count(), start, end);
        }


        private List<string> PreprocessPhrases(IEnumerable<string> searchPhrases)
        {
            var preprocessedPhrases = (from phrase in searchPhrases
                                       select PreprosessPhrase(phrase)).SelectMany(x => x).ToList();
            return preprocessedPhrases;
        }


        private IEnumerable<string> PreprosessPhrase(string phrase)
        {
            return _preprocessor.GetVariations(_preprocessor.GetNormalizedPhrase(phrase));
        }


        private IEnumerable<SearcheeResult> PerformSearch(IEnumerable<ISearchee> searchees, List<string> preprocessedPhrases)
        {
            var results = new List<SearcheeResult>();

            if (preprocessedPhrases.Any())
            {
                foreach (var searchee in searchees)
                {
                    var matcher = _matcherFactory.Create(preprocessedPhrases);
                    var matches = matcher.Matches(searchee.Reader);

                    if (matches.Any())
                    {
                        var searcheeResult = new SearcheeResult(searchee, matches);
                        results.Add(searcheeResult);
                    }
                }
            }

            return results;
        }
    }
}
