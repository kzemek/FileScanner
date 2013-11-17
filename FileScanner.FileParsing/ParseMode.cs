using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    public class ParseMode
    {
        /// <summary>
        /// Factory method which allows reading files without making any changes. 
        /// </summary>
        /// <returns>
        /// Default parse mode (leave averything as it is).
        /// </returns>
        public static IParseMode Default()
        {
            return new BaseParseMode();
        }
        /// <summary>
        /// Factory method which enables replacing uppercase letters with their lowercase equivalents.
        /// </summary>
        /// <returns>
        /// Parse mode which replaces uppercase letters with their lowercase quivalents.
        /// </returns>
        public static IParseMode ReplaceCapitalLetters()
        {
            return new RemoveCapitalLettersMode();
        }
        /// <summary>
        /// Factory method which enables replacing non ascii letters with diacritic-less equivalents.
        /// </summary>
        /// <returns>
        /// Parse mode which enables replacing non ascii letters with diacritic-less equivalents.
        /// </returns>
        public static IParseMode ReplaceNonASCII()
        {
            return new ReplaceNonASCIIMode();
        }
    }
    /// <summary>
    /// Extends the IParseMode interface to make usage more intuitive and simple - uses the decorator pattern.
    /// </summary>
    public static class ParseModeExtensions
    {
        /// <summary>
        /// Enables replacing uppercase letters with their lowercase equivalents using the decorator pattern.
        /// </summary>
        /// <param name="parseMode">The parse mode on which we deployed the function.</param>
        /// <returns>
        /// The parse mode decorated using the RemoveCapitalLettersMode class.
        /// </returns>
        public static IParseMode RemoveCapitalLetters(this IParseMode parseMode)
        {
            return new RemoveCapitalLettersMode(parseMode);
        }
        /// <summary>
        /// Enables replacing non ascii letters with diacritic-less equivalents.
        /// </summary>
        /// <param name="parseMode">The parse mode on which we deployed the function.</param>
        /// <returns>
        /// The parse mode decorated using the ReplaceNonASCIIMode class.
        /// </returns>
        public static IParseMode ReplaceNonASCII(this IParseMode parseMode)
        {
            return new ReplaceNonASCIIMode(parseMode);
        }
    }
}
