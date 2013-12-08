using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Inflector
    {
        /*private static String[] sufix1 = {
            "a", "owi", "em", "ie", "y", "ow", "om","ami", "ach"
        };*/

        /*private static String[] sufixFeminine = {
            "a", "i", "e", "e", "a" //e, ę, ą
        };*/

        //class uses polish letters only internally
        private static Dictionary<string, string[]> sufixes = new Dictionary<String, String[]>() 
        {
            {"m1", new string[]{"","a","owi","","em","u","u"}},
            {"m2", new string[]{"","a","owi","a","em","u","u"}},
            {"z1", new string[]{"a","i","i","ę","ą","i","o"}},
            {"z2", new string[]{"a","y","ę","o","ą"}},
            {"z3", new string[]{"a","i","e","ę","ą","e","o"}},
            {"z4", new string[]{"a","y","e","ę","ą","e","o"}}
        };

        //order of words in dictionary is important
        private static Dictionary<string, string[]> grupyDeklinacyjneM = new Dictionary<String, String[]>() 
        {
            {"m3", new string[]{"k","g","ch"}},
            {"m2", new string[]{"c","cz","dz","dż","rz","sz","ż"}},
            {"m1", new string[]{"ć","dź","ń","ś","ź","p","b","m","w","l","j"}}
        };
        private static Dictionary<string, string[]> grupyDeklinacyjneZ = new Dictionary<String, String[]> () 
        {
            {"z1", new string[]{"l", "j", "ć", "dź", "ń", "ś", "ż", "pi", "bi", "fi", "wi"}},
            {"z2", new string[]{"c", "cz", "dz", "rz", "sz", "z"}},
            {"z3", new string[]{"k", "g", "ch"}},
            {"z4", new string[]{"b", "d", "f", "ł", "m", "n", "p", "r", "s", "t", "w", "z"}}
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
        };

        //String phrase;
        //znajduje tylko odmiany rzeczowników nieżywotnych
        //jeszcze nie działa poprawnie
        //tylko liczba pojedyncza
        public IEnumerable<String> GetVariations(String phrase)
        {
            //rozpoznajRodzaj(String phrase);
            //String phrase = _phrase;
            IEnumerable<String> result;
            if (phrase.EndsWith("a"))
            {
                result =  GetVariationsFeminine(phrase);
                
            }
            else
            {
                result = GetVariationsMasculine(phrase);
            }
            return result;
        }

        private IEnumerable<String> GetVariationsFeminine(String phrase)
        {
            phrase = phrase.Remove(phrase.Length-1);
            String grupaDeklinacyjna = null;
            foreach (var pair in grupyDeklinacyjneZ)
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
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            if (grupaDeklinacyjna==null) {
                grupaDeklinacyjna = "z2";
            }
            for (int i = 0; i < sufixes[grupaDeklinacyjna].Length; ++i)
            {
                //String s = poczTematu + Obocznosci(koniecTematu+sufixFeminine[i]);
                result.Add(poczTematu+koniecTematu+sufixes[grupaDeklinacyjna][i]);
            }
            return result;
        }

    private IEnumerable<String> GetVariationsMasculine(String phrase)
        {
            String grupaDeklinacyjna = null;
            foreach (var pair in grupyDeklinacyjneM)
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
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            if (grupaDeklinacyjna == null)
            {
                grupaDeklinacyjna = "m2";
            }
            for (int i = 0; i < sufixes[grupaDeklinacyjna].Length; ++i)
            {
                //String s = poczTematu + Obocznosci(koniecTematu+sufixFeminine[i]);
                result.Add(poczTematu + koniecTematu + sufixes[grupaDeklinacyjna][i]);
            }
            return result;
        }

        private String Obocznosci(String koniec) {
            if (wymiany.ContainsKey(koniec)) {
                    return wymiany[koniec];
            } else {
                   return koniec;
            }
        }
    }
}
