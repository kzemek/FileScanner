using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    public class FileParser
    {
        public FileParser() { }
        public StreamReader ParseFile(string filePath, ParseMode parseMode)
        {
            return new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

        }
    }
}
