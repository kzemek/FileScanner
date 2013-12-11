using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    public class SerializationPersistenceManager : IPersistanceManager
    {
        public String FileName { get; set; }

        public SerializationPersistenceManager(string fileName)
        {
            this.FileName = fileName;
        }

        public void SaveSearch(ISearch search)
        {
            Stream stream;
            var bformatter = new BinaryFormatter();
            ICollection<ISearch> historySearches;

            if (File.Exists(FileName))
            {
                stream = File.Open(FileName, FileMode.Open);
                historySearches = (ICollection<ISearch>)bformatter.Deserialize(stream);
                stream.Close();
                stream = File.Open(FileName, FileMode.Create);
            }
            else
            {
                stream = File.Open(FileName, FileMode.Create);
                historySearches = new Collection<ISearch>();
            }
            historySearches.Add(search);

            bformatter.Serialize(stream, historySearches);
            stream.Close();
        }

        public ICollection<ISearch> GetFullHistory()
        {
            Stream stream = File.Open(FileName, FileMode.Open);
            try
            {
                var bformatter = new BinaryFormatter();
                return (ICollection<ISearch>)bformatter.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public ISearch GetLastSearch()
        {
            return GetFullHistory().Last();
        }
    }
}
