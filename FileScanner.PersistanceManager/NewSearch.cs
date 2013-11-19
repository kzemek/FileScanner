using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class NewSearch : ISearch
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int ProcessedFilesCount { get; private set; }

        public IEnumerable<string> GetPhrases()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<MatchingFile> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
