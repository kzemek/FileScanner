using System.Collections.Generic;
using System.Data;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class PersistanceManager : IPersistanceManager
    {
        readonly ISQLDatabase _sqLiteDatabase;

        public PersistanceManager(ISQLDatabase sqLiteDatabase)
        {
            _sqLiteDatabase = sqLiteDatabase;
        }

        public PersistanceManager()
        {
            _sqLiteDatabase = new SQLiteDatabase();
        }

        public void SaveData(ISearch search)
        {
            //_sqLiteDatabase.GetDataTable("SELECT ")
        }

        public ICollection<ISearch> GetFullHistory()
        {
            DataTable dataTable = _sqLiteDatabase.GetDataTable("SELECT * FROM [searches]");

        }

        public ISearch GetSearch(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}