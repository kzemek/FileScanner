using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface IPersistanceManager
    {
        void SaveSearch(ISearch search);
        ICollection<ISearch> GetFullHistory();
    }
}