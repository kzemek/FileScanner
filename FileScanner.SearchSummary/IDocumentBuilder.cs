using System;
using System.Collections.Generic;

namespace FileScanner.SearchSummary
{
	public interface IDocumentBuilder
	{
        void AddReportHeader(DateTime? generationTime,
                             String userQuery,
                             IEnumerable<String> searchedLocations);
        void AddReportFooter();

        void AddSectionHeader(string text);
        void AddSearchResult(SearchResult result);

        void Save(string filePath);
	}
}

