﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class SqLiteSaveMethod : ISaveMethod
    {
        private readonly ISQLDatabase _sqLiteDatabase;

        public SqLiteSaveMethod(ISQLDatabase sqLiteDatabase)
        {
            _sqLiteDatabase = sqLiteDatabase;
        }

        public SqLiteSaveMethod(String fileName)
        {
            _sqLiteDatabase = new SqLiteDatabase(fileName, "DatabaseStructure.sql");
        }

        public void Save(ISearch search)
        {
            var searchData = new Dictionary<string, string>
            {
                {"startTime", search.StartTime.Ticks.ToString()},
                {"endTime", search.EndTime.Ticks.ToString()},
                {"processedFilesCount", search.ProcessedFilesCount.ToString(CultureInfo.InvariantCulture)}
            };
            _sqLiteDatabase.Insert("[searches]", searchData);
            int searchID = int.Parse(_sqLiteDatabase.ExecuteScalar("SELECT seq FROM sqlite_sequence where name=\"searches\""));

            foreach (string phrase in search.Phrases)
            {
                var phraseData = new Dictionary<string, string>
                {
                    {"search_id", searchID.ToString(CultureInfo.InvariantCulture)},
                    {"phrase", phrase},
                };
                _sqLiteDatabase.Insert("[phrases]", phraseData);
            }

            foreach (MatchingFile file in search)
            {
                var fileData = new Dictionary<string, string>
                {
                    {"search_id", searchID.ToString(CultureInfo.InvariantCulture)},
                    {"fileName", file.FileName},
                    {"fullPath", file.FullPath},
                    {"sizeInBytes", file.SizeInBytes.ToString(CultureInfo.InvariantCulture)}
                };
                _sqLiteDatabase.Insert("[files]", fileData);
                int fileID = int.Parse(_sqLiteDatabase.ExecuteScalar("SELECT seq FROM sqlite_sequence where name=\"files\""));

                foreach (Match match in file.Matches)
                {
                    var matchData = new Dictionary<string, string>
                    {
                        {"file_id", fileID.ToString(CultureInfo.InvariantCulture)},
                        {"[index]", match.Index.ToString(CultureInfo.InvariantCulture)},
                        {"value", match.Value},
                    };
                    _sqLiteDatabase.Insert("[matches]", matchData);
                }
            }
        }

        public ICollection<ISearch> GetFullHistory()
        {
            DataTable dataTable = _sqLiteDatabase.GetDataTable("SELECT * FROM [searches]");
            return
                (from DataRow row in dataTable.Rows select new HistorySearch(row, _sqLiteDatabase)).Cast<ISearch>()
                    .ToList();
        }
    }
}