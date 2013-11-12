using System;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class Phrase : IPhrase
    {
        public string PhraseText { get; internal set; }

        public new String ToString()
        {
            return PhraseText;
        }
    }
}