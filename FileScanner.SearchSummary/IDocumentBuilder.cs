using System;
using System.Collections.Generic;

namespace FileScanner.SearchSummary
{
    public enum TextStyle {
        Normal,
        Bold
    }

	public interface IDocumentBuilder
	{
        void AddReportHeader(DateTime? generationTime,
                             String userQuery,
                             IEnumerable<String> searchedLocations);
        void AddReportFooter();

        void AddSectionHeader(string text);
        void AddSearchResult(SearchResult result);
        void AddText(string text,
                     TextStyle style = TextStyle.Normal);

        void Save(string filePath);
	}
}

