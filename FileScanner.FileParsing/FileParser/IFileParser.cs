using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    public interface IFileParser
    {
        StreamReader ParseFile();
        string ParseFileToString();
    }
}
