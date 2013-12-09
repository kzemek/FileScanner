using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    interface ISaveMethod
    {
        void Save(ISearch search);
        ICollection<ISearch> GetFullHistory();
    }

}
