using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileScanner.SearchSummary
{
    public class TxtDocumentBuilder : IDocumentBuilder
    {
        public StringBuilder content = new StringBuilder();

        public String getCurrentContent()
        {
            return content.ToString();
        }

        public void AddReportHeader(Nullable<DateTime> generationTime,
                                    String userQuery,
                                    IEnumerable<String> searchedLocations)
        {
            content.Append("\r\nSearch Report\r\n\r\n");

            if (generationTime.HasValue)
            {
                content.Append("Generation date: ")
                       .Append(generationTime.Value.ToShortDateString())
                       .Append("\r\n");
            }

            if (userQuery != null)
            {
                content.Append("Searched phrases:\r\n    " + userQuery);
            }

            if (searchedLocations != null)
            {
                content.Append("\r\nSearched locations:\r\n");
               
                foreach (String location in searchedLocations)
                {
                    content.Append("    " + location + "\r\n");
                }
            }

            content.Append("\r\n" +
                           "-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ," + "\r\n" +
                           " )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) ("  + "\r\n" +
                           " (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )" + "\r\n" +
                           "  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'"  + "\r\n" +
                           "\r\n\r\n");
        }

        public void AddSectionHeader(string text)
        {
            content.Append("  " + text + "\r\n" +
               "----------------------------------------\r\n");
        }

        public void AddText(string text, TextStyle style = TextStyle.Normal)
        {
            if (style == TextStyle.Bold)
                content.Append("***");

            content.Append(text);

            if (style == TextStyle.Bold)
                content.Append("***");
        }

        public void AddSearchResult(SearchResult result)
        {
            if (!result.Equals(null))
            {
                Append("File name:     \t", result.fileName);
                Append("File path:        \t", result.fullFilePath);
                Append("Size (bytes):\t", result.fileSizeBytes);
                Append("Creation date:\t", result.dateCreated);
                Append("Last access:  \t", result.dateLastAccess);
                Append("Last mod.:  \t", result.dateLastModified);
                content.Append("\r\n");
            }
        }

        private void Append(String prefix, Object field){
            if (field != null)
            {
                content.Append("   * "+prefix+field.ToString()+"\r\n");
            }
        }

        public void AddReportFooter()
        {
            content.Append("\r\n\r\n\r\n" +
"      /'^'\\                                  /'^'\\" + "\r\n" +
"     ( o o )                                ( o o )" + "\r\n" +
"-oOOO--(_)--OOOo------------------------oOOO--(_)--OOOo----" + "\r\n" +
"             	Object-oriented technology II" + "\r\n" +
"                   Tuesday 9:30" + "\r\n" +
"  .oooO         AGH University of Science  oooO" + "\r\n" +
"  (   )   Oooo.       and Technology       (   )   Oooo" + "\r\n" +
"---\\ (----(  )------------------------------\\ (----(  )-----" + "\r\n" +
"    \\_)    ) /                               \\_)    ) /" + "\r\n" +
"          (_/                                      (_/" + "\r\n");
        }

        public void Save(string filePath)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            writer.Write(this.content);
            writer.Flush();
            writer.Close();
        }

        void IDocumentBuilder.BeginContextBlock()
        {
        }

        void IDocumentBuilder.EndContextBlock()
        {
        }

        void IDocumentBuilder.AddContextText(string text, TextStyle style)
        {
        }
    }
}
