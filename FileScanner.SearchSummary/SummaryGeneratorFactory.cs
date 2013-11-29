using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    /// <summary>
    /// Used for creating ISummaryGenerator implemetations.
    /// </summary>
    class SummaryGeneratorFactory
    {
        /// <summary>
        /// Creates a new object that implements ISummaryGenerator interface.
        /// </summary>
        /// <returns>A search summary generator.</returns>
        public static ISummaryGenerator Create()
        {
            return new SummaryGenerator();
        }
    }
}
