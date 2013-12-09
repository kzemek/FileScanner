using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    class BaseFileParser : IFileParser
    {
        protected string filePath;
        protected Encoding encoding;
        protected IParseMode parseMode;

        public BaseFileParser(string filePath, Encoding encoding, IParseMode parseMode)
        {
            this.filePath = filePath;
            this.encoding = encoding;
            this.parseMode = parseMode;
        }
        protected virtual string InternalParse()
        {
            StreamReader fileReader = new StreamReader(filePath, encoding);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            fileReader.Close();
            return parsedText;
        }
        string IFileParser.ParseFileToString()
        {
            return InternalParse();
        }
        StreamReader IFileParser.ParseFile()
        {
            string parsedText = InternalParse();
            Stream streamFromString = new MemoryStream(encoding.GetBytes(parsedText));
            return new StreamReader(streamFromString);
        }
    }
}
