using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
                                                      "    WHERE search_id=" + _id +
                                                      "    ORDER BY file_id");
            }

            int fileId = -1;
            var matches = new LinkedList<Match>();
            DataRow row_= null;

            foreach (DataRow row in _searchTable.Rows)
            {
                int newFileId = int.Parse(row["file_id"].ToString());
                if (fileId != newFileId)
                {
                    if (row_ != null)
                    {
                        yield return new MatchingFile(row_, matches);
                    }
                    row_ = row; 

                    matches = new LinkedList<Match>();
                    fileId = newFileId;
                }
                matches.AddLast(new Match(int.Parse(row["index"].ToString()), row["value"].ToString()));
            }
            if(row_ != null)
                yield return new MatchingFile(row_, matches);                 

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}