using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using NMock.Matchers;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class SummaryGeneratorTest
    {
        MockFactory mockFactory;
        FileInfo emptyFileInfo;
        bool emptyFileExisted;

        string searchQuery;
        string outputFilename;
        List<String> inputPaths;
        List<MatchingFile> matchingFiles;
        
        [TestInitialize]
        public void Setup()
        {
            byte[] testStringBytes = Encoding.UTF8.GetBytes(
               "* Słoń afrykański (Loxodonta africana) – gatunek ssaka z rodziny słoniowatych, największy " +
               "ze współcześnie żyjących gatunków ssaków lądowych. Wcześniej uznawany jako jeden gatunek " +
               "wraz z afrykańskim słoniem leśnym (Loxodonta cyclotis). Zwierzę stadne, zamieszkuje " +
               "afrykańską sawannę, lasy i stepy od południowych krańców Sahary po Namibię, północną " +
               "Botswanę i północną część Afryki. W starożytności wykorzystywane jako zwierzęta bojowe.");

            mockFactory = new MockFactory();
            emptyFileInfo = new FileInfo("someTestFile.txt");
            emptyFileExisted = emptyFileInfo.Exists;
            if (!emptyFileExisted)
                emptyFileInfo.Create();

            searchQuery = "foo bar baz";
            outputFilename = "testOutput";
            inputPaths = new List<string> {
                @"c:\some_folder",
                @"c:\other_folder\file.txt"
            };

            matchingFiles = new List<MatchingFile>();
            for (int i = 0; i < 5; ++i)
            {
                MatchingFile file;
                file.fileInfo = emptyFileInfo;
                file.fileReader = new StreamReader(new MemoryStream(testStringBytes));
                file.searchResults = new Dictionary<string, IEnumerable<int>> { { "slon", new List<int> { 2, 65, 198 } } };
                file.accuracy = (float)i;
                matchingFiles.Add(file);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (!emptyFileExisted)
                emptyFileInfo.Delete();
        }

        [TestMethod]
        public void GroupMatchPositions()
        {
            IDictionary<string, IEnumerable<int>> matchPositions =
                new Dictionary<string, IEnumerable<int>> {
                    { "ala", new List<int> { 0, 20, 100 } },
                    { "ma", new List<int> { 10, 30 } },
                    { "kota", new List<int> { 5, 40, 80 } }
                };

            List<PositionTextPairGroup> groups = SummaryGenerator.GroupMatchPositions(matchPositions, 0);
            Assert.AreEqual(8, groups.Count);
            Assert.IsTrue(groups.All(group => group.pairs.Count == 1));
            Assert.AreEqual(3, groups.Count(group => group.pairs.First().text == "ala"));
            Assert.AreEqual(2, groups.Count(group => group.pairs.First().text == "ma"));
            Assert.AreEqual(3, groups.Count(group => group.pairs.First().text == "kota"));

            groups = SummaryGenerator.GroupMatchPositions(matchPositions, 10);
            Assert.AreEqual(2, groups.Count);
            Assert.AreEqual(6, groups[0].pairs.Count);
            Assert.AreEqual(2, groups[1].pairs.Count);
        }

        [TestMethod]
        public void GenerateContext()
        {
            int contextSizeChars = 5;
            List<PositionTextPairGroup> groups =
                new List<PositionTextPairGroup> {
                    new PositionTextPairGroup(new PositionTextPair(0, "ala"), contextSizeChars)
                        .Extend(new PositionTextPair(5, "ma"), contextSizeChars),
                    new PositionTextPairGroup(new PositionTextPair(20, "ala"), contextSizeChars)
                        .Extend(new PositionTextPair(23, "ma"), contextSizeChars)
                        .Extend(new PositionTextPair(24, "ala"), contextSizeChars)
                        .Extend(new PositionTextPair(30, "kota"), contextSizeChars)
                };
            StreamReader reader = new StreamReader(new MemoryStream(
                   Encoding.UTF8.GetBytes("ala12ma1234567890123alamala123kota1234567890")));

            Mock<IDocumentBuilder> builder = mockFactory.CreateMock<IDocumentBuilder>();
            builder.Expects.One
                           .Method(b => b.BeginContextBlock());
            builder.Expects.One
                           .Method(b => b.EndContextBlock());
            builder.Expects.AtMostOne
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("ala", TextStyle.Bold);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("12", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("ma", TextStyle.Bold);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("12345", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("90123", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("alamala", TextStyle.Bold);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("123", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("kota", TextStyle.Bold);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("12345", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With(" ... ", TextStyle.Normal);
            builder.Expects.One
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With(" ...", TextStyle.Normal);

            SummaryGenerator.GenerateContext(builder.MockObject, groups, reader);
        }

        [TestMethod]
        public void Generate()
        {
            Mock<IDocumentBuilder> builder = mockFactory.CreateMock<IDocumentBuilder>();
            builder.Expects.One
                           .Method(b => b.AddReportHeader(new DateTime(), null, null))
                           .WithAnyArguments();
            builder.Expects.AtLeast(2)
                           .Method(b => b.AddSectionHeader(null))
                           .WithAnyArguments();
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.AddSearchResult(new SearchResult()))
                           .WithAnyArguments();
            builder.Expects.One
                           .Method(b => b.AddReportFooter());
            builder.Expects.One
                           .Method(b => b.Save(null))
                           .With(outputFilename);
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.BeginContextBlock());
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.EndContextBlock());
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("string", TextStyle.Bold);
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With("... ", TextStyle.Normal);
            builder.Expects.Exactly(matchingFiles.Count * 2)
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With(" ... ", TextStyle.Normal);
            builder.Expects.Exactly(matchingFiles.Count)
                           .Method(b => b.AddContextText(null, TextStyle.Normal))
                           .With(" ...", TextStyle.Normal);

            SummaryGenerator generator = new SummaryGenerator();
            ReportOptions options = new ReportOptions();
            options.outputFilePath = outputFilename;
            options.contextSizeChars = 0;

            generator.Generate(builder.MockObject, options, searchQuery, inputPaths, matchingFiles);
        }

        /// <summary>
        /// This test is not fully automatic - it requires pressing the "OK" button, so the
        /// TestMethod annotation is commented out.
        /// 
        /// It is here to show how the SearchSummary module should be used by external modules.
        /// </summary>
        //[TestMethod]
        public void Manual_Generate()
        {
            ISummaryGenerator generator = SummaryGeneratorFactory.Create();
            generator.Generate(searchQuery, inputPaths, matchingFiles);
        }
    }
}
