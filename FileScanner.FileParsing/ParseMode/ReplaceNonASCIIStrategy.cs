using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class ReplaceNonASCIIStrategy : BaseParseStrategy
    {
        public ReplaceNonASCIIStrategy() : base() { }
        public ReplaceNonASCIIStrategy(IParseStrategy parseStrategy) : base(parseStrategy) { }

        protected override string InternalExecute(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Replace('ą', 'a')
              .Replace('ć', 'c')
              .Replace('ę', 'e')
              .Replace('ł', 'l')
              .Replace('ń', 'n')
              .Replace('ó', 'o')
              .Replace('ś', 's')
              .Replace('ż', 'z')
              .Replace('ź', 'z')
              .Replace('Ą', 'A')
              .Replace('Ć', 'C')
              .Replace('Ę', 'E')
              .Replace('Ł', 'L')
              .Replace('Ń', 'N')
              .Replace('Ó', 'O')
              .Replace('Ś', 'S')
              .Replace('Ż', 'Z')
              .Replace('Ź', 'Z');
            text = sb.ToString();
            //byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            //text = System.Text.Encoding.ASCII.GetString(bytes);
            if(parseStrategy!=null)
                return parseStrategy.Parse(text);
            return text;
        }
    }
}
