using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileScanner.PatternMatching;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture]
    public class PersistanceManagerTests
    {
        private readonly PersistanceManager _persistenceManager = new PersistanceManager();

        [Test]
        [ExpectedException(typeof(PersistanceManager.UnsupportedExtensionException))]
        public void SaveSearch_GivenUnsupportedFileExtension_ThrowsUnsupportedExtensionException()
        {
            string testDatabaseFileName = System.Reflection.MethodBase.GetCurrentMethod().Name + "TestDatabase.abcdef";

            string[] phrases = { "Ala", "ma", "kota" };
            Match[] file1Matches = { new Match(10, "Ala"), new Match(20, "kota"), new Match(30, "kota") };
            Match[] file2Matches = { new Match(8, "ma"), new Match(11, "ma"), new Match(999, "kota") };
            MatchingFile[] matchingFiles = { new MatchingFile("plik1.txt", "C:/plik1.txt", 123, file1Matches), new MatchingFile("plik2.txt", "C:/plik2.txt", 123, file2Matches) };
            var savedSearch = new Search(DateTime.Now, DateTime.Now.AddMinutes(1.0), 123, phrases, matchingFiles);

            _persistenceManager.SaveSearch(savedSearch, testDatabaseFileName);
        }

        [Test]
        public void SaveSearch_SavesSearchInDatabase_GetLastSearch_GetsItBack()
        {
            string testDatabaseFileName = System.Reflection.MethodBase.GetCurrentMethod().Name + "TestDatabase.s3db";

            string[] phrases = {"Ala", "ma", "kota"};
            Match[] file1Matches = {new Match(10, "Ala"), new Match(20, "kota"), new Match(30, "kota")};
            Match[] file2Matches = {new Match(8, "ma"), new Match(11, "ma"), new Match(999, "kota")};
            MatchingFile[] matchingFiles = {new MatchingFile("plik1.txt", "C:/plik1.txt", 123, file1Matches), new MatchingFile("plik2.txt", "C:/plik2.txt", 123, file2Matches)};
            var savedSearch = new Search(DateTime.Now, DateTime.Now.AddMinutes(1.0), 123, phrases, matchingFiles);

            _persistenceManager.SaveSearch(savedSearch, testDatabaseFileName);
            var loadedSearch = _persistenceManager.GetLastSearch(testDatabaseFileName);

            Assert.AreEqual(savedSearch, loadedSearch);
        }

        [Test]
        public void SaveSearch_SavesSearchesInNewDatabaseFile_GetFullHistory_GetsSearchesBack()
        {
            string testDatabaseFileName = System.Reflection.MethodBase.GetCurrentMethod().Name + "TestDatabase.s3db";

            if (File.Exists(testDatabaseFileName))
            {
                File.Delete(testDatabaseFileName);
            }
            string[][] phrases =
            {
                new string[] {"Ala", "ma", "kota"},
                new string[] {"Janek", "posiada", "psa"},
                new string[] {"Mike", "has", "parrots"}
            };
            Match[][] file1Matches =
            {
                new Match[] { new Match(10, "Ala"), new Match(20, "kota"), new Match(30, "kota") },
                new Match[] { new Match(10, "Janek"), new Match(20, "psa"), new Match(30, "posiada") },
                new Match[] { new Match(10, "has"), new Match(20, "parrots"), new Match(30, "has") },
            };
            Match[][] file2Matches =
            {
                new Match[] { new Match(8, "Ala"), new Match(3214, "kota"), new Match(999, "kota") },
                new Match[] { new Match(143, "Janek"), new Match(67, "psa"), new Match(3, "posiada") },
                new Match[] { new Match(1234, "has"), new Match(18, "parrots"), new Match(9, "has") },
            };
            MatchingFile[][] matchingFiles =
            {
                new MatchingFile[] { new MatchingFile("plik1.txt", "C:/plik1.txt", 123, file1Matches[0]), new MatchingFile("plik2.txt", "C:/plik2.txt", 123, file2Matches[0]) },
                new MatchingFile[] { new MatchingFile("jsadlkjf.txt", "C:/jsadlkjf.txt", 432, file1Matches[1]), new MatchingFile("sdgfdhgfjhg.txt", "C:/sdgfdhgfjhg.txt", 1342523, file2Matches[1]) }, 
                new MatchingFile[] { new MatchingFile("kąęźóćłłłłą.txt", "C:/kąęźóćłłłłą.txt", 1500, file1Matches[2]), new MatchingFile("sag3.txt", "C:/sag3.txt", 77, file2Matches[2]) }
            };
            var savedSearches = new LinkedList<Search>();
            for (int i = 0; i < phrases.Length; i++)
            {
                var savedSearch = new Search(DateTime.Now, DateTime.Now.AddMinutes(1.0), 995, phrases[i], matchingFiles[i]);
                savedSearches.AddLast(savedSearch);
            }

            foreach (var search in savedSearches)
            {
                _persistenceManager.SaveSearch(search, testDatabaseFileName);
            }

            var sth = _persistenceManager.GetFullHistory(testDatabaseFileName);

            Assert.That(savedSearches.SequenceEqual(sth));
        }
    }
}