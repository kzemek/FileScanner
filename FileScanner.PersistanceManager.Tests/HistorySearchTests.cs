using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager;
using FileScanner.PersistanceManager.Interfaces;
using NUnit.Framework;
namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture()]
    public class HistorySearchTests
    {
        private const string TestDatabaseFileName = "testDatabase.s3db";
        private readonly ISQLDatabase _database;
        


        [SetUp()]
        public void Init()
        {
            if (File.Exists(TestDatabaseFileName))
            {
                File.Delete(TestDatabaseFileName);
            }
            var _database = new SqLiteDatabase(TestDatabaseFileName);
            //_database.Insert();
        }

        [Test()]
        public void HistorySearchTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetEnumeratorTest()
        {
            Assert.Fail();
        }

    }
}
