using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileScanner.FileParsing;
using FileScanner.Preprocessing;
using FileScanner.PatternMatching;
using System.IO;
using FileScanner.SearchSummary;

namespace FileScanner
{
    public partial class MainWindow : Form
    {
        private const string NoMatchesFoundMessage = "NOOOOOOOOOOOOO!!! There are no matches for your search!";

        private string CurrentSearchQuery;
        private string CurrentFilePath;
        private IEnumerable<Match> CurrentMatches;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region GUI Helper Methods

        private void PickSearchFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchFileTextBox.Text = dialog.FileName;
                }
            }
        }

        private bool IsSearchDataProvided()
        {
            return !string.IsNullOrEmpty(searchPhraseTextBox.Text) && !string.IsNullOrEmpty(searchFileTextBox.Text);
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
            var streamReader = FileParser.ParseFile(searchFileTextBox.Text,ParseMode.ReplaceCapitalLetters().ReplaceNonASCII());
            var preprocessor = new PreprocessorFactory().GetIPreprocessor();
            var phrases = preprocessor.GetVariations(preprocessor.GetNormalizedPhrase(searchPhraseTextBox.Text));
            var matcher = new Matcher(phrases.ToList());
            var matches = matcher.Matches(streamReader);

            resultsTextBox.Text = matches.Any() ? BuildResults(CurrentMatches) : NoMatchesFoundMessage;

            exportResultsButton.Enabled = matches.Any();
        }


        private void searchPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = IsSearchDataProvided();
        }

        private void searchFileTextbox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = IsSearchDataProvided();
        }

        private void saveResultsButton_Click(object sender, EventArgs e)
        {
            // TODO: Save search results
        }

        private void loadResultsButton_Click(object sender, EventArgs e)
        {
            // TODO: Load search results
        }

        private void exportResultsButton_Click(object sender, EventArgs e)
        {
            if (CurrentFilePath == null || CurrentSearchQuery == null || CurrentMatches == null || !CurrentMatches.Any())
            {
                MessageBox.Show("No results to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> inputPaths = new List<string> { CurrentFilePath };
            List<MatchingFile> searchResults = new List<MatchingFile>();
            MatchingFile file;

            file.fileInfo = new FileInfo(CurrentFilePath);
            file.fileReader = FileParser.ParseFile(CurrentFilePath);
            file.accuracy = searchResults.Count;
            file.searchResults = CurrentMatches.GroupBy(match => match.Value)
                                               .ToDictionary(grouping => grouping.Key,
                                                             grouping => grouping.Select(match => match.Index));

            searchResults.Add(file);

            ISummaryGenerator generator = SummaryGeneratorFactory.Create();
            generator.Generate(CurrentSearchQuery, inputPaths, searchResults);
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

        #endregion
    }
}
