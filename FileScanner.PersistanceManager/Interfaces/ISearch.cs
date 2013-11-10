using System;
using System.Collections.Generic;

namespace FileScanner.PersistanceManager.Interfaces
{
    public interface ISearch: IEnumerable<ISearchFile>
    {
        DateTime SearchTime { get; }
    }
}
