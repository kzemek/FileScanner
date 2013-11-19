using System.Collections.Generic;
using System.IO;
using FileScanner.PatternMatching;

namespace FileScanner.PersistanceManager
{
    public class MatchingFile
    {
        public MatchingFile(string fileName, string fullPath, long sizeInBytes, IEnumerable<Match> matches)
        {
            FileName = fileName;
            FullPath = fullPath;
            SizeInBytes = sizeInBytes;
            Matches = matches;
        }

        public string FileName { get; private set; }

        public string FullPath { get; private set; }

        public long SizeInBytes { get; private set; }

        public IEnumerable<Match> Matches { get; private set; }
    }
}