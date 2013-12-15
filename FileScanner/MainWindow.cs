using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        // TODO: czy tych dwóch funkcji nie możnaby złączyć w jedną? np. searchTextBoxes_TextChanged(...)
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
            // TODO:
            //search.ExportResults();
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

    }
}