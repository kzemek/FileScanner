using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The Matcher class serves to find any of given patterns in a given text.
    /// The Matcher's API is similar to that of
    /// <see cref="System.Text.RegularExpressions.Regex"/>.
    /// The available Matcher's method serve to check if a match exists,
    /// return the first match found in text as a
    /// <see cref="FileScanner.PatternMatching.Match"/> object, and return
    /// an object implementing IEnumerable containing all of the matches found
    /// in text.
    /// </summary>
    public class Matcher : IMatcher
    {
        private System.Text.RegularExpressions.Regex _regex;

        private List<string> _patterns;

        /// <summary>
        /// Initializes a new instance of the Matcher class for the specified
        /// patterns.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public Matcher(List<string> patterns)
        {
            if (patterns.Count() == 0)
                throw new ArgumentException("No patterns were given.");

            _patterns = patterns;

            // Regex implementation details
            _patterns.ForEach(p => System.Text.RegularExpressions.Regex.Escape(p));
            var pattern = "(" + String.Join("|", _patterns) + ")";
            _regex = new System.Text.RegularExpressions.Regex(pattern);
        }

        /// <summary>
        /// Initializes a new instance of the Matcher class for the specified
        /// patterns. Patterns can be given as comma-separated values.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public Matcher(params string[] patterns) : this(patterns.ToList<string>())
        {
        }

        /// <summary>
        /// Determines if any of given patterns is present in the text.
        /// </summary>
        /// <param name="reader">
        /// TextReader object from which the text is read.
        /// </param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        public bool IsMatch(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                if (_regex.IsMatch(line))
                    return true;

            return false;
        }

        /// <summary>
        /// Find a single match of any of given patterns in the text.
        /// </summary>
        /// <param name="reader">
        /// TextReader object from which the text is read.
        /// </param>
        /// <returns>
        /// A <see cref="FileScanner.PatternMatching.Match"/> object
        /// representing the match.
        /// </returns>
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

        /// <summary>
        /// Find all matches of given patterns in the text.
        /// </summary>
        /// <param name="reader">
        /// TextReader object from which the text is read.
        /// </param>
        /// <returns>
        /// An iterable collection of
        /// <see cref="FileScanner.PatternMatching.Match"/> objects
        /// representing the matches.
        /// </returns>
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
