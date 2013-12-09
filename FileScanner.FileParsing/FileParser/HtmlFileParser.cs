using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class HtmlFileParser : BaseFileParser
    {
        public HtmlFileParser(string filePath, Encoding encoding, IParseMode parseMode) 
            : base(filePath, encoding, parseMode)
        {}
        protected override string InternalParse()
        {
            return base.InternalParse();
        }
    }
}
