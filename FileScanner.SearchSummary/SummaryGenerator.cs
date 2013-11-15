using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    public struct SearchResult
    {
        public string fileName;
        public string fullFilePath;
        public DateTime dateCreated;
        public DateTime dateLastAccess;
        public DateTime dateLastModified;
    }

    class SummaryGenerator: ISummaryGenerator
    {
        public void Generate(string searchQuery,
                             IEnumerable<string> inputPaths,
                             IEnumerable<MatchingFile> searchResults)
        {
            IDocumentBuilder builder = new TxtDocumentBuilder();

            builder.AddReportHeader(DateTime.Now);
            builder.AddSectionHeader("Summary");

            // TODO
            //builder.AddText(String.Format("search query: {0}", "searchQuery"));
            builder.AddText(String.Format("total results: {0}", searchResults.Count()));

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

            builder.Save("report.txt");
        }
    }
}
