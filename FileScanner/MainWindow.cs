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
        public MainWindow()
        {
            InitializeComponent();
        }

        #region GUI Helper Methods

        private void _pickSearchFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchFileTextBox.Text = dialog.FileName;
                }
            }
        }

        private bool _isSearchDataProvided()
        {
            return !string.IsNullOrEmpty(searchPhraseTextBox.Text) && !string.IsNullOrEmpty(searchFileTextBox.Text);
        }

        #endregion

        #region Events

        private void searchFilePickerButton_Click(object sender, EventArgs e)
        {
            _pickSearchFile();
        }

        private void searchFileTextbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(searchFileTextBox.Text))
            {
                _pickSearchFile();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            StreamReader streamReader = FileParser.ParseFile(searchFileTextBox.Text);
            Preprocessor preprocessor = new Preprocessor();
            string phrases = preprocessor.GetNormalizedPhrase(searchPhraseTextBox.Text);
            //IEnumerable<string> variations = preprocessor.GetVariations(phrases);
            //Matcher matcher = new Matcher(variations.ToList());
            Matcher matcher = new Matcher(phrases);
            /*
            if (!matcher.IsMatch(streamReader.BaseStream))
            {
                resultsTextBox.Text = "NOOOOOOOOOOOOO!!! There are no matches for your search!";
            }
            else
            {
              */
                IEnumerable<Match> matches = matcher.Matches(streamReader.BaseStream);
                if (matches.Count() == 0)
                {
                    resultsTextBox.Text = "NOOOOOOOOOOOOO!!! There are no matches for your search!";
                }
                else
                {
                    string output = "";
                    foreach (Match m in matches)
                    {
                        output += m.Index.ToString() + " " + m.Value + Environment.NewLine;
                    }
                    resultsTextBox.Text = output;
                }
                
            //}

            //resultsTextBox.Text = isMatch.ToString();

        }

        private void searchPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = _isSearchDataProvided();
        }

        private void searchFileTextbox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = _isSearchDataProvided();
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
