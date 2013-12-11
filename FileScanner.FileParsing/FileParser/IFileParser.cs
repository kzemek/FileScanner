using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    /// <summary>
    /// Main interface used to parse files.
    /// </summary>
    public interface IFileParser
    {
        StreamReader ParseFile();
        string ParseFileToString();
    }
}
