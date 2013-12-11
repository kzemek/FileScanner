using System;
namespace FileScanner.PatternMatching
{
    /// <summary>
    /// Classes implementing IMatcher interface serve to find any of given
    /// patterns in a given text. The API is similar to that of
    /// <see cref="System.Text.RegularExpressions.Regex"/>.
    /// The available IMatcher's method serve to check if a match exists,
    /// return the first match found in text as a
    /// <see cref="FileScanner.PatternMatching.Match"/> object, and return
    /// an object implementing IEnumerable containing all of the matches found
    /// in text.
    /// </summary>
    public interface IMatcher
    {
        /// <summary>
        /// Determines if any of given patterns is present in the text.
        /// </summary>
        /// <param name="reader">
        /// TextReader object from which the text is read.
        /// </param>
        /// <returns>
        /// True if one of patterns is found in the text, false otherwise.
        /// </returns>
        bool IsMatch(System.IO.TextReader reader);

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
        Match Match(System.IO.TextReader reader);

        /// <summary>
        /// Find all matches of given patterns in the text. Overlapping matches
        /// may or may not be found.
        /// </summary>
        /// <param name="reader">
        /// TextReader object from which the text is read.
        /// </param>
        /// <returns>
        /// An iterable collection of
        /// <see cref="FileScanner.PatternMatching.Match"/> objects
        /// representing the matches.
        /// </returns>
        System.Collections.Generic.IEnumerable<Match> Matches(System.IO.TextReader reader);
    }
}
