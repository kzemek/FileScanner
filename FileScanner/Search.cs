using FileScanner.FileParsing;
using FileScanner.PatternMatching;
using FileScanner.Preprocessing;
using FileScanner.SearchSummary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileScanner
{
    public class Search
    {
        private const string NoMatchesFoundMessage = "NOOOOOOOOOOOOO!!! There are no matches for your search!";

        private string searchFile;
        private string searchPhrase;
        private IEnumerable<Match> matches;

        public Search(string searchFile, string searchPhrase)
        {
            this.searchFile = searchFile;
            this.searchPhrase = searchPhrase;
        }

        public string SearchResult()
        {
            FileParserBuilder fileParserFactory = new FileParserBuilder(searchFile)
            {
                ParseMode = ParseMode.ReplaceCapitalLetters().ReplaceNonASCII()
            };
            IFileParser fileParser = fileParserFactory.Create();

            var streamReader = fileParser.ParseFile();
            var preprocessor = new PreprocessorFactory().GetIPreprocessor();
            var phrases = preprocessor.GetVariations(preprocessor.GetNormalizedPhrase(searchPhrase));
            var matcher = new Matcher(phrases.ToList());
            matches = matcher.Matches(streamReader);

            return matches.Any() ? BuildResults(matches) : NoMatchesFoundMessage;
        }

        public bool IsMatch()
        {
            return matches.Any();
        }

        public void ExportResults()
        {
            List<string> inputPaths = new List<string> { searchFile };
            List<MatchingFile> searchResults = new List<MatchingFile>();
            MatchingFile file;

            FileParserBuilder fileParserFactory = new FileParserBuilder(searchFile);
            IFileParser fileParser = fileParserFactory.Create();

            file.fileInfo = new FileInfo(searchFile);
            file.fileReader = fileParser.ParseFile();
            file.accuracy = searchResults.Count;
            file.searchResults = matches.GroupBy(match => match.Value)
                                               .ToDictionary(grouping => grouping.Key,
                                                             grouping => grouping.Select(match => match.Index));

            searchResults.Add(file);

            ISummaryGenerator generator = SummaryGeneratorFactory.Create();
            generator.Generate(searchPhrase, inputPaths, searchResults);
        }

        /// <summary>
        /// Generates human-readable format of matches.
        /// </summary>
        /// <param name="matches"></param>
        /// <returns>String of matches in format "Match.Index Match.Value", one match per line</returns>
        private string BuildResults(IEnumerable<Match> matches)
        {
            var sb = new StringBuilder();

            foreach (var m in matches)
            {
                sb.Append(m.Index).Append(' ').Append(m.Value).AppendLine();
            }

            return sb.ToString();
        }

    }
}
