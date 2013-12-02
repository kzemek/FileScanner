namespace FileScanner.SearchSummary
{
    partial class SummaryOptionsForm
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
            this.ResultFileNameCheckbox = new System.Windows.Forms.CheckBox();
            this.ResultCreationTimeCheckbox = new System.Windows.Forms.CheckBox();
            this.ResultAccessTimeCheckbox = new System.Windows.Forms.CheckBox();
            this.ResultFileSizeCheckbox = new System.Windows.Forms.CheckBox();
            this.ResultFullFilePathCheckbox = new System.Windows.Forms.CheckBox();
            this.HeaderSearchQueryCheckbox = new System.Windows.Forms.CheckBox();
            this.HeaderGenerationDateCheckbox = new System.Windows.Forms.CheckBox();
            this.HeaderInpuPathsCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResultLastModificationTime = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.OutputFilePath = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelReportButton = new System.Windows.Forms.Button();
            this.OutputFilePathLabel = new System.Windows.Forms.Label();
            this.ResultContext = new System.Windows.Forms.CheckBox();
            this.ContextSizeCharacters = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxEntries = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContextSizeCharacters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxEntries)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultFileNameCheckbox
            // 
            this.ResultFileNameCheckbox.AutoSize = true;
            this.ResultFileNameCheckbox.Checked = true;
            this.ResultFileNameCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResultFileNameCheckbox.Location = new System.Drawing.Point(6, 42);
            this.ResultFileNameCheckbox.Name = "ResultFileNameCheckbox";
            this.ResultFileNameCheckbox.Size = new System.Drawing.Size(71, 17);
            this.ResultFileNameCheckbox.TabIndex = 0;
            this.ResultFileNameCheckbox.Text = "File name";
            this.ResultFileNameCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResultCreationTimeCheckbox
            // 
            this.ResultCreationTimeCheckbox.AutoSize = true;
            this.ResultCreationTimeCheckbox.Location = new System.Drawing.Point(6, 111);
            this.ResultCreationTimeCheckbox.Name = "ResultCreationTimeCheckbox";
            this.ResultCreationTimeCheckbox.Size = new System.Drawing.Size(87, 17);
            this.ResultCreationTimeCheckbox.TabIndex = 1;
            this.ResultCreationTimeCheckbox.Text = "Creation time";
            this.ResultCreationTimeCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResultAccessTimeCheckbox
            // 
            this.ResultAccessTimeCheckbox.AutoSize = true;
            this.ResultAccessTimeCheckbox.Location = new System.Drawing.Point(6, 135);
            this.ResultAccessTimeCheckbox.Name = "ResultAccessTimeCheckbox";
            this.ResultAccessTimeCheckbox.Size = new System.Drawing.Size(105, 17);
            this.ResultAccessTimeCheckbox.TabIndex = 2;
            this.ResultAccessTimeCheckbox.Text = "Last access time";
            this.ResultAccessTimeCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResultFileSizeCheckbox
            // 
            this.ResultFileSizeCheckbox.AutoSize = true;
            this.ResultFileSizeCheckbox.Checked = true;
            this.ResultFileSizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResultFileSizeCheckbox.Location = new System.Drawing.Point(6, 88);
            this.ResultFileSizeCheckbox.Name = "ResultFileSizeCheckbox";
            this.ResultFileSizeCheckbox.Size = new System.Drawing.Size(63, 17);
            this.ResultFileSizeCheckbox.TabIndex = 3;
            this.ResultFileSizeCheckbox.Text = "File size";
            this.ResultFileSizeCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResultFullFilePathCheckbox
            // 
            this.ResultFullFilePathCheckbox.AutoSize = true;
            this.ResultFullFilePathCheckbox.Checked = true;
            this.ResultFullFilePathCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResultFullFilePathCheckbox.Location = new System.Drawing.Point(6, 65);
            this.ResultFullFilePathCheckbox.Name = "ResultFullFilePathCheckbox";
            this.ResultFullFilePathCheckbox.Size = new System.Drawing.Size(82, 17);
            this.ResultFullFilePathCheckbox.TabIndex = 4;
            this.ResultFullFilePathCheckbox.Text = "Full file path";
            this.ResultFullFilePathCheckbox.UseVisualStyleBackColor = true;
            // 
            // HeaderSearchQueryCheckbox
            // 
            this.HeaderSearchQueryCheckbox.AutoSize = true;
            this.HeaderSearchQueryCheckbox.Checked = true;
            this.HeaderSearchQueryCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HeaderSearchQueryCheckbox.Location = new System.Drawing.Point(6, 19);
            this.HeaderSearchQueryCheckbox.Name = "HeaderSearchQueryCheckbox";
            this.HeaderSearchQueryCheckbox.Size = new System.Drawing.Size(89, 17);
            this.HeaderSearchQueryCheckbox.TabIndex = 5;
            this.HeaderSearchQueryCheckbox.Text = "Search query";
            this.HeaderSearchQueryCheckbox.UseVisualStyleBackColor = true;
            // 
            // HeaderGenerationDateCheckbox
            // 
            this.HeaderGenerationDateCheckbox.AutoSize = true;
            this.HeaderGenerationDateCheckbox.Checked = true;
            this.HeaderGenerationDateCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HeaderGenerationDateCheckbox.Location = new System.Drawing.Point(6, 42);
            this.HeaderGenerationDateCheckbox.Name = "HeaderGenerationDateCheckbox";
            this.HeaderGenerationDateCheckbox.Size = new System.Drawing.Size(102, 17);
            this.HeaderGenerationDateCheckbox.TabIndex = 6;
            this.HeaderGenerationDateCheckbox.Text = "Generation date";
            this.HeaderGenerationDateCheckbox.UseVisualStyleBackColor = true;
            // 
            // HeaderInpuPathsCheckbox
            // 
            this.HeaderInpuPathsCheckbox.AutoSize = true;
            this.HeaderInpuPathsCheckbox.Checked = true;
            this.HeaderInpuPathsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HeaderInpuPathsCheckbox.Location = new System.Drawing.Point(6, 65);
            this.HeaderInpuPathsCheckbox.Name = "HeaderInpuPathsCheckbox";
            this.HeaderInpuPathsCheckbox.Size = new System.Drawing.Size(79, 17);
            this.HeaderInpuPathsCheckbox.TabIndex = 7;
            this.HeaderInpuPathsCheckbox.Text = "Input paths";
            this.HeaderInpuPathsCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MaxEntries);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ContextSizeCharacters);
            this.groupBox1.Controls.Add(this.ResultContext);
            this.groupBox1.Controls.Add(this.ResultLastModificationTime);
            this.groupBox1.Controls.Add(this.ResultFileNameCheckbox);
            this.groupBox1.Controls.Add(this.ResultCreationTimeCheckbox);
            this.groupBox1.Controls.Add(this.ResultAccessTimeCheckbox);
            this.groupBox1.Controls.Add(this.ResultFileSizeCheckbox);
            this.groupBox1.Controls.Add(this.ResultFullFilePathCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(220, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 212);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search results";
            // 
            // ResultLastModificationTime
            // 
            this.ResultLastModificationTime.AutoSize = true;
            this.ResultLastModificationTime.Location = new System.Drawing.Point(6, 158);
            this.ResultLastModificationTime.Name = "ResultLastModificationTime";
            this.ResultLastModificationTime.Size = new System.Drawing.Size(127, 17);
            this.ResultLastModificationTime.TabIndex = 5;
            this.ResultLastModificationTime.Text = "Last modification time";
            this.ResultLastModificationTime.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HeaderSearchQueryCheckbox);
            this.groupBox2.Controls.Add(this.HeaderGenerationDateCheckbox);
            this.groupBox2.Controls.Add(this.HeaderInpuPathsCheckbox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 90);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report header";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(372, 228);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(100, 23);
            this.SelectFileButton.TabIndex = 9;
            this.SelectFileButton.Text = "Select file";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // OutputFilePath
            // 
            this.OutputFilePath.Enabled = false;
            this.OutputFilePath.Location = new System.Drawing.Point(12, 230);
            this.OutputFilePath.Name = "OutputFilePath";
            this.OutputFilePath.Size = new System.Drawing.Size(354, 20);
            this.OutputFilePath.TabIndex = 10;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(226, 256);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(120, 23);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelReportButton
            // 
            this.CancelReportButton.Location = new System.Drawing.Point(352, 256);
            this.CancelReportButton.Name = "CancelReportButton";
            this.CancelReportButton.Size = new System.Drawing.Size(120, 23);
            this.CancelReportButton.TabIndex = 12;
            this.CancelReportButton.Text = "Cancel";
            this.CancelReportButton.UseVisualStyleBackColor = true;
            this.CancelReportButton.Click += new System.EventHandler(this.CancelReportButton_Click);
            // 
            // OutputFilePathLabel
            // 
            this.OutputFilePathLabel.AutoSize = true;
            this.OutputFilePathLabel.Location = new System.Drawing.Point(13, 211);
            this.OutputFilePathLabel.Name = "OutputFilePathLabel";
            this.OutputFilePathLabel.Size = new System.Drawing.Size(82, 13);
            this.OutputFilePathLabel.TabIndex = 13;
            this.OutputFilePathLabel.Text = "Output file path:";
            // 
            // ResultContext
            // 
            this.ResultContext.AutoSize = true;
            this.ResultContext.Checked = true;
            this.ResultContext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ResultContext.Location = new System.Drawing.Point(6, 181);
            this.ResultContext.Name = "ResultContext";
            this.ResultContext.Size = new System.Drawing.Size(65, 17);
            this.ResultContext.TabIndex = 6;
            this.ResultContext.Text = "Context:";
            this.ResultContext.UseVisualStyleBackColor = true;
            this.ResultContext.CheckedChanged += new System.EventHandler(this.ResultContext_CheckedChanged);
            // 
            // ContextSizeCharacters
            // 
            this.ContextSizeCharacters.Location = new System.Drawing.Point(74, 180);
            this.ContextSizeCharacters.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ContextSizeCharacters.Name = "ContextSizeCharacters";
            this.ContextSizeCharacters.Size = new System.Drawing.Size(80, 20);
            this.ContextSizeCharacters.TabIndex = 7;
            this.ContextSizeCharacters.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "characters";
            // 
            // MaxEntries
            // 
            this.MaxEntries.Location = new System.Drawing.Point(163, 19);
            this.MaxEntries.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.MaxEntries.Name = "MaxEntries";
            this.MaxEntries.Size = new System.Drawing.Size(81, 20);
            this.MaxEntries.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max result entries (0 = no limit):";
            // 
            // SummaryOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 291);
            this.ControlBox = false;
            this.Controls.Add(this.OutputFilePathLabel);
            this.Controls.Add(this.CancelReportButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.OutputFilePath);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SummaryOptionsForm";
            this.Text = "SummaryOptionsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContextSizeCharacters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxEntries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ResultFileNameCheckbox;
        private System.Windows.Forms.CheckBox ResultCreationTimeCheckbox;
        private System.Windows.Forms.CheckBox ResultAccessTimeCheckbox;
        private System.Windows.Forms.CheckBox ResultFileSizeCheckbox;
        private System.Windows.Forms.CheckBox ResultFullFilePathCheckbox;
        private System.Windows.Forms.CheckBox HeaderSearchQueryCheckbox;
        private System.Windows.Forms.CheckBox HeaderGenerationDateCheckbox;
        private System.Windows.Forms.CheckBox HeaderInpuPathsCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.TextBox OutputFilePath;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelReportButton;
        private System.Windows.Forms.CheckBox ResultLastModificationTime;
        private System.Windows.Forms.Label OutputFilePathLabel;
        private System.Windows.Forms.CheckBox ResultContext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MaxEntries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ContextSizeCharacters;
    }
}