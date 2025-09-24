namespace Transcoder
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.filesDataGridView = new System.Windows.Forms.DataGridView();
			this.doneDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.fileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.folderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.transcoderFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.goButton = new System.Windows.Forms.Button();
			this.outputTextbox = new System.Windows.Forms.TextBox();
			this.outputBrowseButton = new System.Windows.Forms.Button();
			this.outputLabel = new System.Windows.Forms.Label();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.bitrateLabel = new System.Windows.Forms.Label();
			this.bitrateNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.encoderComboBox = new System.Windows.Forms.ComboBox();
			this.fileExtensionLabel = new System.Windows.Forms.Label();
			this.fileExtensionTextBox = new System.Windows.Forms.TextBox();
			this.sampleRateLabel = new System.Windows.Forms.Label();
			this.sampleRateComboBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFileBindingSource)).BeginInit();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bitrateNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// filesDataGridView
			// 
			this.filesDataGridView.AllowUserToAddRows = false;
			this.filesDataGridView.AllowUserToOrderColumns = true;
			this.filesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filesDataGridView.AutoGenerateColumns = false;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.filesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.doneDataGridViewCheckBoxColumn,
            this.fileDataGridViewTextBoxColumn,
            this.folderDataGridViewTextBoxColumn,
            this.filePathDataGridViewTextBoxColumn});
			this.filesDataGridView.DataSource = this.transcoderFileBindingSource;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.filesDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
			this.filesDataGridView.Location = new System.Drawing.Point(16, 15);
			this.filesDataGridView.Margin = new System.Windows.Forms.Padding(4);
			this.filesDataGridView.Name = "filesDataGridView";
			this.filesDataGridView.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.filesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.filesDataGridView.RowHeadersWidth = 51;
			this.filesDataGridView.Size = new System.Drawing.Size(1450, 806);
			this.filesDataGridView.TabIndex = 0;
			// 
			// doneDataGridViewCheckBoxColumn
			// 
			this.doneDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.doneDataGridViewCheckBoxColumn.DataPropertyName = "Done";
			this.doneDataGridViewCheckBoxColumn.HeaderText = "Done";
			this.doneDataGridViewCheckBoxColumn.MinimumWidth = 6;
			this.doneDataGridViewCheckBoxColumn.Name = "doneDataGridViewCheckBoxColumn";
			this.doneDataGridViewCheckBoxColumn.ReadOnly = true;
			this.doneDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.doneDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.doneDataGridViewCheckBoxColumn.Width = 71;
			// 
			// fileDataGridViewTextBoxColumn
			// 
			this.fileDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.fileDataGridViewTextBoxColumn.DataPropertyName = "FileName";
			this.fileDataGridViewTextBoxColumn.HeaderText = "File Name";
			this.fileDataGridViewTextBoxColumn.MinimumWidth = 6;
			this.fileDataGridViewTextBoxColumn.Name = "fileDataGridViewTextBoxColumn";
			this.fileDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// folderDataGridViewTextBoxColumn
			// 
			this.folderDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.folderDataGridViewTextBoxColumn.DataPropertyName = "Folder";
			this.folderDataGridViewTextBoxColumn.HeaderText = "Folder";
			this.folderDataGridViewTextBoxColumn.MinimumWidth = 6;
			this.folderDataGridViewTextBoxColumn.Name = "folderDataGridViewTextBoxColumn";
			this.folderDataGridViewTextBoxColumn.ReadOnly = true;
			this.folderDataGridViewTextBoxColumn.Width = 77;
			// 
			// filePathDataGridViewTextBoxColumn
			// 
			this.filePathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.filePathDataGridViewTextBoxColumn.DataPropertyName = "FilePath";
			this.filePathDataGridViewTextBoxColumn.HeaderText = "File Path";
			this.filePathDataGridViewTextBoxColumn.MinimumWidth = 6;
			this.filePathDataGridViewTextBoxColumn.Name = "filePathDataGridViewTextBoxColumn";
			this.filePathDataGridViewTextBoxColumn.ReadOnly = true;
			this.filePathDataGridViewTextBoxColumn.Width = 92;
			// 
			// transcoderFileBindingSource
			// 
			this.transcoderFileBindingSource.DataSource = typeof(Transcoder.TranscoderFile);
			// 
			// goButton
			// 
			this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.goButton.Location = new System.Drawing.Point(1366, 830);
			this.goButton.Margin = new System.Windows.Forms.Padding(4);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(100, 28);
			this.goButton.TabIndex = 7;
			this.goButton.Text = "&Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// outputTextbox
			// 
			this.outputTextbox.AllowDrop = true;
			this.outputTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputTextbox.Location = new System.Drawing.Point(68, 833);
			this.outputTextbox.Margin = new System.Windows.Forms.Padding(4);
			this.outputTextbox.Name = "outputTextbox";
			this.outputTextbox.Size = new System.Drawing.Size(537, 22);
			this.outputTextbox.TabIndex = 1;
			// 
			// outputBrowseButton
			// 
			this.outputBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.outputBrowseButton.Location = new System.Drawing.Point(613, 830);
			this.outputBrowseButton.Margin = new System.Windows.Forms.Padding(4);
			this.outputBrowseButton.Name = "outputBrowseButton";
			this.outputBrowseButton.Size = new System.Drawing.Size(100, 28);
			this.outputBrowseButton.TabIndex = 2;
			this.outputBrowseButton.Text = "&Browse...";
			this.outputBrowseButton.UseVisualStyleBackColor = true;
			this.outputBrowseButton.Click += new System.EventHandler(this.outputBrowseButton_Click);
			// 
			// outputLabel
			// 
			this.outputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.outputLabel.AutoSize = true;
			this.outputLabel.Location = new System.Drawing.Point(12, 835);
			this.outputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.outputLabel.Name = "outputLabel";
			this.outputLabel.Size = new System.Drawing.Size(48, 16);
			this.outputLabel.TabIndex = 4;
			this.outputLabel.Text = "Output:";
			// 
			// statusStrip
			// 
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 864);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip.Size = new System.Drawing.Size(1482, 26);
			this.statusStrip.TabIndex = 5;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(1462, 20);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "Ready";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bitrateLabel
			// 
			this.bitrateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bitrateLabel.AutoSize = true;
			this.bitrateLabel.Location = new System.Drawing.Point(941, 836);
			this.bitrateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.bitrateLabel.Name = "bitrateLabel";
			this.bitrateLabel.Size = new System.Drawing.Size(48, 16);
			this.bitrateLabel.TabIndex = 7;
			this.bitrateLabel.Text = "Bitrate:";
			// 
			// bitrateNumericUpDown
			// 
			this.bitrateNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bitrateNumericUpDown.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.bitrateNumericUpDown.Location = new System.Drawing.Point(997, 834);
			this.bitrateNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
			this.bitrateNumericUpDown.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
			this.bitrateNumericUpDown.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this.bitrateNumericUpDown.Name = "bitrateNumericUpDown";
			this.bitrateNumericUpDown.Size = new System.Drawing.Size(60, 22);
			this.bitrateNumericUpDown.TabIndex = 4;
			this.bitrateNumericUpDown.Value = new decimal(new int[] {
            192,
            0,
            0,
            0});
			// 
			// encoderComboBox
			// 
			this.encoderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.encoderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.encoderComboBox.FormattingEnabled = true;
			this.encoderComboBox.Location = new System.Drawing.Point(721, 833);
			this.encoderComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.encoderComboBox.Name = "encoderComboBox";
			this.encoderComboBox.Size = new System.Drawing.Size(212, 24);
			this.encoderComboBox.TabIndex = 3;
			this.encoderComboBox.SelectedIndexChanged += new System.EventHandler(this.encoderComboBox_SelectedIndexChanged);
			// 
			// fileExtensionLabel
			// 
			this.fileExtensionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.fileExtensionLabel.AutoSize = true;
			this.fileExtensionLabel.Location = new System.Drawing.Point(1224, 836);
			this.fileExtensionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.fileExtensionLabel.Name = "fileExtensionLabel";
			this.fileExtensionLabel.Size = new System.Drawing.Size(68, 16);
			this.fileExtensionLabel.TabIndex = 10;
			this.fileExtensionLabel.Text = "Extension:";
			// 
			// fileExtensionTextBox
			// 
			this.fileExtensionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.fileExtensionTextBox.Location = new System.Drawing.Point(1299, 833);
			this.fileExtensionTextBox.Name = "fileExtensionTextBox";
			this.fileExtensionTextBox.Size = new System.Drawing.Size(60, 22);
			this.fileExtensionTextBox.TabIndex = 6;
			// 
			// sampleRateLabel
			// 
			this.sampleRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sampleRateLabel.AutoSize = true;
			this.sampleRateLabel.Location = new System.Drawing.Point(1065, 836);
			this.sampleRateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.sampleRateLabel.Name = "sampleRateLabel";
			this.sampleRateLabel.Size = new System.Drawing.Size(83, 16);
			this.sampleRateLabel.TabIndex = 12;
			this.sampleRateLabel.Text = "Sample rate:";
			// 
			// sampleRateComboBox
			// 
			this.sampleRateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sampleRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sampleRateComboBox.FormattingEnabled = true;
			this.sampleRateComboBox.Location = new System.Drawing.Point(1156, 833);
			this.sampleRateComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.sampleRateComboBox.Name = "sampleRateComboBox";
			this.sampleRateComboBox.Size = new System.Drawing.Size(60, 24);
			this.sampleRateComboBox.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1482, 890);
			this.Controls.Add(this.sampleRateComboBox);
			this.Controls.Add(this.sampleRateLabel);
			this.Controls.Add(this.fileExtensionTextBox);
			this.Controls.Add(this.fileExtensionLabel);
			this.Controls.Add(this.encoderComboBox);
			this.Controls.Add(this.bitrateNumericUpDown);
			this.Controls.Add(this.bitrateLabel);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.outputLabel);
			this.Controls.Add(this.outputBrowseButton);
			this.Controls.Add(this.outputTextbox);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.filesDataGridView);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "Transcoder";
			((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFileBindingSource)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bitrateNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView filesDataGridView;
		private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.TextBox outputTextbox;
		private System.Windows.Forms.Button outputBrowseButton;
		private System.Windows.Forms.Label outputLabel;
		private System.Windows.Forms.BindingSource transcoderFileBindingSource;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.DataGridViewCheckBoxColumn doneDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn folderDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
		private System.Windows.Forms.Label bitrateLabel;
		private System.Windows.Forms.NumericUpDown bitrateNumericUpDown;
		private System.Windows.Forms.ComboBox encoderComboBox;
		private System.Windows.Forms.Label fileExtensionLabel;
		private System.Windows.Forms.TextBox fileExtensionTextBox;
		private System.Windows.Forms.Label sampleRateLabel;
		private System.Windows.Forms.ComboBox sampleRateComboBox;
	}
}

