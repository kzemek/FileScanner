using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using FileScanner.PatternMatching;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class PersistanceManager : IPersistanceManager
    {

        private ISaveMethod GetSaveMethod (String fileName)
        {
            if (fileName.EndsWith(".s3db"))
            {
                return new SqLiteSaveMethod(fileName);
            }
            else if (fileName.EndsWith(".fsbin"))
            {
                return new SerializationSaveMethod(fileName);
            }
            throw new InvalidExtensionException(string.Format("File {0} have invalid extension (.s3db is supported)", fileName));
        }


        public void SaveSearch(ISearch search, string fileName)
        {
            ISaveMethod saveMethod = GetSaveMethod(fileName);
            saveMethod.Save(search);
        }

        public ICollection<ISearch> GetFullHistory(string fileName)
        {
            ISaveMethod saveMethod = GetSaveMethod(fileName);
            return saveMethod.GetFullHistory();
        }

        public ISearch GetLastSearch(string fileName)
        {
            return this.GetFullHistory(fileName).Last();
        }

        private class InvalidExtensionException : Exception
        {
            public InvalidExtensionException(string message)
                : base(message)
            {
            }
        }
    }

}