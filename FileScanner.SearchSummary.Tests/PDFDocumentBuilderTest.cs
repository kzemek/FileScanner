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
            documentBuilder.AddReportHeader(new DateTime(2012, 02, 02), "Gatunek Sahara",
               new List<String>() { "C:/glowny_folder/troche/dalej", "C:/jakis/inny/folder" });

            documentBuilder.AddSearchResult(new SearchResult("Wykopaliska",
                "C:/glowny_folder/troche/dalej/Wykopaliska.txt", 10, new DateTime(2011, 02, 01), null, null));
            documentBuilder.BeginContextBlock();
            documentBuilder.AddContextText(@"Słoń afrykański (Loxodonta africana) – gatunek ssaka z rodziny słoniowatych,"+
                " największy ze współcześnie żyjących gatunków ssaków lądowych. Wcześniej uznawany jako jeden ");
            documentBuilder.AddContextText(@"gatunek ",TextStyle.Bold); 
            documentBuilder.AddContextText(@"wraz z afrykańskim słoniem leśnym (Loxodonta cyclotis)."+
                " Zwierzę stadne, zamieszkuje afrykańską sawannę, lasy i stepy od południowych krańców "); 
            documentBuilder.AddContextText(@"Sahary ",TextStyle.Bold); 
            documentBuilder.AddContextText(@"po Namibię, północną Botswanę i północną część Afryki."+
                " W starożytności wykorzystywane jako zwierzęta bojowe.");
            documentBuilder.EndContextBlock();

            documentBuilder.AddSearchResult(new SearchResult("Świebodzianka",
                "C:/glowny_folder/troche/dalej/Świebodzianka.txt", 20, new DateTime(2012, 01, 01), null, null));
            documentBuilder.BeginContextBlock();
            documentBuilder.AddContextText(@"Sahara",TextStyle.Bold);
            documentBuilder.AddContextText(@" – strefa pustynna położona w północnej Afryce. Jest ona największą gorącą pustynią na "+
            "Ziemi (ma 9 064 300 km²), rozciągająca się na długości 5700 km od Oceanu"+
            " Atlantyckiego na zachodzie po Morze Czerwone na wschodzie; od północy ograniczona jest górami Atlas i wybrzeżem Morza Śródziemnego"+
            " Znajduje się na terytoriach 11 państw: Maroka, Algierii, Tunezji, Libii, Egiptu, Sahary Zachodniej, Mauretanii, Mali, Nigru, Czadu i Sudanu.");
            documentBuilder.EndContextBlock();

            documentBuilder.AddSearchResult(new SearchResult("Jajko_W_Płynie",
               "C:/glowny_folder/troche/dalej/Jajko_W_Płynie.txt", 30, new DateTime(2010, 01, 01), null, null));
            documentBuilder.BeginContextBlock();
            documentBuilder.AddContextText("Określił go jako grupę osobników pochodzących od podobnych do siebie rodziców[4]. Karol Linneusz sformalizował ");
            documentBuilder.AddContextText("gatunek", TextStyle.Bold);
            documentBuilder.AddContextText("jako jedną z pięciu podstawowych kategorii systematycznych. " +
                "Zdefiniował gatunek jako zbiór osobników podobnych do siebie w taki sposób, w jaki potomstwo podobne jest do rodziców[3]. " +
                "Wprowadził diagnozy gatunku oraz upowszechnił zasady jego nazewnictwa.");
            documentBuilder.EndContextBlock();

            documentBuilder.AddSearchResult(new SearchResult("PlażaDzikaPlaża",
                "C:/glowny_folder/troche/dalej/PlażaDzikaPlaża.txt", 10, new DateTime(2011, 02, 01), null, null));
            documentBuilder.BeginContextBlock();
            documentBuilder.AddContextText(@"Słoń afrykański (Loxodonta africana) – gatunek ssaka z rodziny słoniowatych," +
                " największy ze współcześnie żyjących gatunków ssaków lądowych. Wcześniej uznawany jako jeden ");
            documentBuilder.AddContextText(@"gatunek ", TextStyle.Bold);
            documentBuilder.AddContextText(@"wraz z afrykańskim słoniem leśnym (Loxodonta cyclotis)." +
                " Zwierzę stadne, zamieszkuje afrykańską sawannę, lasy i stepy od południowych krańców ");
            documentBuilder.AddContextText(@"Sahary ", TextStyle.Bold);
            documentBuilder.AddContextText(@"po Namibię, północną Botswanę i północną część Afryki." +
                " W starożytności wykorzystywane jako zwierzęta bojowe.");
            documentBuilder.EndContextBlock();

            documentBuilder.AddSearchResult(new SearchResult("SkórkaZPomarańczy",
                "C:/glowny_folder/troche/dalej/SkórkaZPomarańczy.txt", 20, new DateTime(2012, 01, 01), null, null));
            documentBuilder.BeginContextBlock();
            documentBuilder.AddContextText(@"Sahara", TextStyle.Bold);
            documentBuilder.AddContextText(@" – strefa pustynna położona w północnej Afryce. Jest ona największą gorącą pustynią na " +
            "Ziemi (ma 9 064 300 km²), rozciągająca się na długości 5700 km od Oceanu" +
            " Atlantyckiego na zachodzie po Morze Czerwone na wschodzie; od północy ograniczona jest górami Atlas i wybrzeżem Morza Śródziemnego" +
            " Znajduje się na terytoriach 11 państw: Maroka, Algierii, Tunezji, Libii, Egiptu, Sahary Zachodniej, Mauretanii, Mali, Nigru, Czadu i Sudanu.");
            documentBuilder.EndContextBlock();

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
