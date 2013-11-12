
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileScanner.PatternMatching;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface IPersistanceManager
    {
        void SaveData(ISearch search);
        ICollection<ISearch> GetFullHistory();
        ISearch GetSearch(int id);
    }
}