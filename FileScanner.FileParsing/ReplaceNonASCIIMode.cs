using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class ReplaceNonASCIIMode : BaseParseMode
    {
        public ReplaceNonASCIIMode() : base() { }
        public ReplaceNonASCIIMode(IParseMode parseMode) : base(parseMode) { }

        public override string InternalExecute(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            text = System.Text.Encoding.ASCII.GetString(bytes);
            if(parseMode!=null)
                return parseMode.Parse(text);
            return text;
        }
    }
}
