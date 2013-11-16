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
        public void AddReportHeader_Add_Example_Raport_Header()
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
                "\r\nPrzeszukiwane lokalizacje:\r\n    /jakas/sciezka\r\n    ./inna/sciezka\r\n" +
                "\r\nRaport został wygenerowany dnia: 2012-09-07\r\n\r\n" +
                "-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ," + "\r\n" +
                " )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (" + "\r\n" +
                " (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )" + "\r\n" +
                "  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'" + "\r\n" +
                "\r\n\r\n", documentBuilder.getCurrentContent());
        }

        [TestMethod]
        public void AddSectionHeader_Add_Example_Section_Header()
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
        
        [TestMethod]
        public void AddSearchResult_Some_Field_Equal_Null()
        {
            SearchResult searchResult = new SearchResult("SomeFile", null, null, new DateTime(2012, 03, 04), new DateTime(2012, 05, 7));
            documentBuilder.AddSearchResult(searchResult);
            Assert.AreEqual("   * Nazwa:          \tSomeFile\r\n" +
                            "   * Ostatni dost.:  \t2012-03-04 00:00:00\r\n" +
                            "   * Ostatnia mod.:  \t2012-05-07 00:00:00\r\n", documentBuilder.getCurrentContent());
        }
        

        [TestMethod]
        public void Save_File_Creation()
        {
            var filePath = "./testFile.txt";
            documentBuilder.AddText("Some Text");
            documentBuilder.Save(filePath);
            var text = File.ReadAllText(filePath);
            Assert.AreEqual("Some Text\r\n", text);
            File.Delete(filePath);   
        }

        [TestMethod]
        public void TxtDocumentBuilder_Comprehensive_Test()
        {
            documentBuilder.AddReportHeader(new DateTime(2012,02,02),"Butelka z piwa", 
                new List<String>(){"C:/glowny_folder/troche/dalej","C:/jakis/inny/folder"});
            documentBuilder.AddSectionHeader("Plik numer 1");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/FajneWykopaliska.txt", new DateTime(2011, 02, 01), null, null));
            documentBuilder.AddSectionHeader("Plik numer 2");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/InneWykopaliska.txt", new DateTime(2012, 01, 01), null, null));
            documentBuilder.AddSectionHeader("Plik numer 3");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
               "C:/glowny_folder/troche/dalej/JeszczeInneWykopaliska.txt", new DateTime(2010, 01, 01), null, null));
            documentBuilder.AddReaportFooter();
            String filePath = "./result.txt";
            documentBuilder.Save(filePath);
            var text = File.ReadAllText(filePath);
            Assert.AreEqual(@"
Raport z wyszukiwania

Wyszukiwane frazy:
    Butelka z piwa
Przeszukiwane lokalizacje:
    C:/glowny_folder/troche/dalej
    C:/jakis/inny/folder

Raport został wygenerowany dnia: 2012-02-02

-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,
 )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (
 (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )
  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'


  Plik numer 1
----------------------------------------
   * Nazwa:          	Wykopaliska
   * Ścieżka:        	C:/glowny_folder/troche/dalej/FajneWykopaliska.txt
   * Data utworzenia:	2011-02-01 00:00:00
  Plik numer 2
----------------------------------------
   * Nazwa:          	Wykopaliska
   * Ścieżka:        	C:/glowny_folder/troche/dalej/InneWykopaliska.txt
   * Data utworzenia:	2012-01-01 00:00:00
  Plik numer 3
----------------------------------------
   * Nazwa:          	Wykopaliska
   * Ścieżka:        	C:/glowny_folder/troche/dalej/JeszczeInneWykopaliska.txt
   * Data utworzenia:	2010-01-01 00:00:00



      /'^'\                                  /'^'\
     ( o o )                                ( o o )
-oOOO--(_)--OOOo------------------------oOOO--(_)--OOOo----
             	Technologie Obiektowe II
                   Grupa Wtorek 9:30
  .oooO       Akademia Gorniczo Hutnicza    oooO
  (   )   Oooo.                             (   )   Oooo
---\ (----(  )------------------------------\ (----(  )-----
    \_)    ) /                               \_)   ) /
          (_/                                     (_/
", text);
            File.Delete(filePath);
        }        
    }
}
