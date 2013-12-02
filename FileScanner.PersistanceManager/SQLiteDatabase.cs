using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;

    /// <summary>
    /// Contains methods for interacting with an SQLite database.
    /// Source: http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/
    /// </summary>

    internal class SqLiteDatabase : ISQLDatabase
    {
        private const string DefaultDatabaseFile = "Data Source=PreviousSearches.s3db";
        readonly String _dbConnection;

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public SqLiteDatabase()
        {
            _dbConnection = DefaultDatabaseFile;
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="databaseFile">The File containing the DB</param>

        public SqLiteDatabase(String databaseFile)
        {
            _dbConnection = String.Format("Data Source={0}", databaseFile);
            if (File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
                StreamReader streamReader = new StreamReader("FileScanner.PersistanceManager/previousSearches.sql", Encoding.UTF8);
                string databaseStructure = streamReader.ReadToEnd();
                streamReader.Close();
                this.ExecuteNonQuery(databaseStructure);
            }
        }
        
        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(_dbConnection);
                cnn.Open();
                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

        /// <summary>
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }

        /// <summary>
        ///     Allows the programmer to retrieve single items from the DB.
        /// </summary>
        /// <param name="sql">The query to run.</param>
        /// <returns>A string.</returns>
        public string ExecuteScalar(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            object value = mycommand.ExecuteScalar();
            cnn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }

        /// <summary>
        ///     Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        public void Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";
            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
        }

        /// <summary>
        ///     Allows the programmer to easily delete rows from the DB.
        /// </summary>
        /// <param name="tableName">The table from which to delete.</param>
        /// <param name="where">The where clause for the delete.</param>
        public void Delete(String tableName, String where)
        {
            ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
        }

        /// <summary>
        ///     Allows the programmer to easily insert into the DB
        /// </summary>
        /// <param name="tableName">The table into which we insert the data.</param>
        /// <param name="data">A dictionary containing the column names and data for the insert.</param>
        public void Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
        }

        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        public void ClearDB()
        {
            DataTable tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        public void ClearTable(String table)
        {
            ExecuteNonQuery(String.Format("delete from {0};", table));
        }
    }

}
