using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    //
    public class Inflector
    {
        private static String[] sufix1 = {
            "a", "owi", "em", "ie", "y", "ow", "om","ami", "ach"
        };

        private static String[] sufixFeminine = {
            "a", "i", "e", "e", "a" //e, ę, ą
        };

        private static Dictionary<string, string[]> sufixes = new Dictionary<String, String[]>() 
        {
            {"z1", new string[]{"a","i","i","ę","ą","i","o"}},
            {"z2", new string[]{"a","y","ę","o","ą"}},
            {"z3", new string[]{"a","i","e","ę","ą","e","o"}},
            {"z4", new string[]{"a","y","e","ę","ą","e","o"}}
        };

        private static Dictionary<string, string[]> grupyDeklinacyjne = new Dictionary<String, String[]> () 
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
        public IEnumerable<String> GetVariations(String phrase)
        {
            //rozpoznajRodzaj(String phrase);
            //String phrase = _phrase;
            if (phrase.EndsWith("a"))
            {
                return GetVariationsFeminine(phrase);
                
            }
            else
            {
                List<String> result = new List<String>();
                for (int i = 0; i < sufix1.Length; ++i)
                {
                    result.Add(phrase + sufix1[i]);
                }
                return result;
            }
        }

        private IEnumerable<String> GetVariationsFeminine(String phrase)
        {
            phrase = phrase.Remove(phrase.Length-1);
            String grupaDeklinacyjna;
            for (int j = 0; j < grupyDeklinacyjne.Length; ++j)
            {
                for (int i = 0; i < grupyDeklinacyjne[j].Length; ++i)
                {
                    if (phrase.EndsWith(grupyDeklinacyjne[j][i]))
                    {
                        grupaDeklinacyjna = "z2";
                        break;
                    }
                }
            }
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            for (int i = 0; i < odmiany[grupaDeklinacyjna].Length; ++i)
            {
                //String s = poczTematu + Obocznosci(koniecTematu+sufixFeminine[i]);
                result.Add(poczTematy+koniecTematu+odmiany[grupaDeklinacyjna][i]);
            }
            return result;
        }

        private String Obocznosci(String koniec) {
            if (wymiany.ContainsKey(koniec) {
                    return wymiany[koniec];
            } else {
                   return koniec;
            }
        }
    }
}
