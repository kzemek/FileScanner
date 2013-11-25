using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private Normalizer _normalizer;
        private Inflector _inflector;

        [Obsolete("Please use PreprocessorFactory to get an instance of Preprocessor")]
        public Preprocessor()
        {
            _normalizer = new Normalizer();
            _inflector = new Inflector();
        }

        public Preprocessor(Normalizer normalizer, Inflector inflector)
        {
            this._normalizer = normalizer;
            this._inflector = inflector;
        }

        public String GetNormalizedPhrase(String phrase)
        {
            string phraseWithoutPolishCharacters = _normalizer.RemovePolishCharacters(phrase);
            string basicForm = _normalizer.GetBasicForm(phraseWithoutPolishCharacters);
            return basicForm;
        }

        public IEnumerable<String> GetVariations(String phrase)
        {
            IEnumerable<String> variations = _inflector.GetVariations(phrase);
            return variations;
        }
    }
}
