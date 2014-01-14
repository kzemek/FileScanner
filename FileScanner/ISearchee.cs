using FileScanner.FileParsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner
{
    /// <summary>
    /// Single "Object" on which search is performed
    /// </summary>
    public interface ISearchee
    {
        /// <summary>
        /// Path to searchee object/address of searchee/something that 
        /// uniqely identifies searchee
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Provides a way to read Searchee's contents
        /// </summary>
        StreamReader Reader { get; }
    }


    internal class FileSearchee : ISearchee
    {
        public string Path { get; private set; }

        public StreamReader Reader
        {
            get { return new StreamReader(Path); }
        }


        internal FileSearchee(string path)
        {
            this.Path = path;
        }
    }


    /// <summary>
    /// File searchee that is additionally parsed
    /// </summary>
    internal class ParsedFileSearchee : ISearchee
    {
        private FileParserFactory _parserFactory;

        public string Path { get; private set; }

        public StreamReader Reader
        {
            get
            {
                var fileParser = _parserFactory.Create();
                var streamReader = fileParser.ParseFile();

                return streamReader;
            }
        }


        internal ParsedFileSearchee(string path, FileParserFactory fileParserFactory)
        {
            this.Path = path;
            this._parserFactory = fileParserFactory;
        }
    }
}
