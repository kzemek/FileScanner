using System;
using System.Collections;
using System.Collections.Generic;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    internal class Search : AbstractSearch
    {
        public const string EndTimeEarlierThanStartTimeExceptionMessage = "Given end time was earlier than start time.";

        public override sealed IEnumerable<string> Phrases { get; protected set; }
        private readonly IEnumerable<MatchingFile> _matchingFiles;

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
            _matchingFiles = matchingFiles;
        }

        public override IEnumerator<MatchingFile> GetEnumerator()
        {
            return _matchingFiles.GetEnumerator();
        }
    }
}
