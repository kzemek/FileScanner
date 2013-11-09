using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.Preprocessing
{
    public interface IPreprocessor
    {
        /// <summary>
        /// Returns the basic form of the input phrase (e.g. a noun in the nominative case).
        /// If the input phrase contains any non-English characters, they will be
        /// replaced with corresponsing English ones.
        /// </summary>
        /// <param name="phrase">input phrase</param>
        /// <returns>normalized phrase</returns>
        public String GetNormalizedPhrase(String phrase);

        /// <summary>
        /// Tries to find differenct grammar forms of the input phrase.
        /// </summary>
        /// <param name="phrase">input phrase</param>
        /// <returns>collection of different grammar forms of the input phrase</returns>
        public IEnumerable<String> GetVariations(String phrase);
    }
}
