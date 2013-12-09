using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class HistorySearch : AbstractSearch
    {
        private readonly ISQLDatabase _database;
        private readonly int _id;
        private DataTable _phraseTable;
        private DataTable _searchTable;

        public HistorySearch(DataRow row, ISQLDatabase database)
        {
            StartTime = new DateTime(long.Parse(row["startTime"].ToString()));
            EndTime = new DateTime(long.Parse(row["endTime"].ToString()));
            ProcessedFilesCount = uint.Parse(row["processedFilesCount"].ToString());
            _id = int.Parse(row["search_id"].ToString());
            _database = database;
        }

        public override IEnumerable<string> Phrases
        {
            get
            {
                if (_phraseTable == null)
                {
                    _phraseTable = _database.GetDataTable("SELECT * FROM [phrases] WHERE [search_id] = " + _id);
                }
                return (from DataRow row in _phraseTable.Rows select row["phrase"].ToString());
            }
            protected set { throw new InvalidOperationException(); }
        }

        public override IEnumerator<MatchingFile> GetEnumerator()
        {
            if (_searchTable == null)
            {
                _searchTable = _database.GetDataTable("SELECT * " +
                                                      "    FROM [files]" +
                                                      "    INNER JOIN [matches] USING ([file_id])" +
                                                      "    WHERE search_id=\"" + _id +
                                                      "\"  ORDER BY file_id");
            }

            int previousFileId = -1;
            var matches = new LinkedList<Match>();
            DataRow previousRow = null;

            foreach (DataRow row in _searchTable.Rows)
            {
                int newFileId = int.Parse(row["file_id"].ToString());
                if (previousFileId != newFileId)
                {
                    if (previousRow != null)
                    {
                        yield return new MatchingFile(previousRow, matches);
                    }
                    previousRow = row;

                    matches = new LinkedList<Match>();
                    previousFileId = newFileId;
                }
                matches.AddLast(new Match(int.Parse(row["index"].ToString()), row["value"].ToString()));
            }

            if (previousRow != null)
            {
                yield return new MatchingFile(previousRow, matches);
            }
        }
    }
}