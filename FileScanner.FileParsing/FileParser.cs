using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParsing
{
    /// <summary>
    /// Class containing static function which allow parsing of files using certain parse modes and encodings
    /// </summary>
    public class FileParser
    {
        /*
            Example of usage: 
            StreamReader s = FileParser.ParseFile(@"C:\Users\Dawid\Desktop\test.txt", 
                ParseModeFactory.RemoveCapitalLetters().ReplaceNonASCII(), Encoding.UTF8);
            Console.WriteLine(s.ReadToEnd());
            For now only tested file format is UTF8
        */
        private FileParser() { }

        /// <summary>
        /// Parses given file using given parse mode and encoding and returns a string.
        /// </summary>
        /// <param name="filePath">Path of the file to be parsed.</param>
        /// <param name="parseMode">The type of parsing to apply to the text.</param>
        /// <param name="encoding">File encoding type.</param>
        /// <returns>
        /// String containing the parsed text of the file.
        /// </returns>
        public static string ParseFileToString(string filePath, IParseMode parseMode, Encoding encoding)
        {
            StreamReader fileReader = new StreamReader(filePath, encoding);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            fileReader.Close();
            return parsedText;
        }

        #region Utility ParseFileToString Implementations
        public static string ParseFileToString(string filePath, Encoding encoding)
        {
            return ParseFileToString(filePath, ParseMode.Default(), encoding);
        }
        public static string ParseFileToString(string filePath, IParseMode parseMode)
        {
            return ParseFileToString(filePath, parseMode, Encoding.Default);
        }
        public static string ParseFileToString(string filePath)
        {
            return ParseFileToString(filePath, ParseMode.Default(), Encoding.Default);
        }
        #endregion

        /// <summary>
        /// Parses given file using given parse mode and encoding and returns a StreamReader.
        /// </summary>
        /// <param name="filePath">Path of the file to be parsed.</param>
        /// <param name="parseMode">The type of parsing to apply to the text.</param>
        /// <param name="encoding">File encoding type.</param>
        /// <returns>
        /// StreamReader initialized using the parsed text of the file.
        /// </returns>
        public static StreamReader ParseFile(string filePath, IParseMode parseMode, Encoding encoding)
        {
            string parsedText = ParseFileToString(filePath, parseMode, encoding);
            Stream streamFromString = new MemoryStream(encoding.GetBytes(parsedText));
            return new StreamReader(streamFromString);
        }

        #region Utility ParseFile Implementations
        public static StreamReader ParseFile(string filePath, Encoding encoding)
        {
            return ParseFile(filePath, ParseMode.Default(), encoding);
        }
        public static StreamReader ParseFile(string filePath, IParseMode parseMode)
        {
            return ParseFile(filePath, parseMode, Encoding.Default);
        }
        public static StreamReader ParseFile(string filePath)
        {
            return ParseFile(filePath, ParseMode.Default(), Encoding.Default);
        }
        #endregion

    }
}
