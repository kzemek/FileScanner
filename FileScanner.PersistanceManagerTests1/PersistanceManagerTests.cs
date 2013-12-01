using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileScanner.PersistanceManager.Interfaces;
using Moq;
using NUnit.Framework;
using Match = FileScanner.PatternMatching.Match;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture]
    public class PersistanceManagerTests
    {
        [SetUp]
        public void Before()
        {
            isqlDatabaseMock = new Mock<ISQLDatabase>();

            Instace = new PersistanceManager();
        }

        private PersistanceManager Instace;
        private Mock<ISQLDatabase> isqlDatabaseMock;

        [Test]
        public void GetFullHistoryTest()
        {
            Assert.Fail();
        }

        [Test]
        public void GetSearchTest()
        {
            Assert.Fail();
        }

        [Test]
        public void SaveDataTest()
        {
            var searchMock = new Mock<ISearch>();
            var searchFile1Mock = new Mock<ChMatchingFile>();
            var searchFile2Mock = new Mock<ChMatchingFile>();
            var matchMocksInFile1 = new List<Mock<PatternMatching.Match>> { new Mock<PatternMatching.Match>(), new Mock<PatternMatching.Match>() };
            var matchMocksInFile2 = new List<Mock<PatternMatching.Match>> { new Mock<PatternMatching.Match>(), new Mock<PatternMatching.Match>() };

            searchMock.Setup(search => search.EndTime).Returns(new DateTime(2013, 1, 2, 12, 18, 31));
            searchMock.Setup(search => search.StartTime).Returns(new DateTime(2013, 1, 2, 12, 18, 31));
            searchMock.Setup(search => search.ProcessedFilesCount).Returns(12);
            searchMock.Setup(search => search.GetEnumerator()).Returns(new Collection<MatchingFile>() { searchFile1Mock.Object, searchFile2Mock.Object }.GetEnumerator);

            searchFile1Mock.Setup(searchFile => searchFile.FileName).Returns("file1.txt");
            searchFile1Mock.Setup(searchFile => searchFile.FullPath).Returns("path/file1.txt");
            searchFile1Mock.Setup(searchFile => searchFile.Matches).Returns(new List<PatternMatching.Match>() { matchMocksInFile1[0].Object, matchMocksInFile1[1].Object });

            searchFile2Mock.Setup(searchFile => searchFile.FileName).Returns("file2.txt");
            searchFile2Mock.Setup(searchFile => searchFile.FullPath).Returns("path/file2.txt");
            searchFile2Mock.Setup(searchFile => searchFile.Matches).Returns(new List<PatternMatching.Match>() { matchMocksInFile2[0].Object, matchMocksInFile2[1].Object });

            Instace.SaveSearch(searchMock.Object, "testOfPeristanceManager.s3db");


            //ICollection<ISearch> fullHistory = Instace.GetFullHistory();
        }

        public class ChMatchingFile : MatchingFile
        {
            public virtual new String FileName
            {
                get; set;
            }

            public virtual new String FullPath
            {
                get; set;
            }

            public virtual new List<PatternMatching.Match> Matches
            {
                get; set;
            }
            public ChMatchingFile() : base(null, null, 0, null)
            {
            }
        }
    }
}