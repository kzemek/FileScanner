using System;
using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface IPersistanceManager
    {
        void SaveSearch(ISearch search, String fileName);
        ICollection<ISearch> GetFullHistory(String fileName);
        ISearch GetLastSearch(String fileName);
    }
}