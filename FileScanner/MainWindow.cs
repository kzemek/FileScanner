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

namespace FileScanner
{
    public partial class MainWindow : Form
    {
        private const string NoMatchesFoundMessage = "NOOOOOOOOOOOOO!!! There are no matches for your search!";

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
            var streamReader = FileParser.ParseFile(searchFileTextBox.Text);
            var preprocessor = new Preprocessor();
            var phrases = preprocessor.GetNormalizedPhrase(searchPhraseTextBox.Text);

            var matcher = new Matcher(phrases);
            var matches = matcher.Matches(streamReader);

            resultsTextBox.Text = matches.Any() ? BuildResults(matches) : NoMatchesFoundMessage;
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

        #endregion
    }
}
