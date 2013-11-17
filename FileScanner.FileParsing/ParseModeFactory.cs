using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner.FileParser
{
    public class ParseModeFactory
    {
        public static IParseMode Default()
        {
            return new BaseParseMode();
        }
        public static IParseMode RemoveCapitalLetters()
        {
            return new RemoveCapitalLettersMode();
        }
        public static IParseMode ReplaceNonASCII()
        {
            return new ReplaceNonASCIIMode();
        }
    }
    public static class ParseModeExtensions
    {
        public static IParseMode RemoveCapitalLetters(this IParseMode parseMode)
        {
            return new RemoveCapitalLettersMode(parseMode);
        }

        public static IParseMode ReplaceNonASCII(this IParseMode parseMode)
        {
            return new ReplaceNonASCIIMode(parseMode);
        }
    }
}
