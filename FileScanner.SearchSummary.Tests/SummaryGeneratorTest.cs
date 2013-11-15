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
        
        [TestInitialize]
        public void Setup()
        {
            mockFactory = new MockFactory();

            emptyFileInfo = new FileInfo("someTestFile.txt");
        }

        [TestCleanup]
        public void Cleanup()
        {
            emptyFileInfo.Delete();
        }

        [TestMethod]
        public void TestSummaryGenerator_Internal_Generate_WithDocumentBuilder()
        {
            string searchQuery = "foo bar baz";
            string outputFilename = "testOutput";
            List<string> inputPaths = new List<string> {
                @"c:\some_folder",
                @"c:\other_folder\file.txt"
            };
            List<MatchingFile> matchingFiles = new List<MatchingFile>();
            for (int i = 0; i < 5; ++i)
            {
                MatchingFile file;
                file.fileInfo = emptyFileInfo;
                file.searchResults = new Dictionary<string, IEnumerable<int>>();
                file.accuracy = (float)i;
                matchingFiles.Add(file);
            }

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
            builder.Expects.Any
                           .Method(b => b.AddText(null))
                           .WithAnyArguments();
            builder.Expects.One
                           .Method(b => b.Save(null))
                           .With(outputFilename);

            SummaryGenerator generator = new SummaryGenerator();

            generator.Generate(builder.MockObject, outputFilename, searchQuery, inputPaths, matchingFiles);
        }
    }
}
