using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    public interface IParseMode
    {
        string Parse(string text);
    }
}
