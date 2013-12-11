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
using System.Windows.Forms;


namespace FileScanner
{
    public class Search
    {
        private const string NoMatchesFoundMessage = "NOOOOOOOOOOOOO!!! There are no matches for your search!";
        private readonly IParseStrategy DefaultParseStrategy = ParseStrategy.ReplaceCapitalLetters().ReplaceNonASCII();

        private string _searchFile;
        private string _searchPhrase;
        private IEnumerable<Match> _matches;

        public Search(string searchFile, string searchPhrase)
        {
            this._searchFile = searchFile;
            this._searchPhrase = searchPhrase;
        }


        public string SearchResult()
        {
            var searchStartDate = DateTime.Now;

            var streamReader = GetParsedFileStream(DefaultParseStrategy);
            var phrases = GetPhrases();

            FindMatches(streamReader, phrases);
            PersistResults(searchStartDate, DateTime.Now, phrases);

            return _matches.Any() ? BuildResults(_matches) : NoMatchesFoundMessage;
        }


        private StreamReader GetParsedFileStream(IParseStrategy parseStrategy)
        {
            var fileParserBuilder = new FileParserBuilder(_searchFile, parseStrategy);

            var fileParser = fileParserBuilder.Create();
            var streamReader = fileParser.ParseFile();

            return streamReader;
        }


        private IEnumerable<string> GetPhrases()
        {
            var preprocessor = new PreprocessorFactory().GetIPreprocessor();
            var phrases = preprocessor.GetVariations(preprocessor.GetNormalizedPhrase(_searchPhrase));

            return phrases;
        }


        private void FindMatches(StreamReader streamReader, IEnumerable<string> phrases)
        {
            var matcher = new MatcherFactory().Create(phrases.ToList());
            _matches = matcher.Matches(streamReader);
        }


        private void PersistResults(DateTime startDate, DateTime endDate, IEnumerable<string> phrases)
        {
            var matchingFile = new PersistanceManager.MatchingFile(_searchFile, _searchPhrase, 0, _matches);
            var matchingFiles = new List<PersistanceManager.MatchingFile>() { matchingFile };
            var processedFilesCount = 1u;

            var storedSearch = new PersistanceManager.Search(startDate, endDate, processedFilesCount, phrases, matchingFiles);
            
            // TODO: Remove magic db name string
            var persistanceManager = new PersistanceManager.SqLitePersistanceManager("db.s3db");
            
            persistanceManager.SaveSearch(storedSearch);
        }


        public bool IsMatch()
        {
            return _matches.Any();
        }


        public void ExportResults()
        {
            var searchResults = BuildSearchSummary();

            GenerateSearchSummary(searchResults);
        }


        private List<MatchingFile> BuildSearchSummary()
        {
            // TODO: Deal with accuracy

            MatchingFile file = new MatchingFile()
            {
                accuracy = 0,
                fileInfo = new FileInfo(_searchFile),
                fileReader = new StreamReader(_searchFile,Encoding.Default),
                searchResults = _matches.GroupBy(m => m.Value).ToDictionary(g => g.Key, g => g.Select(m => m.Index))
            };

            return new List<MatchingFile> { file };
        }


        private void GenerateSearchSummary(List<MatchingFile> searchResults)
        {
            var inputPaths = new List<string> { _searchFile };
            var summaryGenerator = SummaryGeneratorFactory.Create();

            summaryGenerator.Generate(_searchPhrase, inputPaths, searchResults);
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
