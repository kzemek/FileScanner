using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// The Match object represents a single match of a pattern given, in text
    /// given to a <see cref="FileScanner.PatternMatching.Matcher"/> method.
    /// </summary>
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
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Match)obj;
            return _index == other._index && _value == other._value;
        }

        public override int GetHashCode()
        {
            return _index.GetHashCode() * _value.GetHashCode();
        }
    }
}
