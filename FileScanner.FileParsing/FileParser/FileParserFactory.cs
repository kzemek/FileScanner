﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    /// <summary>
    /// A builder pattern, factory pattern crosover which allows us to get the appropriate file parser
    /// as well as specify the file path, encoding, and parse strategy.
    /// </summary>
    public class FileParserFactory
    {
        public string FilePath { get; set; }
        public Encoding Encoding { get; set; }
        public IParseStrategy ParseStrategy { get; set; }

        #region Constructors
        public FileParserFactory(string filePath)
        {
            this.FilePath = filePath;
        }
        public FileParserFactory(string filePath, Encoding encoding)
        {
            this.FilePath = filePath;
            this.Encoding = encoding;
        }
        public FileParserFactory(string filePath, IParseStrategy parseStrategy)
        {
            this.FilePath = filePath;
            this.ParseStrategy = parseStrategy;
        }
        public FileParserFactory(string filePath, Encoding encoding, IParseStrategy parseStrategy)
        {
            this.FilePath = filePath;
            this.ParseStrategy = parseStrategy;
            this.Encoding = encoding;
        }
        #endregion

        public IFileParser Create()
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException("The specified file was not found.", FilePath);
            if(Encoding == null)
                Encoding = System.Text.Encoding.Default;
            if(ParseStrategy == null)
                ParseStrategy = FileParsing.ParseStrategy.LeaveUnchanged();

            string extension = Path.GetExtension(FilePath);

            if (extension == ".html" || extension == ".htm")
                return new HtmlFileParser(FilePath, Encoding, ParseStrategy);

            return new BaseFileParser(FilePath, Encoding, ParseStrategy);
        }
    }
}
