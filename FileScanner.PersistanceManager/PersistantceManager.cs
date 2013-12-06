using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using FileScanner.PatternMatching;
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
            _sqLiteDatabase = new SqLiteDatabase();
        }

        public void SaveSearch(ISearch search)
        {
            var searchData = new Dictionary<string, string>
            {
                {"startTime", search.StartTime.ToString(CultureInfo.InvariantCulture)},
                {"endTime", search.EndTime.ToString(CultureInfo.InvariantCulture)},
                {"processedFilesCount", search.ProcessedFilesCount.ToString(CultureInfo.InvariantCulture)}
            };
            _sqLiteDatabase.Insert("[searches]", searchData);
            var searchID = int.Parse(_sqLiteDatabase.ExecuteScalar("SELECT last_insert_rowid()"));

            foreach (string phrase in search.Phrases)
            {
                var phraseData = new Dictionary<string, string>
                {
                    {"search_id", searchID.ToString(CultureInfo.InvariantCulture)},
                    {"phrase", phrase},
                };
                _sqLiteDatabase.Insert("[phrases]", phraseData);
            }

            foreach (MatchingFile file in search)
            {
                var fileData = new Dictionary<string, string>
                {
                    {"search_id", searchID.ToString(CultureInfo.InvariantCulture)},
                    {"fileName", file.FileName},
                    {"fullPath", file.FullPath},
                    {"sizeInBytes", file.SizeInBytes.ToString(CultureInfo.InvariantCulture)}
                };
                _sqLiteDatabase.Insert("[files]", fileData);
                var fileID = int.Parse(_sqLiteDatabase.ExecuteScalar("SELECT last_insert_rowid()"));

                foreach (Match match in file.Matches)
                {
                    var matchData = new Dictionary<string, string>
                    {
                        {"file_id", fileID.ToString(CultureInfo.InvariantCulture)},
                        {"index", match.Index.ToString(CultureInfo.InvariantCulture)},
                        {"value", match.Value},
                    };
                    _sqLiteDatabase.Insert("[matches]", matchData);
                }
            }
        }

        public ICollection<ISearch> GetFullHistory()
        {
            var dataTable = _sqLiteDatabase.GetDataTable("SELECT * FROM [searches]");
            return
                (from DataRow row in dataTable.Rows select new HistorySearch(row, _sqLiteDatabase)).Cast<ISearch>()
                    .ToList();
        }
    }
}