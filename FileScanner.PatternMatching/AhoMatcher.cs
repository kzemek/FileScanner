using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The implementation of IMatcher interface using Aho-Corasick algorithm
    /// for matching patterns in text.
    /// </summary>
    class AhoMatcher : IMatcher
    {
        private Trie _machine;

        /// <summary>
        /// Initializes a new instance of the AhoMatcher class for the
        /// specified patterns.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public AhoMatcher(List<string> patterns)
        {
            _machine = new Trie(patterns);
        }

        public bool IsMatch(TextReader reader)
        {
            return Match(reader) != null;
        }

        private KeyValuePair<Match, int> Match(TextReader reader, int position)
        {
            while (reader.Peek() != -1)
            {
                char c = (char) reader.Read();

                ++position;

                var patternMatch = _machine.Feed(c);
                if (patternMatch != null)
                {
                    var match = new Match(position - patternMatch.Length, patternMatch);
                    return new KeyValuePair<Match, int>(match, position);
                }
            }

            return new KeyValuePair<Match, int>(null, position);
        }

        public Match Match(TextReader reader)
        {
            return Match(reader, 0).Key;
        }

        public IEnumerable<Match> Matches(TextReader reader)
        {
            var matches = new List<Match>();
            var position = 0;

            while (reader.Peek() != -1) // until EOF
            {
                var pair = Match(reader, position);
                position = pair.Value;

                if (pair.Key != null)
                    matches.Add(pair.Key);
            }

            return matches;
        }
    }
}
