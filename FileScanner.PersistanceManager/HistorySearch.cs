using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class HistorySearch : ISearch
    {
        private readonly int _id;
        private readonly ISQLDatabase _sql;
        private DataTable _searchTable;

        public IEnumerable<IPhrase> GetPhrases()
        {
            var phraseTable = _sql.GetDataTable("SELECT * FROM [phrases] WHERE [search_id] = " + _id);

            return (from DataRow row in phraseTable.Rows select new Phrase() {PhraseText = row["phraseText"].ToString()}).Cast<IPhrase>();
        }

        public HistorySearch(DateTime searchTime, int id, ISQLDatabase sql)
        {
            SearchTime = searchTime;
            _id = id;
            _sql = sql;
        }
        public HistorySearch(DataRow row, ISQLDatabase sql)
        {
            SearchTime = Convert.ToDateTime(row["searchDate"]);
            _id = int.Parse(row["id"].ToString());
            _sql = sql; 
        }

        public IEnumerator<SearchSummary.MatchingFile> GetEnumerator()
        {
            if (_searchTable == null)
            {
                _searchTable = _sql.GetDataTable("SELECT * " +
                                                 "    FROM [files]" +
                                                 "    INNER JOIN [matches] USING ([file_id])" +
                                                 "    INNER JOIN [phrases] USING ([phrase_id], [serach_id])" +
                                                 "WHERE search_id=" + _id +
                                                 "ORDER BY file_id");           
            }
            return (from DataRow row in _searchTable.Rows select new HistorySearchFile()).GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public DateTime SearchTime { get; private set; }
    }
}