using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class PDFDocumentBuilderTest
    {
        PDFDocumentBuilder documentBuilder;

        [TestInitialize]
        public void Setup()
        {
            documentBuilder = new PDFDocumentBuilder();
        }

        [TestMethod]
        public void SaveIfFileIsCreatedAndCouldBeOpened()
        {
            documentBuilder.AddReportHeader(new DateTime(2012, 02, 02), "Butelka z piwa",
               new List<String>() { "C:/glowny_folder/troche/dalej", "C:/jakis/inny/folder" });
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/FajneWykopaliska.txt", 10, new DateTime(2011, 02, 01), null, null));
            documentBuilder.AddContextText("jakis przykladowy tekst który moze sie wypisac a moze i nie");
            documentBuilder.AddContextText("a ten tekst powinien zostac wyswietlony boldem....", TextStyle.Bold);
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/InneWykopaliska.txt", 20, new DateTime(2012, 01, 01), null, null));
            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
               "C:/glowny_folder/troche/dalej/JeszczeInneWykopaliska.txt", 30, new DateTime(2010, 01, 01), null, null));
            documentBuilder.AddReportFooter();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "SomePDF2.pdf");
            FileInfo info = new FileInfo(path);

            if (info.Exists)
            {
                info.Delete();
                info.Refresh();
            }

            Assert.IsFalse(info.Exists);
            documentBuilder.Save(path);

            info.Refresh();
            Assert.IsTrue(info.Exists);
        }
    }
}
