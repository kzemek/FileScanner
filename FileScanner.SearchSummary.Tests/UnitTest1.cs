using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileScanner.SearchSummary;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTxtDocumentBuilder()
        {
            TxtDocumentBuilder documentBuilder = new TxtDocumentBuilder();
            documentBuilder.AddReportHeader(DateTime.Now);
            documentBuilder.AddSectionHeader("Plik numer 1");
            SearchResult sr = new SearchResult();
            sr.fileName = "Wykopaliska";
            sr.fullFilePath = "C:/Doc/Deskt/Wykopaliska";
            sr.dateLastModified = DateTime.Now;
            sr.dateLastAccess = DateTime.Now;
            documentBuilder.AddSearchResult(sr);
            documentBuilder.AddSectionHeader("Plik numer 2");
            documentBuilder.AddSearchResult(sr);
            documentBuilder.AddSectionHeader("Plik numer 3");
            documentBuilder.AddSearchResult(sr);
            documentBuilder.AddReaportFooter();
            documentBuilder.Save("C:\\Users\\blost\\Desktop\\example.txt");
            documentBuilder.SeePartialResults();
        }
    }
}
