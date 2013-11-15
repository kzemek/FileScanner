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
        public MainWindow()
        {
            InitializeComponent();
        }

        #region GUI Helper Methods

        private void _pickSearchPath()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    searchPathTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private bool _isSearchDataProvided()
        {
            return !string.IsNullOrEmpty(searchPhraseTextBox.Text) && !string.IsNullOrEmpty(searchPathTextBox.Text);
        }

        #endregion

        #region Events

        private void searchPathPickerButton_Click(object sender, EventArgs e)
        {
            _pickSearchPath();
        }

        private void searchPathTextbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(searchPathTextBox.Text))
            {
                _pickSearchPath();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            // TODO: Launch search engine
        }

        private void searchPhraseTextBox_TextChanged(object sender, EventArgs e)
        {
            searchButton.Enabled = _isSearchDataProvided();
        }

        private void searchPathTextbox_TextChanged(object sender, EventArgs e)
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
