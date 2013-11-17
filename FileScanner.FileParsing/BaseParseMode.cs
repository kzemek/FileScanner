using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    public class BaseParseMode : IParseMode
    {
        protected IParseMode parseMode;
        public BaseParseMode(){}
        public BaseParseMode(IParseMode parseMode)
        {
            this.parseMode = parseMode;
        }
        public virtual string InternalExecute(string text)
        {
            return text;
        }
        string IParseMode.Parse(string text)
        {
            return InternalExecute(text);
        }
    }
}
