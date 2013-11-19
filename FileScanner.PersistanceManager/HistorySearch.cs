using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class HistorySearch : ISearch
    {
        private readonly int _id;
        private readonly ISQLDatabase _database;
        private DataTable _searchTable;

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int ProcessedFilesCount { get; private set; }

        public HistorySearch(DataRow row, ISQLDatabase database)
        {
            StartTime = Convert.ToDateTime(row["startTime"]);
            EndTime = Convert.ToDateTime(row["endTime"]);
            ProcessedFilesCount = int.Parse(row["processedFilesCount"].ToString());
            _id = int.Parse(row["id"].ToString());
            _database = database;
        }

        public IEnumerable<String> GetPhrases()
        {
            var phraseTable = _database.GetDataTable("SELECT * FROM [phrases] WHERE [search_id] = " + _id);

            return (from DataRow row in phraseTable.Rows select row["phraseText"].ToString());
        }

        public IEnumerator<MatchingFile> GetEnumerator()
        {
            if (_searchTable == null)
            {
                _searchTable = _database.GetDataTable("SELECT * " +
                                                      "    FROM [files]" +
                                                      "    INNER JOIN [matches] USING ([file_id])" +
                                                      "    INNER JOIN [phrases] USING ([phrase_id], [search_id])" +
                                                      "    WHERE search_id=" + _id +
                                                      "    ORDER BY file_id");
            }

            var distinctFileIDs = _searchTable.DefaultView.ToTable(true, "file_id");

            foreach (DataRow fileIDRow in distinctFileIDs.Rows)
            {
                var fileID = int.Parse(fileIDRow["file_id"].ToString());
                var matches = (from DataRow row in _searchTable.Rows
                    where int.Parse(row["file_id"].ToString()) == fileID
                    select new Match(int.Parse(row["index"].ToString()), row["value"].ToString())) as List<Match>;
                yield return
                    new MatchingFile(row["fileName"].ToString(), row["fullPath"].ToString(), row["sizeInBytes"], matches)
                    ;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}