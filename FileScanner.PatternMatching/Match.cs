using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PatternMatching
{
    public class Match
    {
        /// <summary>
        /// The position in the original string where the first character of
        /// the captured substring is found.
        /// </summary>
        public int Index { get { return 0; } }

        /// <summary>
        /// Gets the captured substring from the input string.
        /// </summary>
        public String Value { get { return null; } }

        /// <summary>
        /// Returns a new Match object with the results for the next match,
        /// starting at the position at which the last match ended (at the
        /// character after the last matched character).
        /// </summary>
        /// <returns>Match object representing the next match.</returns>
        public Match NextMatch()
        {
            return null;
        }
    }
}
