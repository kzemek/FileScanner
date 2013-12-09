using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Normalizer
    {
        #region polishEnglishMappings
        private static Dictionary<char, char> polishEnglishMapping = new Dictionary<char, char>() {
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
        #endregion

        #region suffixMappings
        private static Dictionary<string, string> suffixMappings = new Dictionary<string, string> {
            {"gu", "g"}, {"hu", "u"}, {"ii", "ia"}, {"ju", "j"}, {"ku", "u"}, {"lu", "l"}, {"wu", "w"}, {"zu", "u"},
            {"owi", ""},
            {"em", ""},
            {"cu", "c"}, {"rze", "r"},
            {"y", ""}, {"je", "j"}, {"ki", "k"}, {"sci", "sta"},
            {"ow", ""},
            {"om", ""},
            {"ami", ""},
            {"ach", ""}
        };
        private static Dictionary<string, string> suffixWithPalatilizingMappings = new Dictionary<string, string> {
            {"iem", ""},
            {"ie", ""}
        };
        #endregion

        /// <summary>
        /// Returns a string with Polish characters converted to English ones and all letters converted to lower-case.
        /// </summary>
        /// <param name="phrase">input word</param>
        /// <returns>converted word</returns>
        public String RemovePolishCharacters(String phrase)
        {
            string lowerCasePhrase = phrase.ToLower();
            char[] phraseCharacters = lowerCasePhrase.ToCharArray();
            var normalizedCharacters = phraseCharacters.Select(GetNormalizedCharacter);
            return new String(normalizedCharacters.ToArray());
        }

        private char GetNormalizedCharacter(char c)
        {
            if (polishEnglishMapping.ContainsKey(c))
                return polishEnglishMapping[c];
            else
                return c;
        }

        /// <summary>
        /// Finds the basic form for the given word. For example, returns a singular nominative form if a noun is given.
        /// </summary>
        /// <param name="phrase">input word</param>
        /// <returns>basic form of the given word</returns>
        public String GetBasicForm(String phrase)
        {
            StringBuilder mutablePhrase = new StringBuilder(phrase);
            bool changed = false;
            foreach (var suffixMapping in suffixWithPalatilizingMappings)
            {
                string suffix = suffixMapping.Key;
                if (mutablePhrase.ToString().EndsWith(suffix))
                {
                    string newSuffix = suffixMapping.Value;
                    ReplaceSuffix(mutablePhrase, suffix, newSuffix);
                    RemovePalatization(mutablePhrase);
                    changed = true;
                    break;
                }
            }
            if (!changed)
            {
                foreach (var suffixMapping in suffixMappings)
                {
                    string suffix = suffixMapping.Key;
                    if (mutablePhrase.ToString().EndsWith(suffix))
                    {
                        string newSuffix = suffixMapping.Value;
                        ReplaceSuffix(mutablePhrase, suffix, newSuffix);
                        break;
                    }
                }
            }
            AddMissingE(mutablePhrase);
            return mutablePhrase.ToString();
        }

        private void ReplaceSuffix(StringBuilder mutablePhrase, string suffix, string newSuffix)
        {
            int suffixStart = mutablePhrase.ToString().LastIndexOf(suffix);
            mutablePhrase.Remove(suffixStart, suffix.Length);
            mutablePhrase.Append(newSuffix);
        }

        private void RemovePalatization(StringBuilder mutablePhrase)
        {
            int lastIndex = mutablePhrase.Length - 1;
            if (mutablePhrase[lastIndex] == 'c')
                mutablePhrase[lastIndex] = 't';
            else if (mutablePhrase.ToString().EndsWith("dz"))
                mutablePhrase.Remove(lastIndex, 1);
        }

        private void AddMissingE(StringBuilder mutablePhrase)
        {
            if (mutablePhrase.ToString().EndsWith("rk"))
                ReplaceSuffix(mutablePhrase, "rk", "rek");
            else if (mutablePhrase.ToString().EndsWith("tk"))
                ReplaceSuffix(mutablePhrase, "tk", "tek");
        }
    }
}
