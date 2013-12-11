using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    /// <summary>
    /// A basic implementation of the IFileParser interface which utilize parse strategies
    /// to parse any file. Specific file parsers are meant to derive from this class.
    /// </summary>
    class BaseFileParser : IFileParser
    {
        protected string filePath;
        protected Encoding encoding;
        protected IParseStrategy parseStrategy;

        public BaseFileParser(string filePath, Encoding encoding, IParseStrategy parseStrategy)
        {
            this.filePath = filePath;
            this.encoding = encoding;
            this.parseStrategy = parseStrategy;
        }
        protected virtual string InternalParse()
        {
            StreamReader fileReader = new StreamReader(filePath, encoding);
            string parsedText = parseStrategy.Parse(fileReader.ReadToEnd());
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
