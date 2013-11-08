using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PatternMatching
{
    public class Matcher
    {
        /// <summary>
        /// Initializes a new instance of the Matcher class for the specified
        /// patterns.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public Matcher(List<String> patterns)
        { }

        /// <summary>
        /// Initializes a new instance of the Matcher class for the specified
        /// patterns. Patterns can be given as comma-separated values.
        /// </summary>
        /// <param name="patterns">The patterns to search for in text.</param>
        public Matcher(params String[] patterns)
        { }

        /// <summary>
        /// Determines if any of given patterns is present in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        public bool IsMatch(Stream text)
        {
            return false;
        }

        /// <summary>
        /// Determines if any of given patterns is present in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        public bool IsMatch(String text)
        {
            return false;
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
            return null;
        }

        /// <summary>
        /// Find a single match of any of given patterns in the text.
        /// </summary>
        /// <param name="text">Text in which to search for patterns.</param>
        /// <returns>
        /// A <see cref="FileScanner.PatternMatching.Match"/> object
        /// representing the match.
        /// </returns>
        public Match Match(String text)
        {
            return null;
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
            return null;
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
        public IEnumerable<Match> Matches(String text)
        {
            return null;
        }
    }
}
