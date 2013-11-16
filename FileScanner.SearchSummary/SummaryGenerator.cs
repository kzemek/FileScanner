using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("FileScanner.SearchSummary.Tests")]

namespace FileScanner.SearchSummary
{
    public struct SearchResult
    {
        public string fileName;
        public string fullFilePath;
        public DateTime dateCreated;
        public DateTime dateLastAccess;
        public DateTime dateLastModified;

        public SearchResult(String fileName, String fullFilePath, DateTime dateCreated, DateTime dateLastAccess, DateTime dateLastModified)
        {
            this.fileName = fileName;
            this.fullFilePath = fullFilePath;
            this.dateCreated = dateCreated;
            this.dateLastAccess = dateLastAccess;
            this.dateLastModified = dateLastModified;
        }

    }

    public class SummaryGenerator: ISummaryGenerator
    {
        internal void Generate(IDocumentBuilder builder,
                               string outputFilename,
                               string searchQuery,
                               IEnumerable<string> inputPaths,
                               IEnumerable<MatchingFile> searchResults)
        {
            builder.AddReportHeader(DateTime.Now, searchQuery, inputPaths);
            builder.AddSectionHeader("Summary");

            builder.AddText(String.Format("search query: {0}", searchQuery));
            builder.AddText(String.Format("total results: {0}", searchResults.Count()));
            builder.AddText(String.Format("input paths:\n\t{0}",
                                          inputPaths.Aggregate("", (all, next) => all + "\n" + next)));

            builder.AddSectionHeader("Search results");
            foreach (MatchingFile match in searchResults.OrderByDescending(info => info.accuracy))
            {
                SearchResult result = new SearchResult();
                result.fileName = match.fileInfo.Name;
                result.fullFilePath = match.fileInfo.ToString();
                result.dateCreated = match.fileInfo.CreationTime;
                result.dateLastAccess = match.fileInfo.LastAccessTime;
                result.dateLastModified = match.fileInfo.LastWriteTime;

                builder.AddSearchResult(result);
            }

            builder.Save(outputFilename);
        }

        public void Generate(string searchQuery,
                             IEnumerable<string> inputPaths,
                             IEnumerable<MatchingFile> searchResults)
        {
            Generate(new TxtDocumentBuilder(), "output.txt", searchQuery, inputPaths, searchResults);
        }
    }
}
