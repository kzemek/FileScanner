using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.RtfRendering;



namespace FileScanner.SearchSummary
{
    public class PDFDocumentBuilder : IDocumentBuilder
    {
        Document document;
        PdfDocumentRenderer pdfRenderer;
        Section section;
        Paragraph contextParagraph;

        int counter = 0 ; 

        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";

            style = document.Styles["Heading1"];
            style.Font.Size = 18;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
            style.Document.FootnoteLocation = FootnoteLocation.BottomOfPage;
        }

        public PDFDocumentBuilder()
        {
            document = new Document();
            const bool unicode = true;
            pdfRenderer = new PdfDocumentRenderer(unicode);
            document.Info.Title = "Raport z wyszukiwania";
            document.Info.Author = "Zajęcia projektowe z TO grupa 9:30 Wtorek";
            DefineStyles(document);
            pdfRenderer.Document = document;
            section = document.AddSection();
        }

        public void AddReportHeader(DateTime? generationTime, string userQuery, IEnumerable<string> searchedLocations)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Style = "Heading1";
            FormattedText ft = paragraph.AddFormattedText("Raport z wyszukiwania",TextFormat.Bold);
            
            if (generationTime.HasValue)
            {
                paragraph = section.AddParagraph("\nRaport wygenerowano dnia: " + generationTime.Value.ToShortDateString());
            }

            if (userQuery != null)
            {
                paragraph = section.AddParagraph("\nWyszukiwane frazy:    " + userQuery);
            }

            if (searchedLocations != null)
            {
                paragraph = section.AddParagraph("\nPrzeszukiwane lokalizacje:");
                foreach (String location in searchedLocations)
                {
                    paragraph = section.AddParagraph("\t" + location);
                }
            }
            section.AddParagraph("\n\n\n");
            paragraph.Style = "Normal";

        }

        public void AddReportFooter()
        {
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Technologie Obiektowe II\n" + "Grupa Wtorek 9:30\n" + "Akademia Gorniczo Hutnicza\n");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        public void AddSectionHeader(string text)
        {
            counter++;
            this.section.AddParagraph("Plik numer " + counter + "\n");
        }

        public void AddSearchResult(SearchResult result)
        {
            AddSectionHeader("");
            String content = "";
            if (!result.Equals(null))
            {
                content += "\n";
                content += Append("Nazwa:\t\t", result.fileName);
                content += Append("Ścieżka:\t\t", result.fullFilePath);
                content += Append("Rozmiar (bajty):\t", result.fileSizeBytes);
                content += Append("Data utworzenia:\t", result.dateCreated);
                content += Append("Ostatni dost.:\t", result.dateLastAccess);
                content += Append("Ostatnia mod.:\t", result.dateLastModified);
                content += "\n";
            }
            section.AddParagraph(content);
        }

        private String Append(String prefix, Object field)
        {
            if (field != null)
            {
                return  prefix + field.ToString() + "\n";
            }
            return null;
        }

        public void Save(string filePath)
        {
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filePath);
        }

        public void AddText(string text, TextStyle style = TextStyle.Normal)
        {
            if(style==TextStyle.Bold)
            {
            }
            contextParagraph.AddText(text);

        }

        public void BeginContextBlock()
        {
            contextParagraph = section.AddParagraph();
        }
        
        public void EndContextBlock()
        {
            contextParagraph = null;
        }

        public void AddContextText(string text, TextStyle style = TextStyle.Normal)
        {
        }
    }
}
