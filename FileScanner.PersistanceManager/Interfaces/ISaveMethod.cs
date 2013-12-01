using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISaveMethod
    {
        void Save(ISearch search);
        ICollection<ISearch> GetFullHistory();

    }

}
