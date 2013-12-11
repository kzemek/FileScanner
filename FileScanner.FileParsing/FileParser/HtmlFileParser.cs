using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FileScanner.FileParsing
{
    class HtmlFileParser : BaseFileParser
    {
        public HtmlFileParser(string filePath, Encoding encoding, IParseStrategy parseStrategy) 
            : base(filePath, encoding, parseStrategy)
        {}
        protected override string InternalParse()
        {
            string rawText = base.InternalParse();
            Match m = Regex.Match(rawText, @"<body.*?>(.*?)</body.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (m.Success)
            {
                string text = m.Groups[1].ToString();
                text = Regex.Replace(text, @"<script+.*?>.*?</script+.*?>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                text = Regex.Replace(text, @"<\w+.*?>|</\w+.?>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                return text;
            }
            return "";
        }
    }
}
