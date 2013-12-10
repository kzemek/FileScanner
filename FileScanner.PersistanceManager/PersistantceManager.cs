using System;
using System.Collections.Generic;
using System.Linq;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class PersistanceManager : IPersistanceManager
    {
        public const string UnsupportedExtensionExceptionMessage = "File '{0}' has an unsupported extension.";

        private ISaveMethod GetSaveMethod(String fileName)
        {
            if (fileName.EndsWith(".s3db"))
            {
                return new SqLiteSaveMethod(fileName);
            }
            else if (fileName.EndsWith(".fsbin"))
            {
                return new SerializationSaveMethod(fileName);
            }
            throw new UnsupportedExtensionException(string.Format(UnsupportedExtensionExceptionMessage, fileName));
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

        public class UnsupportedExtensionException : Exception
        {
            public UnsupportedExtensionException(string message)
                : base(message)
            {
            }
        }
    }
}