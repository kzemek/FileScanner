using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Normalizer
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
        public String RemovePolishCharacters(String phrase)
        {
            char[] phraseCharacters = phrase.ToCharArray();
            var normalizedCharacters = phraseCharacters.Select(GetNormalizedCharacter);
            return new String(normalizedCharacters.ToArray());
        }

        public String GetBasicForm(String phrase)
        {
            String result;
            result = "";
            if (phrase.EndsWith("ów")) {
                //przykładowa końcówka
                result = phrase.Remove(phrase.LastIndexOf("ów"));
            }
            return result;
        }

        private char GetNormalizedCharacter(char c)
        {
            if (_mappings.ContainsKey(c))
                return _mappings[c];
            else
                return c;
        }
    }
}
