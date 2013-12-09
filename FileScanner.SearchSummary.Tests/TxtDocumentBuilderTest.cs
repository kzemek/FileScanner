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

        TxtDocumentBuilder documentBuilder;
        
        [TestInitialize]
        public void Setup()
        {
            documentBuilder = new TxtDocumentBuilder();
        }

        [TestMethod]
        public void AddReportHeader_Add_Example_Report_Header()
        {

            List<String> paths = new List<string>()
            {
                "/jakas/sciezka",
                "./inna/sciezka"
            };

            documentBuilder.AddReportHeader(new DateTime(2012, 09, 07), "idzie wojna idzie wojna", paths);
            Assert.AreEqual("\r\nSearch Report\r\n\r\n" +
                "Generation date: 2012-09-07\r\n" +
                "Searched phrases:\r\n    " +
                "idzie wojna idzie wojna\r\n" +
                "Searched locations:\r\n    /jakas/sciezka\r\n    ./inna/sciezka\r\n\r\n" +
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
                666,new DateTime(2012,02,13),new DateTime(2012,03,04),new DateTime(2012,05,7));
            documentBuilder.AddSearchResult(searchResult);
            Assert.AreEqual("   * File name:     \tSomeFile\r\n" +
                            "   * File path:        \tC:/Directory/AnotherDirecory/SomeFile.txt\r\n" +
                            "   * Size (bytes):\t666\r\n" +
                            "   * Creation date:\t2012-02-13 00:00:00\r\n" +
                            "   * Last access:  \t2012-03-04 00:00:00\r\n" +
                            "   * Last mod.:  \t2012-05-07 00:00:00\r\n\r\n", documentBuilder.getCurrentContent());
        }
        
        [TestMethod]
        public void AddSearchResult_Some_Field_Equal_Null()
        {
            SearchResult searchResult = new SearchResult("SomeFile", null, null, null, new DateTime(2012, 03, 04), new DateTime(2012, 05, 7));
            documentBuilder.AddSearchResult(searchResult);
            Assert.AreEqual("   * File name:     \tSomeFile\r\n" +
                            "   * Last access:  \t2012-03-04 00:00:00\r\n" +
                            "   * Last mod.:  \t2012-05-07 00:00:00\r\n\r\n", documentBuilder.getCurrentContent());
        }
        

        [TestMethod]
        public void Save_File_Creation()
        {
            var filePath = "./testFile.txt";
            documentBuilder.AddText("Some Text");
            documentBuilder.Save(filePath);
            var text = File.ReadAllText(filePath);
            Assert.AreEqual("Some Text", text);
            File.Delete(filePath);   
        }

        [TestMethod]
        public void TxtDocumentBuilder_Comprehensive_Test()
        {
            documentBuilder.AddReportHeader(new DateTime(2012,02,02),"Butelka z piwa", 
                new List<String>(){"C:/glowny_folder/troche/dalej","C:/jakis/inny/folder"});
            documentBuilder.AddSectionHeader("Plik numer 1");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/FajneWykopaliska.txt", 10, new DateTime(2011, 02, 01), null, null));
            documentBuilder.AddSectionHeader("Plik numer 2");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/InneWykopaliska.txt", 20, new DateTime(2012, 01, 01), null, null));
            documentBuilder.AddSectionHeader("Plik numer 3");
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
               "C:/glowny_folder/troche/dalej/JeszczeInneWykopaliska.txt", 30, new DateTime(2010, 01, 01), null, null));
            documentBuilder.AddReportFooter();
            String filePath = "./result.txt";
            documentBuilder.Save(filePath);
            var text = File.ReadAllText(filePath);
            Assert.AreEqual("\r\n" +
                "Search Report\r\n" +
                "\r\n" +
                "Generation date: 2012-02-02\r\n" +
                "Searched phrases:\r\n" +
                "    Butelka z piwa\r\n" +
                "Searched locations:\r\n" +
                "    C:/glowny_folder/troche/dalej\r\n" +
                "    C:/jakis/inny/folder\r\n" +
                "\r\n" +
                "-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,\r\n" +
                " )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (\r\n" +
                " (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )\r\n" +
                "  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'\r\n" +
                "\r\n" +
                "\r\n" +
                "  Plik numer 1\r\n" +
                "----------------------------------------\r\n" +
                "   * File name:     \tWykopaliska\r\n" +
                "   * File path:        \tC:/glowny_folder/troche/dalej/FajneWykopaliska.txt\r\n" +
                "   * Size (bytes):\t10\r\n" +
                "   * Creation date:\t2011-02-01 00:00:00\r\n" +
                "\r\n" +
                "  Plik numer 2\r\n" +
                "----------------------------------------\r\n" +
                "   * File name:     \tWykopaliska\r\n" +
                "   * File path:        \tC:/glowny_folder/troche/dalej/InneWykopaliska.txt\r\n" +
                "   * Size (bytes):\t20\r\n" +
                "   * Creation date:\t2012-01-01 00:00:00\r\n" +
                "\r\n" +
                "  Plik numer 3\r\n" +
                "----------------------------------------\r\n" +
                "   * File name:     \tWykopaliska\r\n" +
                "   * File path:        \tC:/glowny_folder/troche/dalej/JeszczeInneWykopaliska.txt\r\n" +
                "   * Size (bytes):\t30\r\n" +
                "   * Creation date:\t2010-01-01 00:00:00\r\n" +
                "\r\n" +
                "\r\n" +
                "\r\n" +
                "\r\n" +
                @"      /'^'\                                  /'^'\" + "\r\n" +
                @"     ( o o )                                ( o o )" + "\r\n" +
                @"-oOOO--(_)--OOOo------------------------oOOO--(_)--OOOo----" + "\r\n" +
                @"             	Object-oriented technology II" + "\r\n" +
                @"                   Tuesday 9:30" + "\r\n" +
                @"  .oooO         AGH University of Science  oooO" + "\r\n" +
                @"  (   )   Oooo.       and Technology       (   )   Oooo" + "\r\n" +
                @"---\ (----(  )------------------------------\ (----(  )-----" + "\r\n" +
                @"    \_)    ) /                               \_)    ) /" + "\r\n" +
                @"          (_/                                      (_/" + "\r\n",
                text);
            Console.Write(documentBuilder.content);
            File.Delete(filePath);
        }        
    }
}
