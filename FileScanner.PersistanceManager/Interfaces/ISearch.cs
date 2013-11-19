using System;
using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISearch: IEnumerable<SearchSummary.MatchingFile>
    {
        DateTime SearchTime { get; }
    }
}
