using System;
using System.Collections;
using System.Collections.Generic;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class Search : ISearch
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int ProcessedFilesCount { get; private set; }
        public IEnumerable<string> Phrases { get; private set; }
        public IEnumerable<MatchingFile> MatchingFiles { get; private set; }

        public Search(DateTime startTime, DateTime endTime, int processedFilesCount, IEnumerable<String> phrases, IEnumerable<MatchingFile> matchingFiles)
        {
            StartTime = startTime;
            EndTime = endTime;
            ProcessedFilesCount = processedFilesCount;
            Phrases = phrases;
            MatchingFiles = matchingFiles;
        }

        public IEnumerator<MatchingFile> GetEnumerator()
        {
            return MatchingFiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
