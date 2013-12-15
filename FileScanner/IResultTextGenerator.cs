using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner
{
    /// <summary>
    /// Generates search result displayed to user
    /// </summary>
    interface IResultTextGenerator
    {
        string Generate(ISearchResult searchResults);
    }


    /// <summary>
    /// Generates search result displayed to user in a following manner:
    /// "path:
    ///  - match phrase : occurences"
    /// 
    /// for example:
    /// 
    /// C:\File1.txt:
    ///  - dog : 11 22
    ///  - cow : 33 44
    ///  
    /// C:\File2.txt:
    ///  - cow : 55
    ///  - cat : 66 77
    /// </summary>
    internal class SimpleResultTextGenerator : IResultTextGenerator
    {
        public string Generate(ISearchResult searchResults)
        {
            var sb = new StringBuilder();

            foreach (var result in searchResults.Searchees)
            {
                GenerateSearchee(sb, result);
            }

            return sb.ToString();
        }

        private void GenerateSearchee(StringBuilder sb, SearcheeResult result)
        {
            sb.Append(result.Searchee.Path).Append(':').AppendLine();

            var matchPhraseGroups = from m in result.Matches
                                    group m by m.Value into g
                                    select g;

            foreach (var phraseGroup in matchPhraseGroups)
            {
                GeneratePhrase(sb, phraseGroup);
            }

            sb.AppendLine();
        }

        private void GeneratePhrase(StringBuilder sb, IGrouping<string, PatternMatching.Match> phraseGroup)
        {
            sb.Append(" - ").Append(phraseGroup.Key).Append(" : ");

            foreach (var match in phraseGroup)
            {
                sb.Append(match.Index).Append(' ');
            }

            sb.AppendLine();
        }
    }
}

