using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    [Flags]
    public enum ParseMode
    {
        Default = 0x0,
        RemoveCapitalLetters = 0x1,
        RemoveNonASCII = 0x2, 
    }
}
