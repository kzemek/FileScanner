using System.Collections.Generic;
using System.Data;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class PersistanceManager : IPersistanceManager
    {
        private readonly ISQLDatabase _sqLiteDatabase;

        public PersistanceManager(ISQLDatabase sqLiteDatabase)
        {
            _sqLiteDatabase = sqLiteDatabase;
        }

        public PersistanceManager()
        {
            _sqLiteDatabase = new SQLiteDatabase();
        }

        public void SaveSearch(ISearch search)
        {
            //_sqLiteDatabase.GetDataTable("SELECT ")
        }

        public ICollection<ISearch> GetFullHistory()
        {
            var dataTable = _sqLiteDatabase.GetDataTable("SELECT * FROM [searches]");
            var history = new List<ISearch>();

            foreach (DataRow row in dataTable.Rows)
            {
                history.Add(new HistorySearch(row, _sqLiteDatabase));
            }

            return history;
        }
    }
}