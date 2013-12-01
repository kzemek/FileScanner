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
            String koniecTematu = phrase.Substring(phrase.Length - 1);
            String poczTematu = phrase.Remove(phrase.Length - 1);
            HashSet<String> result = new HashSet<String>();
            for (int i = 0; i < sufixFeminine.Length; ++i)
            {
                String s = poczTematu + Obocznosci(koniecTematu+sufixFeminine[i]);
                result.Add(s);
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
