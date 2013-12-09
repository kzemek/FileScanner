using System;
using System.Collections.Generic;
using System.Data;

namespace FileScanner.PersistanceManager.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with an SQL database.
    /// Based on: http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/
    /// </summary>
    public interface ISQLDatabase
    {
        /// <summary>
        /// Allows the programmer to run a query against the database.
        /// </summary>
        /// <param name="sqlQuery">The query to run</param>
        /// <returns>The result set.</returns>
        DataTable GetDataTable(string sqlQuery);

        /// <summary>
        /// Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sqlQuery">The query to run.</param>
        /// <returns>Number of rows updated.</returns>
        int ExecuteNonQuery(string sqlQuery);

        /// <summary>
        /// Allows the programmer to retrieve single items from the database.
        /// </summary>
        /// <param name="sqlQuery">The query to run.</param>
        /// <returns>The result.</returns>
        string ExecuteScalar(string sqlQuery);

        /// <summary>
        /// Allows the programmer to insert into the database.
        /// </summary>
        /// <param name="tableName">The table to insert the data into.</param>
        /// <param name="dataSet">A dictionary containing the column names and data for the insert.</param>
        void Insert(String tableName, Dictionary<String, String> dataSet);

        /// <summary>
        /// Allows the programmer to update rows in the database.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="dataSet">A dictionary containing column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        void Update(String tableName, Dictionary<String, String> dataSet, String where);

        /// <summary>
        /// Allows the programmer to delete rows from the database.
        /// </summary>
        /// <param name="tableName">The table to delete from.</param>
        /// <param name="where">The where clause for the delete.</param>
        void Delete(String tableName, String where);

        /// <summary>
        /// Allows the programmer to clear all data from a table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        void ClearTable(String table);
    }
}