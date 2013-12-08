using System;
namespace FileScanner.PatternMatching
{
    interface IMatcher
    {
        bool IsMatch(System.IO.TextReader reader);
        Match Match(System.IO.TextReader reader);
        System.Collections.Generic.IEnumerable<Match> Matches(System.IO.TextReader reader);
    }
}
