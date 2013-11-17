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

namespace FileScanner.SearchSummary
{
    public struct ReportOptions
    {
        public bool headerHasSearchQuery;
        public bool headerHasGenerationDate;
        public bool headerHasInputPaths;

        public bool resultHasFileName;
        public bool resultHasFullFilePath;
        public bool resultHasFileSize;
        public bool resultHasCreationTime;
        public bool resultHasLastAccessTime;
        public bool resultHasLastModificationTime;

        public string outputFilePath;
    }

    public partial class SummaryOptionsForm : Form
    {
        public ReportOptions Options
        {
            get
            {
                ReportOptions options = new ReportOptions();

                options.headerHasSearchQuery = HeaderSearchQueryCheckbox.Checked;
                options.headerHasGenerationDate = HeaderGenerationDateCheckbox.Checked;
                options.headerHasInputPaths = HeaderInpuPathsCheckbox.Checked;

                options.resultHasFileName = ResultFileNameCheckbox.Checked;
                options.resultHasFullFilePath = ResultFullFilePathCheckbox.Checked;
                options.resultHasFileSize = ResultFileSizeCheckbox.Checked;
                options.resultHasLastAccessTime = ResultAccessTimeCheckbox.Checked;
                options.resultHasLastModificationTime = ResultLastModificationTime.Checked;

                options.outputFilePath = OutputFilePath.Text;

                return options;
            }
        }

        public SummaryOptionsForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            OutputFilePath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "report.txt");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelReportButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.CreatePrompt = false;
            dialog.DefaultExt = "txt";
            dialog.FileName = "report.txt";
            dialog.Title = "Save report as";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OutputFilePath.Text = dialog.FileName;
            }
        }
    }
}
