using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        public String GetNormalizedPhrase(String phrase)
        {
            return phrase;
        }

        public IEnumerable<String> GetVariations(String phrase)
        {
            IEnumerable<String> result = new List<String>();
            return result;
        }
    }
}
