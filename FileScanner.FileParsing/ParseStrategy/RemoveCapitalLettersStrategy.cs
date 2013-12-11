using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class RemoveCapitalLettersStrategy : BaseParseStrategy
    {
        public RemoveCapitalLettersStrategy() : base() { }
        public RemoveCapitalLettersStrategy(IParseStrategy parseStrategy) : base(parseStrategy) { }

        protected override string InternalExecute(string text)
        {
            text = text.ToLower();
            if (parseStrategy != null)
                return parseStrategy.Parse(text);
            return text;
        }
    }
}
