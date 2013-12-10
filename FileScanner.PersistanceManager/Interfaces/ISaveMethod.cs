using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISaveMethod
    {
        void Save(ISearch search);
        ICollection<ISearch> GetFullHistory();
    }

}
