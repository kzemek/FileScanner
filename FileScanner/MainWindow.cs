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
        private Search search;
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
            search = new Search(searchFileTextBox.Text, searchPhraseTextBox.Text);
            resultsTextBox.Text = search.SearchResult();

            exportResultsButton.Enabled = search.IsMatch();
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
            search.ExportResults();
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
