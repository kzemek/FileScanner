using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileScanner.SearchSummary;
using System.Collections.Generic;
using System.IO;
namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class TxtDocumentBuilderTest
    {

        TxtDocumentBuilder documentBuilder = new TxtDocumentBuilder();
        
        [TestInitialize]
        public void Setup()
        {
            documentBuilder = new TxtDocumentBuilder();
        }

        [TestMethod]
        public void AddReportHeaderTest()
        {

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
                "\r\n\r\n", documentBuilder.getCurrentContent());
        }

        [TestMethod]
        public void AddSectionHeader()
        {
            documentBuilder.AddSectionHeader("ExampleText");
            Assert.AreEqual("  ExampleText\r\n----------------------------------------\r\n", documentBuilder.getCurrentContent());
        }

        [TestMethod]
        public void AddSearchResult_All_Fields_Not_Null()
        {
            SearchResult searchResult = new SearchResult("SomeFile","C:/Directory/AnotherDirecory/SomeFile.txt",
                new DateTime(2012,02,13),new DateTime(2012,03,04),new DateTime(2012,05,7));
            documentBuilder.AddSearchResult(searchResult);
            Assert.AreEqual("   * Nazwa:          \tSomeFile\r\n" +
                            "   * Ścieżka:        \tC:/Directory/AnotherDirecory/SomeFile.txt\r\n" +
                            "   * Data utworzenia:\t2012-02-13 00:00:00\r\n" +
                            "   * Ostatni dost.:  \t2012-03-04 00:00:00\r\n" +
                            "   * Ostatnia mod.:  \t2012-05-07 00:00:00\r\n", documentBuilder.getCurrentContent());
        }
        /*
        [TestMethod]
        public void AddSearchResult_Some_Field_Equal_Null()
        {
            DateTime dt = null;
            SearchResult searchResult = new SearchResult("SomeFile", null,new DateTime(2012, 02, 13), new DateTime(2012, 03, 04), new DateTime(2012, 05, 7));
            documentBuilder.AddSearchResult(searchResult);
            Assert.AreEqual("   * Nazwa:          \tSomeFile\r\n" +
                            "   * Ostatni dost.:  \t2012-03-04 00:00:00\r\n" +
                            "   * Ostatnia mod.:  \t2012-05-07 00:00:00\r\n", documentBuilder.getCurrentContent());
        }
        */
        [TestMethod]
        public void Save_FileCreation()
        {
            var filePath = @".\testFile.txt";
            documentBuilder.Save(filePath);
            var text = File.ReadAllText(filePath);
            Assert.AreEqual("",text);
            File.Delete(filePath);   
        }

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
