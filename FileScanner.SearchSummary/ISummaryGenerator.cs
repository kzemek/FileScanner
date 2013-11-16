using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileScanner.SearchSummary
{
    /// <summary>
    /// Structure containing information about a single file that matches search query.
    /// </summary>
    public struct MatchingFile
    {
        /// <summary>
        /// A file containing data that matches the search query.
        /// </summary>
        public FileInfo fileInfo;

        /// <summary>
        /// Dictionary: phrase => occurrence positions inside the file.
        /// Positions should be given as absolute positions inside the original (not preprocessed) file.
        /// Note: occurrence positions will NOT be used in the first iteration.
        /// </summary>
        public IDictionary<string, IEnumerable<int>> searchResults;

        /// <summary>
        /// Describes how well the file matches the search query.
        /// This value should be greater for better matches.
        /// </summary>
        public float accuracy;
    }

    /// <summary>
    /// Generates reports from search results.
    /// </summary>
    public interface ISummaryGenerator
    {
        /// <summary>
        /// Asks the user for specific report options and generates a report in used-specified file format.
        /// </summary>
        /// <param name="searchQuery">
        /// Original search query, as received from the user.
        /// </param>
        /// <param name="inputPaths">
        /// Collection of full paths to all files and directories that were specified by the user as input
        /// for the search algorithm.
        /// </param>
        /// <param name="searchResults">
        /// Collection of files matching the search query. For details, see FileInfo struct documentation.
        /// </param>
        void Generate(string searchQuery,
                      IEnumerable<string> inputPaths,
                      IEnumerable<MatchingFile> searchResults);
    }
}
