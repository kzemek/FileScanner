using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Inflector
    {

        //class uses polish letters only internally
        private static Dictionary<string, string[]> sufixes = new Dictionary<String, String[]>() 
        {
            {"m1", new string[]{"","a","owi","","em","u","u"}},
            {"m2", new string[]{"","a","owi","a","em","u","u"}},
            {"m3", new string[]{"","a", "a", "owi", "u", "em", "u"}}, //iem
            {"z1", new string[]{"a","i","i","ę","ą","i","o"}},
            {"z2", new string[]{"a","y","ę","o","ą"}},
            {"z3", new string[]{"a","i","e","ę","ą","e","o"}},
            {"z4", new string[]{"a","y","e","ę","ą","e","o"}},
            {"n2", new string[]{"o","a","u","o","em","u","o"}}
        };

        //order of words in dictionary is important
        private static Dictionary<string, string[]> grupyDeklinacyjneM = new Dictionary<String, String[]>() 
        {
            
            {"m3", new string[]{"k","g","ch"}},
            {"m2", new string[]{"c","cz","dz","dż","rz","sz","ż"}},
            {"m1", new string[]{"ć","dź","ń","ś","ź","p","b","m","w","l","j"}},
            {"m4", new string[]{"b","d","f","ł","m","n","p","r","s","t"}}
            //grupa m5
        };
        private static Dictionary<string, string[]> grupyDeklinacyjneZ = new Dictionary<String, String[]> () 
        {
            {"z1", new string[]{"l", "j", "ć", "dź", "ń", "ś", "ż", "pi", "bi", "fi", "wi"}},
            {"z2", new string[]{"c", "cz", "dz", "rz", "sz", "z"}},
            {"z3", new string[]{"k", "g", "ch"}},
            {"z4", new string[]{"b", "d", "f", "ł", "m", "n", "p", "r", "s", "t", "w", "z"}}
        };

        private static Dictionary<string, string[]> grupyDeklinacyjneN = new Dictionary<String, String[]>() 
        {
            //{"n1", new string[]{"ć","dź","ń","ś","ź"}},
            {"n2", new string[]{"k","g","ch"}}
        };
        /*private static String[] rodzZenskiGr2 = {
            "c", "cz", "dz", "rz", "sz", "z" //dż,ż zamiast z
            };*/

        private static Dictionary<String, String> wymiany = new Dictionary<String,String> (){
                                                             {"ke", "ce"},
                                                             {"ki", "cy"},
                                                             {"kem", "kiem"},
                                                             {"ky", "ki"},
                                                             {"ge", "dze"}
                                                             //uzupełnić
        };

        //String phrase;
        //znajduje tylko odmiany rzeczowników nieżywotnych
        //jeszcze nie działa poprawnie
        //na razie tylko liczba pojedyncza
        //zwraca określoną formę tylko jeden raz
        public IEnumerable<String> GetVariations(String phrase)
        {
            String[] phrases = phrase.Split(' ');
                IEnumerable<String>[] results = new IEnumerable<String>[phrases.Length];
                for (int i=0;i<results.Length;++i) 
                {
                    String word = phrases[i];
                    results[i] = GetWordVariations(word);
                }
                return CartesianProduct(results, 0);    
        }

        //return cartesian product of strings separated with spaces
        private IEnumerable<String> CartesianProduct(IEnumerable<string>[] phrases, int i)
        {
             
            if (i==(phrases.Length - 1)) {
                        return phrases.ElementAt(i);
            }
            List<String> results = new List<String>();
            foreach (String variation in phrases[i])
            {

                IEnumerable<String> temporaryResults = CartesianProduct(phrases, i+1);
                foreach (String res in temporaryResults) {
                    results.Add(variation + " "+ res);
                }
            }
            return results;
        }

        private IEnumerable<string> GetWordVariations(String word)
        {
            IEnumerable<String> result;
            if (word.EndsWith("a"))
            {
                result = GetVariationsFeminine(word);

            }
            else
            {
                if (word.EndsWith("o"))
                {
                    result = GetVariationsNeuter(word);
                }
                else
                {
                    result = GetVariationsMasculine(word);
                }
            }
            return result;
        }

        private IEnumerable<string> GetVariationsNeuter(string phrase)
        {
            phrase = phrase.Remove(phrase.Length - 1);
            String grupaDeklinacyjna = null;
            grupaDeklinacyjna = FindDeclensionGroup(phrase, grupaDeklinacyjna, grupyDeklinacyjneN);
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            if (grupaDeklinacyjna == null)
            {
                grupaDeklinacyjna = "n2";
            }
            return AddSufixes(grupaDeklinacyjna, koniecTematu, poczTematu, result);
        }

        private IEnumerable<String> GetVariationsFeminine(String phrase)
        {
            phrase = phrase.Remove(phrase.Length-1);
            String grupaDeklinacyjna = null;
            grupaDeklinacyjna = FindDeclensionGroup(phrase, grupaDeklinacyjna, grupyDeklinacyjneZ);
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            if (grupaDeklinacyjna==null) {
                grupaDeklinacyjna = "z2";
            }
            return AddSufixes(grupaDeklinacyjna, koniecTematu, poczTematu, result);
        }

        private static string FindDeclensionGroup(String phrase, String grupaDeklinacyjna, Dictionary<string, string[]> declensionDictionary)
        {
            foreach (var pair in declensionDictionary)
            {
                for (int i = 0; i < pair.Value.Length; ++i)
                {
                    if (phrase.EndsWith(pair.Value[i]))
                    {
                        grupaDeklinacyjna = pair.Key;
                        break;
                    }
                }
                if (grupaDeklinacyjna != null)
                {
                    break;
                }
            }
            return grupaDeklinacyjna;
        }

        private static IEnumerable<string> AddSufixes(String grupaDeklinacyjna, String koniecTematu, String poczTematu, HashSet<String> result)
        {
            for (int i = 0; i < sufixes[grupaDeklinacyjna].Length; ++i)
            {
                //String s = poczTematu + Obocznosci(koniecTematu+sufixFeminine[i]);
                String s = koniecTematu + sufixes[grupaDeklinacyjna][i];
                if (wymiany.ContainsKey(s))
                {
                    s = wymiany[s];
                }
                //trzeba usunąć polskie znaki
                result.Add(poczTematu + s);
            }
           
            return result;
        }

    private IEnumerable<String> GetVariationsMasculine(String phrase)
        {
            String grupaDeklinacyjna = null;
            grupaDeklinacyjna = FindDeclensionGroup(phrase, grupaDeklinacyjna, grupyDeklinacyjneM);
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            if (grupaDeklinacyjna == null)
            {
                grupaDeklinacyjna = "m2";
            }
            return AddSufixes(grupaDeklinacyjna, koniecTematu, poczTematu, result);
        }

    //from Normalizer
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
    private String RemovePolishCharacters(String phrase)
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
    }
}
