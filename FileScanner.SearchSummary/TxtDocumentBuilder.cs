﻿using System;
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

        public void AddReportHeader(DateTime generationTime,
                                    String userQuery,
                                    IEnumerable<String> searchedLocations)
        {
            StringBuilder directories = new StringBuilder();
            foreach(String location in searchedLocations)
            {
                directories.Append("    "+location+"\r\n");
            }
            content.Append("\r\nRaport z wyszukiwania\r\n\r\n" +
                "Wyszukiwane frazy:\r\n    " +
                userQuery+
                "\r\nPrzeszukiwane lokalizacje:\r\n" + directories.ToString()+
                "\r\nRaport został wygenerowany dnia: " + generationTime.ToShortDateString() + "\r\n\r\n" +
                "-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ,-.   ," + "\r\n" +
                " )  (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (" + "\r\n" +
                " (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   ) (   )" + "\r\n" +
                "  `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'   `-'" + "\r\n" +
                "\r\n\r\n");
        }

        public void AddSectionHeader(string text)
        {
            content.Append("  " + text + "\r\n" +
               "----------------------------------------\r\n");
        }

        public void AddText(string text)
        {
            content.Append(text + "\r\n");
   
        }

        public void AddSearchResult(SearchResult result)
        {
            if (!result.Equals(null))
            {
               Append("Nazwa:          \t", result.fileName);
               Append("Ścieżka:        \t", result.fullFilePath);
               Append("Data utworzenia:\t", result.dateCreated);
               Append("Ostatni dost.:  \t", result.dateLastAccess);
               Append("Ostatnia mod.:  \t", result.dateLastModified); 
            }
        }

        private void Append(String prefix, Object field){
            if (field != null)
            {
                content.Append("   * "+prefix+field.ToString()+"\r\n");
            }
        }

        public void AddReaportFooter()
        {
            content.Append("\r\n\r\n\r\n" +
"      /'^'\\                                  /'^'\\" + "\r\n" +
"     ( o o )                                ( o o )" + "\r\n" +
"-oOOO--(_)--OOOo------------------------oOOO--(_)--OOOo----" + "\r\n" +
"             	Technologie Obiektowe II" + "\r\n" +
"                   Grupa Wtorek 9:30" + "\r\n" +
"  .oooO       Akademia Gorniczo Hutnicza    oooO" + "\r\n" +
"  (   )   Oooo.                             (   )   Oooo" + "\r\n" +
"---\\ (----(  )------------------------------\\ (----(  )-----" + "\r\n" +
"    \\_)    ) /                               \\_)   ) /" + "\r\n" +
"          (_/                                     (_/" + "\r\n");
        }

        public void Save(string filePath)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            writer.Write(this.content);
            writer.Flush();
            writer.Close();
        }

    }
}