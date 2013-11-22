using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private Normalizer n;
        private Inflector i;
        public Preprocessor()
        {
            n = new Normalizer();
            i = new Inflector();
        }
        public Preprocessor(Normalizer _n, Inflector _i)
        {
            this.n = _n;
            this.i = _i;
        }
        public String GetNormalizedPhrase(String phrase)
        {
            return n.RemovePolishCharacters(phrase);

        }

        public IEnumerable<String> GetVariations(String phrase)
        {
            Inflector i = new Inflector();
            return i.GetVariations(phrase);
        }
    }
}
