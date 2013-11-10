using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
	struct SearchResult
	{
		string fileName;
		string fullFilePath;
		DateTime dateCreated;
		DateTime dateLastAccess;
		DateTime dateLastModified;
	}

    class SummaryGenerator: ISummaryGenerator
    {
        void Generate(IEnumerable<FileInfo> searchResults)
        {
            IDocumentBuilder builder = new TxtDocumentBuilder();

			builder.AddReportHeader(DateTime.Now);
			builder.AddSectionHeader("Summary");

			// TODO
			//builder.AddText(String.Format("search query: {0}", "searchQuery"));
			builder.AddText(String.Format("total results: {0}", searchResults.Count()));

			builder.AddSectionHeader("Search results");
			foreach (FileInfo fileInfo in searchResults.OrderByDescending(info => info.accuracy))
			{
				SearchResult result = new SearchResult();
				result.fileName = Path.GetFileName(fileInfo.filePath);
				result.fullFilePath = fileInfo.filePath;
				result.dateCreated = File.GetCreationTime(fileInfo.filePath);
				result.dateLastAccess = File.GetLastAccessTime(fileInfo.filePath);
				result.dateLastModified = File.GetLastAccessTime(fileInfo.filePath);

				builder.AddSearchResult(result);
			}

			builder.Save("report.txt");
        }
    }
}
