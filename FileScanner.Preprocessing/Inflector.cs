using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Inflector
    {
        private static String[] sufix1 = {
            "", "a", "owi", "em", "ie", "y", "ów", "om","ami", "ach"
        };
        public IEnumerable<String> GetVariations(String phrase)
        {
            List<String> result = new List<String>(sufix1);
            for (int i = 0; i < sufix1.Length;++i)
                {
                    result.Add(phrase + sufix1[i]);
                }
            return result;
        }
        public String GetPluralNominative(String phrase)
        {
            throw new NotImplementedException();
            return "";
        }
    }
}
