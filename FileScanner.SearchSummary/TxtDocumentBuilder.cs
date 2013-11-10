using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.SearchSummary
{
    class TxtDocumentBuilder : IDocumentBuilder
    {
        private StringBuilder content;

        public void AddReportHeader(DateTime generationTime)
        {
               
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
