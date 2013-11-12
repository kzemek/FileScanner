
using System;
using FileScanner.PatternMatching;

namespace FileScanner.PersistanceManager
{
    public class HistoryMach : Match
    {
        public new int Index { get; internal set; }

        public new String Value { get; internal set; }

        public new Match NextMatch()
        {
            return null;
        }

    }
}
