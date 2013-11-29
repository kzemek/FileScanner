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
        bool emptyFileExisted;

        string searchQuery;
        string outputFilename;
        List<String> inputPaths;
        List<MatchingFile> matchingFiles;
        
        [TestInitialize]
        public void Setup()
        {
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
                file.fileReader = new StreamReader(new MemoryStream(0));
                file.searchResults = new Dictionary<string, IEnumerable<int>>();
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
