using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScanner.Preprocessing
{
    public class Inflector
    {
        public IEnumerable<String> GetVariations(String phrase)
        {
            IEnumerable<String> result = new List<String>();
            return result;
        }
        public String GetPluralNominative(String phrase)
        {
            throw new NotImplementedException();
            return "";
        }
    }
}
