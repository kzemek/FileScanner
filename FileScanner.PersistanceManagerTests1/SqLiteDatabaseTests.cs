using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager;
using NUnit.Framework;
namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture()]
    public class SqLiteDatabaseTests
    {
        private SqLiteDatabase _database;

        [SetUp()]
        public void Init()
        {
            _database = new SqLiteDatabase("testDatabase.s3db");
        }

        [Test()]
        public void SqLiteDatabaseTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetDataTableTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ExecuteNonQueryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ExecuteScalarTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ClearDBTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ClearTableTest()
        {
            Assert.Fail();
        }
    }
}
