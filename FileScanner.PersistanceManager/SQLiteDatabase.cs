using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    /// <summary>
    /// Contains methods for interacting with an SQLite database.
    /// Based on: http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/
    /// </summary>
    internal class SqLiteDatabase : ISQLDatabase
    {
        private readonly String _connectionString;

        /// <summary>
        /// Creates a new SqLiteDatabase object and associates it with database file of given name.
        /// If the database file doesn't exist, creates an empty database under the given file name according to the database structure contained in the second file.
        /// </summary>
        /// <param name="databaseFileName">Name of the file containing the database.</param>
        /// <param name="databaseStructureFileName">Name of the file containing the database structure (CREATE statements).</param>
        public SqLiteDatabase(string databaseFileName, string databaseStructureFileName)
        {
            _connectionString = String.Format("Data Source={0}; Version=3;", databaseFileName);
            if (!File.Exists(databaseFileName))
            {
                SQLiteConnection.CreateFile(databaseFileName);
                var streamReader = new StreamReader(databaseStructureFileName, Encoding.UTF8);
                var databaseStructure = streamReader.ReadToEnd();
                streamReader.Close();
                ExecuteNonQuery(databaseStructure);
            }
        }

        public DataTable GetDataTable(string sqlQuery)
        {
            var dataTable = new DataTable();
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand(connection);
            command.CommandText = sqlQuery;
            SQLiteDataReader reader = command.ExecuteReader();
            dataTable.Load(reader);
            reader.Close();
            connection.Close();
            return dataTable;
        }

        public int ExecuteNonQuery(string sqlQuery)
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand(connection);
            command.CommandText = sqlQuery;
            int rowsUpdated = command.ExecuteNonQuery();
            connection.Close();
            return rowsUpdated;
        }

        public string ExecuteScalar(string sqlQuery)
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand(connection);
            command.CommandText = sqlQuery;
            object result = command.ExecuteScalar();
            connection.Close();
            if (result != null)
            {
                return result.ToString();
            }
            return "";
        }

        public void Insert(String tableName, Dictionary<String, String> dataSet)
        {
            String columns = "";
            String values = "";
            foreach (var item in dataSet)
            {
                columns += String.Format(" {0},", item.Key);
                values += String.Format(" '{0}',", item.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
        }

        public void Update(String tableName, Dictionary<String, String> dataSet, String where)
        {
            String dataSetString = "";
            if (dataSet.Count >= 1)
            {
                foreach (var item in dataSet)
                {
                    dataSetString += String.Format(" {0} = '{1}',", item.Key, item.Value);
                }
                dataSetString = dataSetString.Substring(0, dataSetString.Length - 1);
            }
            ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, dataSetString, where));
        }

        public void Delete(String tableName, String where)
        {
            ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
        }

        public void ClearTable(String table)
        {
            ExecuteNonQuery(String.Format("delete from {0};", table));
        }
    }
}