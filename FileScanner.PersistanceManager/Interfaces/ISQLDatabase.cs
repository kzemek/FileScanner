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
    }
}