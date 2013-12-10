using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture()]
    public class SqLiteDatabaseTests
    {
        private const string DatabaseFileName = "TestDatabase.s3db";
        private const string DatabaseStructureFileName = "SQLiteDatabaseTestDatabaseStructure.sql";
        private const string DatabaseStructure = @"CREATE TABLE tests ( 
                                                       test_id  INTEGER  PRIMARY KEY AUTOINCREMENT,
                                                       sth      BIGINT,
                                                       sth_else VARCHAR ( 256 )
                                                   );";

        private SqLiteDatabase CreateTestDatabase(string databaseFileName, string databaseStructureFileName)
        {
            if (File.Exists(databaseFileName))
            {
                File.Delete(databaseFileName);
            }
            if (File.Exists(databaseStructureFileName))
            {
                File.Delete(databaseStructureFileName);
            }
            var databaseStructureFile = File.CreateText(databaseStructureFileName);
            databaseStructureFile.Write(DatabaseStructure);
            databaseStructureFile.Close();
            return new SqLiteDatabase(databaseFileName, databaseStructureFileName);
        }

        [Test()]
        public void Constructor_GivenNonExistingFileName_CreatesDatabaseFile()
        {
            var databaseFileName = System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseFileName;
            CreateTestDatabase(databaseFileName, System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseStructureFileName);

            Assert.That(File.Exists(databaseFileName));
        }

        [Test()]
        public void Insert_InsertsDataIntoDatabase_GetDataTable_GetsDataBack()
        {
            var database = CreateTestDatabase(System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseFileName, System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseStructureFileName);
            
            const long sth = 93259945293984;
            const string sth_else = "hello world";

            database.Insert("tests", new Dictionary<string, string>
            {
                {"sth", sth.ToString(CultureInfo.InvariantCulture)},
                {"sth_else", sth_else.ToString(CultureInfo.InvariantCulture)}
            });

            var dataTable = database.GetDataTable("SELECT * FROM [tests]");
            var rowEnumerator = dataTable.Rows.GetEnumerator();
            rowEnumerator.MoveNext();
            var row = (DataRow) rowEnumerator.Current;
            Assert.AreEqual(row["sth"], sth);
            Assert.AreEqual(row["sth_else"], sth_else);
        }

        [Test()]
        public void ExecuteNonQuery_GivenANonQuery_UpdatesRows()
        {
            var database = CreateTestDatabase(System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseFileName, System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseStructureFileName);
            
            const long sth = 93259945293984;
            const string sth_else = "hello world";

            var rowsUpdated =
                database.ExecuteNonQuery(String.Format("INSERT INTO [tests] (sth, sth_else) VALUES ({0}, \"{1}\");",
                    sth.ToString(CultureInfo.InvariantCulture), sth_else.ToString(CultureInfo.InvariantCulture)));

            Assert.AreEqual(1, rowsUpdated);
            var dataTable = database.GetDataTable("SELECT * FROM [tests]");
            var rowEnumerator = dataTable.Rows.GetEnumerator();
            rowEnumerator.MoveNext();
            var row = (DataRow) rowEnumerator.Current;
            Assert.AreEqual(row["sth"], sth);
            Assert.AreEqual(row["sth_else"], sth_else);
        }

        [Test()]
        public void ExecuteScalarTest_GivenAQuery_ReturnsCorrectScalarResult()
        {
            var database = CreateTestDatabase(System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseFileName, System.Reflection.MethodBase.GetCurrentMethod().Name + DatabaseStructureFileName);
            
            long[] sths = {93259945293984, 534235234};
            const string sth_else = "hello world";

            foreach (long sth in sths)
            {
                database.Insert("tests", new Dictionary<string, string>
                {
                    {"sth", sth.ToString(CultureInfo.InvariantCulture)},
                    {"sth_else", sth_else.ToString(CultureInfo.InvariantCulture)}
                });
            }

            var result = database.ExecuteScalar("SELECT AVG(sth) FROM [tests]");

            Assert.That(sths.Average(), Is.EqualTo(long.Parse(result)).Within(.01));
        }
    }
}