using System;
using System.Collections.Generic;
using FileScanner.PatternMatching;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISearchFile : IEnumerable<Match>
    {
        String FilePath { get; }
    }
}