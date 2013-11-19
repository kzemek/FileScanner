using System;
using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISearch : IEnumerable<MatchingFile>
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        int ProcessedFilesCount { get; }
        IEnumerable<String> GetPhrases();
    }
}
