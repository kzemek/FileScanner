using System.Collections.Generic;
using System.Data;
using FileScanner.PatternMatching;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture()]
    public class MatchingFileTests
    {
        [Test()]
        public void RowConstructor_CreatesCorrectMatchingFile()
        {
            const string fileName = "plik.txt";
            const string fullPath = "c:\\plik.txt";
            const int sizeInBytes = 12345;

            var dataTable = new DataTable();
            dataTable.Columns.Add("fileName", typeof(string));
            dataTable.Columns.Add("fullPath", typeof(string));
            dataTable.Columns.Add("sizeInBytes", typeof(long));
            dataTable.Rows.Add(fileName, fullPath, sizeInBytes);

            var enumerator = dataTable.Rows.GetEnumerator();
            enumerator.MoveNext();

            var matchingFile = new MatchingFile((DataRow) enumerator.Current, new LinkedList<Match>());
            Assert.AreEqual(fileName, matchingFile.FileName);
            Assert.AreEqual(fullPath, matchingFile.FullPath);
            Assert.AreEqual(sizeInBytes, matchingFile.SizeInBytes);
        }
    }
}
