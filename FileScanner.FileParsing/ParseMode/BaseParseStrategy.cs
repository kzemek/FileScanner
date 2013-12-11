using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class BaseParseStrategy : IParseStrategy
    {
        protected IParseStrategy parseStrategy;
        public BaseParseStrategy(){}
        public BaseParseStrategy(IParseStrategy parseStrategy)
        {
            this.parseStrategy = parseStrategy;
        }
        protected virtual string InternalExecute(string text)
        {
            return text;
        }
        string IParseStrategy.Parse(string text)
        {
            return InternalExecute(text);
        }
    }
}
