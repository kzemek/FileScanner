using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public class PreprocessorFactory : IPreprocessorFactory
    {
        public PreprocessorFactory() { }

        /// <summary>
        /// Creates and returns a new Proprocessor instance.
        /// </summary>
        /// <returns>a new Preprocessor instance</returns>
        public IPreprocessor GetIPreprocessor()
        {
            Normalizer normalizer = new Normalizer();
            Inflector inflector = new Inflector();
            return new Preprocessor(normalizer, inflector);
        }
    }
}
