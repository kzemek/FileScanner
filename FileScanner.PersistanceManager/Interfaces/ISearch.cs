using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISearch : IEnumerable<MatchingFile>
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        uint ProcessedFilesCount { get; }
        IEnumerable<String> Phrases { get; }
    }
}
