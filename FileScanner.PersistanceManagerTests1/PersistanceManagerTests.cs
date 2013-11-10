using System;
using System.Collections.Generic;
using FileScanner.PersistanceManager.Interfaces;
using Moq;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture]
    public class PersistanceManagerTests
    {
        [SetUp]
        public void Before()
        {
            isqlDatabaseMock = new Mock<ISQLDatabase>();

            Instace = new PersistanceManager(isqlDatabaseMock.Object);
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
            var searchFile1Mock = new Mock<ISearchFile>();
            var searchFile2Mock = new Mock<ISearchFile>();
            var matchMocksInFile1 = new List<Mock<Match>> {new Mock<Match>(), new Mock<Match>()};
            var matchMocksInFile2 = new List<Mock<Match>> {new Mock<Match>(), new Mock<Match>()};

            searchMock.Setup(search => search.SearchTime).Returns(new DateTime(2013, 1, 2, 12, 18, 31));
            searchFile1Mock.Setup(searchFile => searchFile.FilePath).Returns("path/file1.txt");
            searchFile2Mock.Setup(searchFile => searchFile.FilePath).Returns("path/file2.txt");
            

            searchFile1Mock.Setup(searchFile => (IEnumerator<Match>) searchFile.GetEnumerator())
                .Returns(matchMocksInFile1.ConvertAll(
                    mock => mock.Object).GetEnumerator());
            searchFile2Mock.Setup(searchFile => (IEnumerator<Match>) searchFile.GetEnumerator())
                .Returns(matchMocksInFile2.ConvertAll(
                    mock => mock.Object).GetEnumerator());

            searchMock.Setup(search => search.GetEnumerator())
                .Returns(new List<ISearchFile> {searchFile1Mock.Object, searchFile2Mock.Object}.GetEnumerator());

            Instace.SaveData(searchMock.Object);


        }
    }
}