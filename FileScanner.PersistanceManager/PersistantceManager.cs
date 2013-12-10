﻿using System;
using System.Collections.Generic;
using System.Linq;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class PersistanceManager : IPersistanceManager
    {
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
            throw new InvalidExtensionException(string.Format("File '{0}' has an unsupported extension.", fileName));
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