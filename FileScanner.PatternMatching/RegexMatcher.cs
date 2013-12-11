using System;
using System.Collections.Generic;
using System.IO;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The implementation of IMatcher interface using Regex class from the
    /// standard library for matching patterns in text.
    /// </summary>
    class RegexMatcher : IMatcher
    {
        private System.Text.RegularExpressions.Regex _regex;

        /// <summary>
        /// Initializes a new instance of the RegexMatcher class for the
        /// specified patterns.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public RegexMatcher(List<string> patterns)
        {            
            patterns.ForEach(p => System.Text.RegularExpressions.Regex.Escape(p));

            var pattern = "(" + String.Join("|", patterns) + ")";
            _regex = new System.Text.RegularExpressions.Regex(pattern);
        }

        public bool IsMatch(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                if (_regex.IsMatch(line))
                    return true;

            return false;
        }

        public Match Match(TextReader reader)
        {
            string line;
            int position = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var match = _regex.Match(line);
                if (match.Value != string.Empty)
                    return new Match(match.Index + position, match.Value);

                position += line.Length + Environment.NewLine.Length;
            }
            
            return null;
        }

        public IEnumerable<Match> Matches(TextReader reader)
        {
            var matches = new List<Match>();

            string line;
            int position = 0;
            while ((line = reader.ReadLine()) != null)
            {
                foreach (System.Text.RegularExpressions.Match match in _regex.Matches(line))
                    matches.Add(new Match(match.Index + position, match.Value));

                position += line.Length + Environment.NewLine.Length;
            }

            return matches;
        }
    }
}
