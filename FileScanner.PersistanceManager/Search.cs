using System;
using System.Collections;
using System.Collections.Generic;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class Search : ISearch
    {
        public const string PhrasesOrMatchingFilesNullExceptionMessage = "Given phrases list or matching files list had the value 'null' where an object reference was required.";
        public const string EndTimeEarlierThanStartTimeExceptionMessage = "Given end time was earlier than start time.";

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public uint ProcessedFilesCount { get; private set; }
        public IEnumerable<string> Phrases { get; private set; }
        public IEnumerable<MatchingFile> MatchingFiles { get; private set; }

        public Search(DateTime startTime, DateTime endTime, uint processedFilesCount, IEnumerable<String> phrases, IEnumerable<MatchingFile> matchingFiles)
        {
            if (startTime.CompareTo(endTime) > 0)
            {
                throw new ArgumentException(EndTimeEarlierThanStartTimeExceptionMessage);
            }
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
