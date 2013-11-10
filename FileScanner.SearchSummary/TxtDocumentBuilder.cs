using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    class TxtDocumentBuilder : IDocumentBuilder
    {
        private StringBuilder content = new StringBuilder();

        public void AddReportHeader(DateTime generationTime)
        {
                  content.Append("\nRaport z wyszukiwania fraz <tutaj frazy> w katalogach <>" +
                                   "Raport został wygenerowany dnia: " + generationTime.ToShortDateString() );
        }

        public void AddSectionHeader(string text)
        {
            throw new NotImplementedException();
        }

        public void AddText(string text)
        {
            throw new NotImplementedException();
        }

        public void AddSearchResult(SearchResult result)
        {
            throw new NotImplementedException();
        }

        public void Save(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
