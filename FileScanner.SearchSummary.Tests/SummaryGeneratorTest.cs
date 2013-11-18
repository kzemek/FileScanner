using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using System.Collections.Generic;
using System.IO;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class SummaryGeneratorTest
    {
        MockFactory mockFactory;
        FileInfo emptyFileInfo;

        string searchQuery;
        string outputFilename;
        List<String> inputPaths;
        List<MatchingFile> matchingFiles;
        
        [TestInitialize]
        public void Setup()
        {
            mockFactory = new MockFactory();
            emptyFileInfo = new FileInfo("someTestFile.txt");

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
                file.searchResults = new Dictionary<string, IEnumerable<int>>();
                file.accuracy = (float)i;
                matchingFiles.Add(file);
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            emptyFileInfo.Delete();
        }

        [TestMethod]
        public void Internal_Generate()
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

            SummaryGenerator generator = new SummaryGenerator();
            ReportOptions options = new ReportOptions();
            options.outputFilePath = outputFilename;

            generator.Generate(builder.MockObject, options, searchQuery, inputPaths, matchingFiles);
        }

        public void Manual_Generate()
        {
            SummaryGenerator generator = new SummaryGenerator();
            generator.Generate(searchQuery, inputPaths, matchingFiles);
        }
    }
}
