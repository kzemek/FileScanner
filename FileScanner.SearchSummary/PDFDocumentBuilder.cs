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
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            style.ParagraphFormat.SpaceBefore = 3;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.ParagraphFormat.SpaceBefore = 2;
            style.ParagraphFormat.SpaceAfter = 2;

            style.Font.Bold = true;
            document.Styles.AddStyle("Bold", "Normal");
            style = document.Styles["Bold"];
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.Font.Color = Colors.MediumVioletRed;

            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Borders.Width = 1.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;
            style.ParagraphFormat.KeepTogether = true;

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
            document.Info.Title = "Search results";
            document.Info.Author = "Zajęcia projektowe z TO grupa 9:30 Wtorek";
            DefineStyles(document);
            pdfRenderer.Document = document;
            section = document.AddSection();
        }

        public void AddReportHeader(DateTime? generationTime, string userQuery, IEnumerable<string> searchedLocations)
        {
            Paragraph paragraph = section.AddParagraph();
            paragraph.Style = "Heading1";
            paragraph.AddText("Search Report");

            paragraph = section.AddParagraph();
            paragraph.Style = "Heading2";
            paragraph.AddText("\nKey information\n");
            
            if (generationTime.HasValue)
            {
                paragraph = section.AddParagraph("Generation date: " + generationTime.Value.ToShortDateString());
                paragraph.Style = "Heading3";
            }

            if (userQuery != null)
            {
                paragraph = section.AddParagraph("Searched phrases:    " + userQuery);
                paragraph.Style = "Heading3";
            }

            if (searchedLocations != null)
            {
                paragraph = section.AddParagraph("Searched locations:");
                paragraph.Style = "Heading3";
                foreach (String location in searchedLocations)
                {
                    paragraph = section.AddParagraph("\t" + location);
                }
            }
            section.AddParagraph("\n\n");
            paragraph.Style = "Normal";

        }

        public void AddReportFooter()
        {
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Object-oriented technology II\n" + "Tuesday 9:30\n" + "AGH University of Science and Technology\n");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        public void AddSectionHeader(string text)
        {
            counter++;
            Paragraph paragraph = section.AddParagraph(text+"\n");
            paragraph.Style = "Heading2";
        }

        public void AddSearchResult(SearchResult result)
        {
            AddSectionHeader(result.fileName);
            String content = "";
            if (!result.Equals(null))
            {
                content += "\n";
                content += Append("File path:   \t", result.fullFilePath);
                content += Append("Size (bytes):\t", result.fileSizeBytes);
                content += Append("Creation date:\t", result.dateCreated);
                content += Append("Last access:\t", result.dateLastAccess);
                content += Append("Last mod.:\t", result.dateLastModified);
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

        public void AddContextText(string text, TextStyle style = TextStyle.Normal)
        {
             
            if (style == TextStyle.Bold)
            {
                contextParagraph.AddFormattedText(text, document.Styles["Bold"].Font);
            }
            else
            {
                contextParagraph.AddText(text);
            }

        }

        public void BeginContextBlock()
        {
            section.AddParagraph("Context:\n\n");
            contextParagraph = section.AddParagraph();
            contextParagraph.Style = "TextBox";
        }
        
        public void EndContextBlock()
        {
            section.AddParagraph("\n\n");
            contextParagraph = null;
        }
    }
}
