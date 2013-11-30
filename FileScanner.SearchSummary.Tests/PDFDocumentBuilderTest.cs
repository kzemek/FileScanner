using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
        public void AddReportHeader()
        {
            documentBuilder.AddReportHeader(new DateTime(2012, 09, 07), "Asd", null);
            documentBuilder.Save("C:/Users/blost/Desktop/Downloads/SomePDF3.pdf");
        }

    //  [TestMethod]
    //    public void Save_PDF_File_Creation()
    //    {
    //        documentBuilder.Save("C:/Users/blost/Desktop/Downloads/SomePDF.pdf");
    //    }
    }
}
