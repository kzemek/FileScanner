using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace FileScanner.SearchSummary
{
    public class PDFDocumentBuilder : IDocumentBuilder
    {
        PdfDocument document = new PdfDocument();
        XGraphics gfx;
        PdfPage page;
        XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            
        public PDFDocumentBuilder()
        {
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            document.Info.Title = "FileScanner Raport";
        }

        public void AddReportHeader(DateTime? generationTime, string userQuery, IEnumerable<string> searchedLocations)
        {
            gfx.DrawString("Raport z wyszukiwania", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height/9),
                XStringFormats.Center);
            var font2 = new XFont("Verdana", 10, XFontStyle.BoldItalic);
            String str = "Data generacji: " + generationTime.Value.ToShortDateString();
            gfx.DrawString(str, font2, XBrushes.Black,
                new XRect(410, 20, 0, 0),
                XStringFormats.Default);
            var str2 = "Wyszukiwane frazy: " + userQuery;
            gfx.DrawString(str2, font2, XBrushes.Black,
                new XRect(30, 100, 0 , 0),
                XStringFormats.Default);
        }

        public void AddReportFooter()
        {
            throw new NotImplementedException();
        }

        public void AddSectionHeader(string text)
        {
            throw new NotImplementedException();
        }

        public void AddSearchResult(SearchResult result)
        {
            throw new NotImplementedException();
        }

        public void Save(string filePath)
        {
            document.Save(filePath);
            Process.Start(filePath);
        }
    }
}
