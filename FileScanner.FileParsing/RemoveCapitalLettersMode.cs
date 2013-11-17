using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    class RemoveCapitalLettersMode : BaseParseMode
    {
        public RemoveCapitalLettersMode() : base() { }
        public RemoveCapitalLettersMode(IParseMode parseMode) : base(parseMode) { }

        public override string InternalExecute(string text)
        {
            text = text.ToLower();
            if (parseMode != null)
                return parseMode.Parse(text);
            return text;
        }
    }
}
