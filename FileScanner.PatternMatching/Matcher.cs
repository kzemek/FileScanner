using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The Matcher class serves to find any of given patterns in a given text.
    /// The Matcher's API is similar to that of
    /// <see cref="System.Text.RegularExpressions.Regex"/>.
    /// The available Matcher's method serve to check if a match exists,
    /// return the first match found in text as a
    /// <see cref="FileScanner.PatternMatching.Match"/> object, and return
    /// a list of all of the matches found in text.
    /// </summary>
    public class Matcher
    {
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
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        public bool IsMatch(Stream text)
        {
            var reader = new StreamReader(text);
            return IsMatch(reader.ReadToEnd());
        }

        /// <summary>
        /// Determines if any of given patterns is present in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        public bool IsMatch(string text)
        {
            return _patterns.Any(p => text.Contains(p));
        }

        /// <summary>
        /// Find a single match of any of given patterns in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// A <see cref="FileScanner.PatternMatching.Match"/> object
        /// representing the match.
        /// </returns>
        public Match Match(Stream text)
        {
            var reader = new StreamReader(text);
            return Match(reader.ReadToEnd());
        }

        /// <summary>
        /// Find a single match of any of given patterns in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// A <see cref="FileScanner.PatternMatching.Match"/> object
        /// representing the match.
        /// </returns>
        public Match Match(string text)
        {
            Match m = null;

            foreach (var p in _patterns)
            {
                int index = text.IndexOf(p);
                if (index != -1)
                    if (m == null || index < m.Index)
                        m = new Match(index, p);
            }

            return m;
        }

        /// <summary>
        /// Find all matches of given patterns in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// An iterable collection of
        /// <see cref="FileScanner.PatternMatching.Match"/> objects
        /// representing the matches.
        /// </returns>
        public IEnumerable<Match> Matches(Stream text)
        {
            var reader = new StreamReader(text);
            return Matches(reader.ReadToEnd());
        }

        /// <summary>
        /// Find all matches of given patterns in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// An iterable collection of
        /// <see cref="FileScanner.PatternMatching.Match"/> objects
        /// representing the matches.
        /// </returns>
        public IEnumerable<Match> Matches(string text)
        {
            var matches = new SortedList<int, string>();
            foreach (var p in _patterns)
            {
                for (int index = 0; (index = text.IndexOf(p, index)) != -1; index += p.Count())
                    if (!matches.ContainsKey(index))
                        matches.Add(index, p);
            }

            var r = new List<Match>();
            foreach (var match in matches)
                r.Add(new Match(match.Key, match.Value));

            return r;
        }
    }
}
