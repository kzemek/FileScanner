using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    class History: IEnumerable<ISearch>
    {
        private DataTable _dataTable;
        private readonly ISQLDatabase _sql;

        internal History(DataTable dataTable, ISQLDatabase sql)
        {
            _dataTable = dataTable;
            _sql = sql;
        }

        public IEnumerator<ISearch> GetEnumerator()
        {
            foreach (DataRow row in _dataTable.Rows)
            {
                yield return new HistorySearch(DateTime.Parse(row["date"].ToString()), int.Parse(row["id"].ToString()));

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
