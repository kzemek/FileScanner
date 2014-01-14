namespace FileScanner
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchButton = new System.Windows.Forms.Button();
            this.searchFileLabel = new System.Windows.Forms.Label();
            this.searchFileTextBox = new System.Windows.Forms.TextBox();
            this.searchFilePickerButton = new System.Windows.Forms.Button();
            this.searchPhraseTextBox = new System.Windows.Forms.TextBox();
            this.searchPhraseLabel = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.GroupBox();
            this.searchReportBox = new System.Windows.Forms.GroupBox();
            this.exportResultsButton = new System.Windows.Forms.Button();
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.loadResultsButton = new System.Windows.Forms.Button();
            this.saveResultsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dbFilePickerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dbLocationTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dbContentListView = new System.Windows.Forms.ListView();
            this.StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Phrases = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilesCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.searchBox.SuspendLayout();
            this.searchReportBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.searchButton.Location = new System.Drawing.Point(320, 67);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchFileLabel
            // 
            this.searchFileLabel.AutoSize = true;
            this.searchFileLabel.Location = new System.Drawing.Point(6, 44);
            this.searchFileLabel.Name = "searchFileLabel";
            this.searchFileLabel.Size = new System.Drawing.Size(32, 13);
            this.searchFileLabel.TabIndex = 1;
            this.searchFileLabel.Text = "In file";
            // 
            // searchFileTextBox
            // 
            this.searchFileTextBox.Location = new System.Drawing.Point(44, 41);
            this.searchFileTextBox.Name = "searchFileTextBox";
            this.searchFileTextBox.Size = new System.Drawing.Size(321, 20);
            this.searchFileTextBox.TabIndex = 5;
            this.searchFileTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchFileTextbox_MouseClick);
            this.searchFileTextBox.TextChanged += new System.EventHandler(this.searchFileTextbox_TextChanged);
            this.searchFileTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchFileTextBox_KeyDown);
            // 
            // searchFilePickerButton
            // 
            this.searchFilePickerButton.Location = new System.Drawing.Point(371, 39);
            this.searchFilePickerButton.Name = "searchFilePickerButton";
            this.searchFilePickerButton.Size = new System.Drawing.Size(24, 23);
            this.searchFilePickerButton.TabIndex = 3;
            this.searchFilePickerButton.Text = "...";
            this.searchFilePickerButton.UseVisualStyleBackColor = true;
            this.searchFilePickerButton.Click += new System.EventHandler(this.searchFilePickerButton_Click);
            // 
            // searchPhraseTextBox
            // 
            this.searchPhraseTextBox.Location = new System.Drawing.Point(88, 13);
            this.searchPhraseTextBox.Name = "searchPhraseTextBox";
            this.searchPhraseTextBox.Size = new System.Drawing.Size(307, 20);
            this.searchPhraseTextBox.TabIndex = 5;
            this.searchPhraseTextBox.TextChanged += new System.EventHandler(this.searchPhraseTextBox_TextChanged);
            this.searchPhraseTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchPhraseTextBox_KeyDown);
            // 
            // searchPhraseLabel
            // 
            this.searchPhraseLabel.AutoSize = true;
            this.searchPhraseLabel.Location = new System.Drawing.Point(6, 16);
            this.searchPhraseLabel.Name = "searchPhraseLabel";
            this.searchPhraseLabel.Size = new System.Drawing.Size(76, 13);
            this.searchPhraseLabel.TabIndex = 4;
            this.searchPhraseLabel.Text = "Search phrase";
            // 
            // searchBox
            // 
            this.searchBox.Controls.Add(this.searchPhraseTextBox);
            this.searchBox.Controls.Add(this.searchButton);
            this.searchBox.Controls.Add(this.searchFilePickerButton);
            this.searchBox.Controls.Add(this.searchPhraseLabel);
            this.searchBox.Controls.Add(this.searchFileTextBox);
            this.searchBox.Controls.Add(this.searchFileLabel);
            this.searchBox.Location = new System.Drawing.Point(12, 9);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(403, 102);
            this.searchBox.TabIndex = 6;
            this.searchBox.TabStop = false;
            this.searchBox.Text = "Search";
            // 
            // searchReportBox
            // 
            this.searchReportBox.Controls.Add(this.exportResultsButton);
            this.searchReportBox.Controls.Add(this.resultsTextBox);
            this.searchReportBox.Location = new System.Drawing.Point(13, 117);
            this.searchReportBox.Name = "searchReportBox";
            this.searchReportBox.Size = new System.Drawing.Size(403, 402);
            this.searchReportBox.TabIndex = 7;
            this.searchReportBox.TabStop = false;
            this.searchReportBox.Text = "Results";
            // 
            // exportResultsButton
            // 
            this.exportResultsButton.Enabled = false;
            this.exportResultsButton.Location = new System.Drawing.Point(324, 373);
            this.exportResultsButton.Name = "exportResultsButton";
            this.exportResultsButton.Size = new System.Drawing.Size(73, 23);
            this.exportResultsButton.TabIndex = 3;
            this.exportResultsButton.Text = "Export to file";
            this.exportResultsButton.UseVisualStyleBackColor = true;
            this.exportResultsButton.Click += new System.EventHandler(this.exportResultsButton_Click);
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.Location = new System.Drawing.Point(6, 20);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.Size = new System.Drawing.Size(389, 347);
            this.resultsTextBox.TabIndex = 2;
            // 
            // loadResultsButton
            // 
            this.loadResultsButton.Location = new System.Drawing.Point(343, 11);
            this.loadResultsButton.Name = "loadResultsButton";
            this.loadResultsButton.Size = new System.Drawing.Size(25, 23);
            this.loadResultsButton.TabIndex = 1;
            this.loadResultsButton.Text = "...";
            this.loadResultsButton.UseVisualStyleBackColor = true;
            this.loadResultsButton.Click += new System.EventHandler(this.loadResultsButton_Click);
            // 
            // saveResultsButton
            // 
            this.saveResultsButton.Enabled = false;
            this.saveResultsButton.Location = new System.Drawing.Point(260, 41);
            this.saveResultsButton.Name = "saveResultsButton";
            this.saveResultsButton.Size = new System.Drawing.Size(108, 23);
            this.saveResultsButton.TabIndex = 0;
            this.saveResultsButton.Text = "Save last search";
            this.saveResultsButton.UseVisualStyleBackColor = true;
            this.saveResultsButton.Click += new System.EventHandler(this.saveResultsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dbFilePickerButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dbLocationTextBox);
            this.groupBox1.Controls.Add(this.saveResultsButton);
            this.groupBox1.Controls.Add(this.loadResultsButton);
            this.groupBox1.Location = new System.Drawing.Point(421, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 69);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // dbFilePickerButton
            // 
            this.dbFilePickerButton.Location = new System.Drawing.Point(380, 12);
            this.dbFilePickerButton.Name = "dbFilePickerButton";
            this.dbFilePickerButton.Size = new System.Drawing.Size(24, 23);
            this.dbFilePickerButton.TabIndex = 4;
            this.dbFilePickerButton.Text = "...";
            this.dbFilePickerButton.UseVisualStyleBackColor = true;
            this.dbFilePickerButton.Click += new System.EventHandler(this.dbFilePickerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "DB Location";
            // 
            // dbLocationTextBox
            // 
            this.dbLocationTextBox.Location = new System.Drawing.Point(78, 12);
            this.dbLocationTextBox.Name = "dbLocationTextBox";
            this.dbLocationTextBox.Size = new System.Drawing.Size(259, 20);
            this.dbLocationTextBox.TabIndex = 2;
            this.dbLocationTextBox.TextChanged += new System.EventHandler(this.dbLocationTextBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dbContentListView);
            this.groupBox2.Location = new System.Drawing.Point(422, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 434);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database content";
            // 
            // dbContentListView
            // 
            this.dbContentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StartTime,
            this.EndTime,
            this.Phrases,
            this.FilesCount});
            this.dbContentListView.Location = new System.Drawing.Point(7, 20);
            this.dbContentListView.Name = "dbContentListView";
            this.dbContentListView.Size = new System.Drawing.Size(360, 408);
            this.dbContentListView.TabIndex = 0;
            this.dbContentListView.UseCompatibleStateImageBehavior = false;
            this.dbContentListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.dbContentListView_ItemSelectionChanged);
            // 
            // StartTime
            // 
            this.StartTime.Text = "Started";
            // 
            // EndTime
            // 
            this.EndTime.Text = "Ended";
            // 
            // Phrases
            // 
            this.Phrases.Text = "Phrases";
            // 
            // FilesCount
            // 
            this.FilesCount.Text = "ProcessedFiles";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 531);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.searchReportBox);
            this.Controls.Add(this.searchBox);
            this.MaximumSize = new System.Drawing.Size(823, 570);
            this.MinimumSize = new System.Drawing.Size(823, 570);
            this.Name = "MainWindow";
            this.Text = "File Scanner";
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            this.searchReportBox.ResumeLayout(false);
            this.searchReportBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label searchFileLabel;
        private System.Windows.Forms.TextBox searchFileTextBox;
        private System.Windows.Forms.Button searchFilePickerButton;
        private System.Windows.Forms.TextBox searchPhraseTextBox;
        private System.Windows.Forms.Label searchPhraseLabel;
        private System.Windows.Forms.GroupBox searchBox;
        private System.Windows.Forms.GroupBox searchReportBox;
        private System.Windows.Forms.Button saveResultsButton;
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.Button loadResultsButton;
        private System.Windows.Forms.Button exportResultsButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button dbFilePickerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dbLocationTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView dbContentListView;
        private System.Windows.Forms.ColumnHeader StartTime;
        private System.Windows.Forms.ColumnHeader EndTime;
        private System.Windows.Forms.ColumnHeader Phrases;
        private System.Windows.Forms.ColumnHeader FilesCount;
    }
}

