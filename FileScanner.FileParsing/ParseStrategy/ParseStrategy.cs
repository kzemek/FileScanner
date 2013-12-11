using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    public static class ParseStrategy
    {
        /// <summary>
        /// Factory method which allows reading files without making any changes. 
        /// </summary>
        /// <returns>
        /// LeaveUnchanged parse strategy (leave averything as it is).
        /// </returns>
        public static IParseStrategy LeaveUnchanged()
        {
            return new BaseParseStrategy();
        }
        /// <summary>
        /// Factory method which enables replacing uppercase letters with their lowercase equivalents.
        /// </summary>
        /// <returns>
        /// Parse strategy which replaces uppercase letters with their lowercase quivalents.
        /// </returns>
        public static IParseStrategy ReplaceCapitalLetters()
        {
            return new RemoveCapitalLettersStrategy();
        }
        /// <summary>
        /// Factory method which enables replacing non ascii letters with diacritic-less equivalents.
        /// </summary>
        /// <returns>
        /// Parse strategy which enables replacing non ascii letters with diacritic-less equivalents.
        /// </returns>
        public static IParseStrategy ReplaceNonASCII()
        {
            return new ReplaceNonASCIIStrategy();
        }
    }
    /// <summary>
    /// Extends the IParseStrategy interface to make usage more intuitive and simple - uses the decorator pattern.
    /// </summary>
    public static class ParseStrategyExtensions
    {
        /// <summary>
        /// Enables replacing uppercase letters with their lowercase equivalents using the decorator pattern.
        /// </summary>
        /// <param name="parseStrategy">The parse strategy on which we deployed the function.</param>
        /// <returns>
        /// The parse strategy decorated using the RemoveCapitalLettersStrategy class.
        /// </returns>
        public static IParseStrategy ReplaceCapitalLetters(this IParseStrategy parseStrategy)
        {
            return new RemoveCapitalLettersStrategy(parseStrategy);
        }
        /// <summary>
        /// Enables replacing non ascii letters with diacritic-less equivalents.
        /// </summary>
        /// <param name="parseStrategy">The parse strategy on which we deployed the function.</param>
        /// <returns>
        /// The parse strategy decorated using the ReplaceNonASCIIStrategy class.
        /// </returns>
        public static IParseStrategy ReplaceNonASCII(this IParseStrategy parseStrategy)
        {
            return new ReplaceNonASCIIStrategy(parseStrategy);
        }
    }
}
