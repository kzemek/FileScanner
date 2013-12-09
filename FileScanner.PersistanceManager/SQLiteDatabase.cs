using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    /// <summary>
    ///     Contains methods for interacting with an SQLite database.
    ///     Based on: http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/
    /// </summary>
    internal class SqLiteDatabase : ISQLDatabase
    {
        private readonly String _dbConnection;

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="databaseFile">The File containing the DB</param>
        public SqLiteDatabase(String databaseFile)
        {
            _dbConnection = String.Format("Data Source={0}; Version=3;", databaseFile);
            if (!File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
                //var streamReader = new StreamReader("FileScanner.PersistanceManager/previousSearches.sql", Encoding.UTF8);
                //string databaseStructure = streamReader.ReadToEnd();
                //streamReader.Close();
                //ExecuteNonQuery(databaseStructure);
                ExecuteNonQuery(@"
-- Table: searches
CREATE TABLE searches ( 
    search_id           INTEGER  PRIMARY KEY AUTOINCREMENT,
    startTime           BIGINT,
    endTime             BIGINT,
    processedFilesCount INTEGER 
);


-- Table: files
CREATE TABLE files ( 
    file_id     INTEGER         PRIMARY KEY AUTOINCREMENT,
    fileName    VARCHAR( 256 ),
    fullPath    VARCHAR( 256 ),
    sizeInBytes INTEGER,
    search_id                   NOT NULL
                                REFERENCES searches ( search_id ) ON DELETE CASCADE
                                                                  ON UPDATE CASCADE 
);


-- Table: matches
CREATE TABLE matches ( 
    file_id  INTEGER         REFERENCES files ( file_id ),
    [index]  INTEGER,
    value    VARCHAR( 256 ),
    match_id INTEGER         PRIMARY KEY AUTOINCREMENT 
);


-- Table: phrases
CREATE TABLE phrases ( 
    search_id INTEGER         REFERENCES searches ( search_id ),
    phrase_id INTEGER         PRIMARY KEY,
    phrase    VARCHAR( 256 ) 
);

");
            }
        }

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable GetDataTable(string sql)
        {
            var dt = new DataTable();
            var cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            var mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            cnn.Close();
            return dt;
        }

        /// <summary>
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {
            var cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            var mycommand = new SQLiteCommand(cnn);
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
            var cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            var mycommand = new SQLiteCommand(cnn);
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
                foreach (var val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key, val.Value);
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
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
            foreach (var val in data)
            {
                columns += String.Format(" {0},", val.Key);
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
            DataTable tables = GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
            foreach (DataRow table in tables.Rows)
            {
                ClearTable(table["NAME"].ToString());
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