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
            byte[] testStringBytes = Encoding.UTF8.GetBytes("test string for testing strings");

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
                file.searchResults = new Dictionary<string, IEnumerable<int>> { { "string", new List<int> { 5, 24 } } };
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
        public void Internal_PositionTextPair_Overlaps()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ala"),
                new PositionTextPair(1, "lama"),
                new PositionTextPair(3, "ma"),
                new PositionTextPair(4, "akota")
            };

            Assert.IsTrue(pairs[0].OverlapsOrIsAdjacentTo(pairs[1]));
            Assert.IsTrue(pairs[1].OverlapsOrIsAdjacentTo(pairs[0]));
            Assert.IsTrue(pairs[0].OverlapsOrIsAdjacentTo(pairs[2]));
            Assert.IsTrue(pairs[2].OverlapsOrIsAdjacentTo(pairs[0]));
            Assert.IsFalse(pairs[0].OverlapsOrIsAdjacentTo(pairs[3]));
            Assert.IsFalse(pairs[3].OverlapsOrIsAdjacentTo(pairs[0]));
        }

        [TestMethod]
        public void Internal_PositionTextPair_Merge_Adjacent()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ala"),
                new PositionTextPair(3, "ma"),
                new PositionTextPair(0, "ala"),
                new PositionTextPair(3, "ma")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(3, pairs[1].position);
            Assert.AreEqual("ma", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("ala", pairs[2].text);
        }

        [TestMethod]
        public void Internal_PositionTextPair_Merge_Overlapping()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ma"),
                new PositionTextPair(1, "ala"),
                new PositionTextPair(0, "ma"),
                new PositionTextPair(1, "ala"),
                new PositionTextPair(20, "alama"),
                new PositionTextPair(24, "ala"),
                new PositionTextPair(20, "alama"),
                new PositionTextPair(24, "ala")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("mala", pairs[0].text);
            Assert.AreEqual(1, pairs[1].position);
            Assert.AreEqual("ala", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("mala", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("ma", pairs[2].text);

            pairs[4].Merge(pairs[5]);
            pairs[7].Merge(pairs[6]);

            Assert.AreEqual(20, pairs[4].position);
            Assert.AreEqual("alamala", pairs[4].text);
            Assert.AreEqual(24, pairs[5].position);
            Assert.AreEqual("ala", pairs[5].text);

            Assert.AreEqual(20, pairs[7].position);
            Assert.AreEqual("alamala", pairs[7].text);
            Assert.AreEqual(20, pairs[6].position);
            Assert.AreEqual("alama", pairs[6].text);
        }

        [TestMethod]
        public void Internal_PositionTextPair_Merge_Substring_CommonStart()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(0, "ala"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(0, "ala")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(0, pairs[1].position);
            Assert.AreEqual("ala", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }

        [TestMethod]
        public void Internal_PositionTextPair_Merge_Substring_CommonEnd()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(2, "ama"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(2, "ama")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(2, pairs[1].position);
            Assert.AreEqual("ama", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }

        [TestMethod]
        public void Internal_PositionTextPair_Merge_Substring_Inside()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(1, "lam"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(1, "lam")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(1, pairs[1].position);
            Assert.AreEqual("lam", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }

        [TestMethod]
        public void Internal_PositionTextPair_IsWithinRange()
        {
            int contextSizeChars = 5;
            string testStr = "alama";
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, testStr),
                new PositionTextPair(contextSizeChars, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length - 1, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length + 1, testStr)
            };
            PositionTextPairGroup group = new PositionTextPairGroup(pairs[0], contextSizeChars);

            Assert.IsTrue(group.IsWithinRange(pairs[0], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[1], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[2], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[3], contextSizeChars));
            Assert.IsFalse(group.IsWithinRange(pairs[4], contextSizeChars));

            group = new PositionTextPairGroup(pairs[4], contextSizeChars);

            Assert.IsFalse(group.IsWithinRange(pairs[0], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[1], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[2], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[3], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[4], contextSizeChars));
        }

        [TestMethod]
        public void Internal_GroupMatchPositions()
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
        public void Internal_GenerateContext()
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
            builder.Expects.Exactly(matchingFiles.Count)
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
