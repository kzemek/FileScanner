using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileScanner.SearchSummary;
using System.Collections.Generic;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class TxtDocumentBuilderTest
    {

        [TestMethod]
        public void AddReportHeaderTest()
        {

            TxtDocumentBuilder documentBuilder = new TxtDocumentBuilder();
            List<String> paths = new List<string>()
            {
                "/jakas/sciezka",
                "./inna/sciezka"
            };

            documentBuilder.AddReportHeader(new DateTime(2012, 09, 07), "idzie wojna idzie wojna", paths);
            Assert.AreEqual("\r\nRaport z wyszukiwania\r\n\r\n" +
                "Wyszukiwane frazy:\r\n    " +
                "idzie wojna idzie wojna" +
                "\r\nPrzeszukiwane katalogi:\r\n    /jakas/sciezka\r\n    ./inna/sciezka\r\n" +
                "\r\nRaport został wygenerowany dnia: 2012-09-07\r\n\r\n" +
                "-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ," + "\r\n" +
                " )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (" + "\r\n" +
                " (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )" + "\r\n" +
                "  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'" + "\r\n" +
                "\r\n\r\n", documentBuilder.getContent());
        }

        //[TestMethod]
        //public void  
            /*
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
             */ 
        
    }
}
