using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    /// <summary>
    /// Strategy pattern implementation component containing the action to be performed by all IParseStrategy implementations.
    /// </summary>
    public interface IParseStrategy
    {
        string Parse(string text);
    }
}
