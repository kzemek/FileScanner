using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using FileScanner.PersistanceManager.Interfaces;

namespace FileScanner.PersistanceManager
{
    class SerializationSaveMethod: ISaveMethod
    {
        private readonly String _fileName;

        public SerializationSaveMethod(string fileName)
        {
            this._fileName = fileName;
        }

        public void Save(ISearch search)
        {

            Stream stream;
            var bformatter = new BinaryFormatter();
            ICollection<ISearch> historySearches;

            if (File.Exists(_fileName))
            {
                stream = File.Open(_fileName, FileMode.Open);
                historySearches = (ICollection<ISearch>) bformatter.Deserialize(stream);
            }
            else
            {
                stream = File.Open(_fileName, FileMode.Create);
                historySearches = new Collection<ISearch>();
            }
            historySearches.Add(search);

            bformatter.Serialize(stream, historySearches);
            stream.Close();
        }

        public ICollection<ISearch> GetFullHistory()
        {
            Stream stream = File.Open(_fileName, FileMode.Open);
            try
            {
                var bformatter = new BinaryFormatter();
                return (ICollection<ISearch>) bformatter.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
