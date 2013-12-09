using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture]
    public class PersistanceManagerTests
    {
        private const string TestDatabaseFileName = "testDatabase.s3db";
        private readonly PersistanceManager _persistenceManager = new PersistanceManager();
        private ISQLDatabase _iSqlDatabase;

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
        public void SaveSearchTest()
        {
            string[] phrases = {"Ala", "ma", "kota"};
            Match[] file1Matches = {new Match(10, "Ala"), new Match(20, "kota"), new Match(30, "kota")};
            Match[] file2Matches = {new Match(8, "ma"), new Match(11, "ma"), new Match(999, "kota")};
            MatchingFile[] matchingFiles = {new MatchingFile("plik1.txt", "C:/plik1.txt", 123, file1Matches), new MatchingFile("plik2.txt", "C:/plik2.txt", 123, file2Matches)};
            var savedSearch = new Search(DateTime.Now, DateTime.Now.AddMinutes(1.0), 123, phrases, matchingFiles);

            _persistenceManager.SaveSearch(savedSearch, TestDatabaseFileName);

            var loadedSearch = _persistenceManager.GetLastSearch(TestDatabaseFileName);

            Assert.AreEqual(savedSearch.StartTime, loadedSearch.StartTime);
            Assert.AreEqual(savedSearch.EndTime, loadedSearch.EndTime);
            //...
        }

    }
}