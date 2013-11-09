using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private static Dictionary<char, char> _mappings = new Dictionary<char, char>() {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ź', 'z'},
            {'ż', 'z'}
        };

        public String GetNormalizedPhrase(String phrase)
        {
            char[] phraseCharacters = phrase.ToCharArray();
            var normalizedCharacters = phraseCharacters.Select(GetNormalizedCharacter);
            return new String(normalizedCharacters.ToArray());
        }

        private char GetNormalizedCharacter(char c)
        {
            if (_mappings.ContainsKey(c))
                return _mappings[c];
            else
                return c;
        }

        public IEnumerable<String> GetVariations(String phrase)
        {
            IEnumerable<String> result = new List<String>();
            return result;
        }
    }
}
