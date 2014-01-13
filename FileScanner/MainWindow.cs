using FileScanner.SearchSummary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileScanner
{
    public partial class MainWindow : Form
    {
        private MainWindowHelper _helper;
        private ISearchResult _lastSearchResult;

        public MainWindow()
        {
            InitializeComponent();

            _helper = new MainWindowHelper();
            _lastSearchResult = new EmptySearchResult();
        }

        #region GUI Helper Methods

        private void PickDatabaseFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchFileTextBox.Text = dialog.FileName;
                }
            }
        }


        private void PickSearchFile()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchFileTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private bool IsDatabaseProvided()
        {
            return File.Exists(dbLocationTextBox.Text);
        }

        private bool IsSearchDataProvided()
        {
            return !string.IsNullOrEmpty(searchPhraseTextBox.Text) && !string.IsNullOrEmpty(searchFileTextBox.Text)
                && (File.Exists(searchFileTextBox.Text) || Directory.Exists(searchFileTextBox.Text));
        }

        #endregion

        #region Events

        private void searchFilePickerButton_Click(object sender, EventArgs e)
        {
            PickSearchFile();
        }

        private void searchFileTextbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(searchFileTextBox.Text))
            {
                PickSearchFile();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var searchees = _helper.GetSearcheeProvider(searchFileTextBox.Text);
            var phrases = searchPhraseTextBox.Text.Split(' ');
            var searcher = _helper.Searcher;
            var resultTextGenerator = _helper.ResultTextGenerator;

            _lastSearchResult = searcher.Search(searchees, phrases);

            resultsTextBox.Text = resultTextGenerator.Generate(_lastSearchResult);
            exportResultsButton.Enabled = _lastSearchResult.Searchees.Any();
        }

        private void searchPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = IsSearchDataProvided();
        }

        private void searchFileTextbox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = IsSearchDataProvided();
        }

        private void dbFilePickerButton_Click(object sender, EventArgs e)
        {
        }

        private void saveResultsButton_Click(object sender, EventArgs e)
        {
            if (_lastSearchResult == null) return;
            _helper.PersistResults(_lastSearchResult, dbLocationTextBox.Text);
        }

        private void loadResultsButton_Click(object sender, EventArgs e)
        {
            PickDatabaseFile();
        }

        private void exportResultsButton_Click(object sender, EventArgs e)
        {
            var searchResults = _helper.BuildSearchSummary(_lastSearchResult);

            _helper.GenerateSearchSummary(searchResults, _lastSearchResult);
        }

        private void searchPhraseTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (IsSearchDataProvided()) this.searchButton.PerformClick();
                else
                    this.ActiveControl = searchFileTextBox;
        }

        private void searchFileTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (IsSearchDataProvided()) this.searchButton.PerformClick();
                else
                    this.ActiveControl = searchPhraseTextBox;
        }

        private void dbLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            saveResultsButton.Enabled = IsDatabaseProvided();
            if (IsDatabaseProvided())
            {
                var dbPath = dbLocationTextBox.Text;
                var history = new PersistanceManager.SqLitePersistanceManager(dbPath).GetFullHistory();

                foreach (var historyItem in history)
                {
                    var startTime = historyItem.StartTime.ToString();
                    var endTime = historyItem.StartTime.ToString();
                    var files = Convert.ToString(historyItem.ProcessedFilesCount);
                    var searchPhrases = String.Join(" ", historyItem.Phrases);

                    var listItem = new ListViewItem(new[] { startTime, endTime, files, searchPhrases });
                    listItem.Tag = new SearchResultAdapter(historyItem);

                    dbContentListView.Items.Add(listItem);
                }
            }


        }

        private void dbContentListView_ItemActivate(object sender, EventArgs e)
        {
            if (dbContentListView.SelectedItems.Count == 0) return;

            var historyItem = (ISearchResult)dbContentListView.SelectedItems[0].Tag;
            resultsTextBox.Text = _helper.ResultTextGenerator.Generate(_lastSearchResult);

            exportResultsButton.Enabled = false;
        }

        #endregion
    }

    internal class MainWindowHelper
    {
        private static readonly Preprocessing.IPreprocessor Preprocessor = new Preprocessing.PreprocessorFactory().GetIPreprocessor();
        private static readonly PatternMatching.MatcherFactory MatcherFactory = new PatternMatching.MatcherFactory();
        private static readonly Searcher DefaultSearcher = new Searcher(Preprocessor, MatcherFactory);
        private static readonly FileParsing.IParseStrategy ParseStrategy = FileParsing.ParseStrategy.ReplaceNonASCII();

        internal Searcher Searcher { get { return DefaultSearcher; } }
        internal IResultTextGenerator ResultTextGenerator { get { return new SimpleResultTextGenerator(); } }

        internal ISearcheeProvider GetSearcheeProvider(string rootPath)
        {
            return new ParsedFileSearcheeProvider(rootPath, ParseStrategy);
        }

        internal List<MatchingFile> BuildSearchSummary(ISearchResult result)
        {
            var q = from r in result.Searchees
                    select
                        new MatchingFile()
                        {
                            accuracy = 1,
                            fileInfo = new FileInfo(r.Searchee.Path),
                            fileReader = new StreamReader(r.Searchee.Path, Encoding.Default),
                            searchResults = r.Matches.GroupBy(m => m.Value).ToDictionary(g => g.Key, g => g.Select(m => m.Index))
                        };

            return q.ToList();
        }

        internal void GenerateSearchSummary(List<MatchingFile> searchResults, ISearchResult result)
        {
            var inputPaths = from p in result.Searchees select p.Searchee.Path;
            var summaryGenerator = SummaryGeneratorFactory.Create();
            var searchQuery = string.Join(" ", result.Phrases);

            summaryGenerator.Generate(searchQuery, inputPaths, searchResults);
        }

        internal void PersistResults(ISearchResult results, string dbPath)
        {
            var matchingFiles = new List<PersistanceManager.MatchingFile>(results.Searchees.Count());

            foreach (var hit in results.Searchees)
            {
                var info = new FileInfo(hit.Searchee.Path);
                var matchingFile =
                    new PersistanceManager.MatchingFile(info.Name, hit.Searchee.Path, info.Length, hit.Matches);

                matchingFiles.Add(matchingFile);
            }

            var storedSearch = new PersistanceManager.Search(results.StartDate, results.EndDate,
                (uint)results.ProcessedSearcheesCount, results.Phrases, matchingFiles);

            new PersistanceManager.SqLitePersistanceManager(dbPath).SaveSearch(storedSearch);
        }
    }
}