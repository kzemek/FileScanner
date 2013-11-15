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
            // TODO: Launch search engine
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
