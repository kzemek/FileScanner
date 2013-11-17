using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    public class FileParser
    {
        /*
            Example of usage: 
            StreamReader s = FileParser.ParseFile(@"C:\Users\Dawid\Desktop\dupa.txt", ParseModeFactory.Default(), Encoding.UTF8);
            Console.WriteLine(s.ReadToEnd());
            For now only tested file format is UTF8
        */
        private FileParser() { }
        public static StreamReader ParseFile(string filePath, IParseMode parseMode, Encoding encoding)
        {
            StreamReader fileReader = new StreamReader(filePath, encoding);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            Stream streamFromString = new MemoryStream(encoding.GetBytes(parsedText));
            fileReader.Close();
            return new StreamReader(streamFromString);
        }
        public static string ParseFileToString(string filePath, IParseMode parseMode, Encoding encoding)
        {
            StreamReader fileReader = new StreamReader(filePath, encoding);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            fileReader.Close();
            return parsedText;
        }
        public static StreamReader ParseFile(string filePath, IParseMode parseMode)
        {
            StreamReader fileReader = new StreamReader(filePath, Encoding.Default);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            Stream streamFromString = new MemoryStream(Encoding.Default.GetBytes(parsedText));
            fileReader.Close();
            return new StreamReader(streamFromString);
        }
        public static string ParseFileToString(string filePath, IParseMode parseMode)
        {
            StreamReader fileReader = new StreamReader(filePath, Encoding.Default);
            string parsedText = parseMode.Parse(fileReader.ReadToEnd());
            fileReader.Close();
            return parsedText;
        }

    }
}
