using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{

    public class FileParserBuilder
    {
        public string FilePath { get; set; }
        public Encoding Encoding { get; set; }
        public IParseMode ParseMode { get; set; }
        public FileParserBuilder(string filePath)
        {
            this.FilePath = filePath;
        }
        public IFileParser Create()
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException("The specified file was not found.", FilePath);
            if(Encoding == null)
                Encoding = System.Text.Encoding.Default;
            if(ParseMode == null)
                ParseMode = FileParsing.ParseMode.Default();

            string extension = Path.GetExtension(FilePath);
            if (extension == ".html" || extension == ".htm")
                return new HtmlFileParser(FilePath, Encoding, ParseMode);

            return new BaseFileParser(FilePath, Encoding, ParseMode);
        }
    }
}
