using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PatternMatching
{
    public class Match
    {
        private int _index;
        private string _value;

        public Match(int index, string value)
        {
            _index = index;
            _value = value;
        }

        /// <summary>
        /// The position in the original string where the first character of
        /// the captured substring is found.
        /// </summary>
        public int Index { get { return _index; } }

        /// <summary>
        /// Gets the captured substring from the input string.
        /// </summary>
        public string Value { get { return _value; } }

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

        public override bool Equals(object obj)
        {
            var other = (Match)obj;
            return _index == other._index && _value == other._value;
        }
    }
}
