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
            this.searchBox.SuspendLayout();
            this.searchReportBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.searchButton.Location = new System.Drawing.Point(501, 70);
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
            this.searchFileTextBox.Size = new System.Drawing.Size(502, 20);
            this.searchFileTextBox.TabIndex = 5;
            this.searchFileTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchFileTextbox_MouseClick);
            this.searchFileTextBox.TextChanged += new System.EventHandler(this.searchFileTextbox_TextChanged);
            this.searchFileTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchFileTextBox_KeyDown);
            // 
            // searchFilePickerButton
            // 
            this.searchFilePickerButton.Location = new System.Drawing.Point(552, 39);
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
            this.searchPhraseTextBox.Size = new System.Drawing.Size(488, 20);
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
            this.searchBox.Location = new System.Drawing.Point(13, 13);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(582, 102);
            this.searchBox.TabIndex = 6;
            this.searchBox.TabStop = false;
            this.searchBox.Text = "Search";
            // 
            // searchReportBox
            // 
            this.searchReportBox.Controls.Add(this.exportResultsButton);
            this.searchReportBox.Controls.Add(this.resultsTextBox);
            this.searchReportBox.Controls.Add(this.loadResultsButton);
            this.searchReportBox.Controls.Add(this.saveResultsButton);
            this.searchReportBox.Location = new System.Drawing.Point(13, 122);
            this.searchReportBox.Name = "searchReportBox";
            this.searchReportBox.Size = new System.Drawing.Size(582, 328);
            this.searchReportBox.TabIndex = 7;
            this.searchReportBox.TabStop = false;
            this.searchReportBox.Text = "Results";
            // 
            // exportResultsButton
            // 
            this.exportResultsButton.Enabled = false;
            this.exportResultsButton.Location = new System.Drawing.Point(9, 299);
            this.exportResultsButton.Name = "exportResultsButton";
            this.exportResultsButton.Size = new System.Drawing.Size(73, 23);
            this.exportResultsButton.TabIndex = 3;
            this.exportResultsButton.Text = "Export";
            this.exportResultsButton.UseVisualStyleBackColor = true;
            this.exportResultsButton.Click += new System.EventHandler(this.exportResultsButton_Click);
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.Location = new System.Drawing.Point(7, 20);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.Size = new System.Drawing.Size(569, 273);
            this.resultsTextBox.TabIndex = 2;
            // 
            // loadResultsButton
            // 
            this.loadResultsButton.Location = new System.Drawing.Point(422, 299);
            this.loadResultsButton.Name = "loadResultsButton";
            this.loadResultsButton.Size = new System.Drawing.Size(73, 23);
            this.loadResultsButton.TabIndex = 1;
            this.loadResultsButton.Text = "Load...";
            this.loadResultsButton.UseVisualStyleBackColor = true;
            this.loadResultsButton.Click += new System.EventHandler(this.loadResultsButton_Click);
            // 
            // saveResultsButton
            // 
            this.saveResultsButton.Location = new System.Drawing.Point(501, 299);
            this.saveResultsButton.Name = "saveResultsButton";
            this.saveResultsButton.Size = new System.Drawing.Size(75, 23);
            this.saveResultsButton.TabIndex = 0;
            this.saveResultsButton.Text = "Save";
            this.saveResultsButton.UseVisualStyleBackColor = true;
            this.saveResultsButton.Click += new System.EventHandler(this.saveResultsButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 462);
            this.Controls.Add(this.searchReportBox);
            this.Controls.Add(this.searchBox);
            this.MaximumSize = new System.Drawing.Size(623, 501);
            this.MinimumSize = new System.Drawing.Size(623, 501);
            this.Name = "MainWindow";
            this.Text = "File Scanner";
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            this.searchReportBox.ResumeLayout(false);
            this.searchReportBox.PerformLayout();
            this.ActiveControl = searchPhraseTextBox;
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
    }
}

