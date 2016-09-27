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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.filesDataGridView = new System.Windows.Forms.DataGridView();
			this.fileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.goButton = new System.Windows.Forms.Button();
			this.outputTextbox = new System.Windows.Forms.TextBox();
			this.outputBrowseButton = new System.Windows.Forms.Button();
			this.outputLabel = new System.Windows.Forms.Label();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.doneDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.transcoderFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).BeginInit();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFileBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// filesDataGridView
			// 
			this.filesDataGridView.AllowUserToAddRows = false;
			this.filesDataGridView.AllowUserToOrderColumns = true;
			this.filesDataGridView.AutoGenerateColumns = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.filesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.filesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.filesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.doneDataGridViewCheckBoxColumn,
            this.fileDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn3});
			this.filesDataGridView.DataSource = this.transcoderFileBindingSource;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.filesDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
			this.filesDataGridView.Location = new System.Drawing.Point(12, 12);
			this.filesDataGridView.Name = "filesDataGridView";
			this.filesDataGridView.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.filesDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.filesDataGridView.Size = new System.Drawing.Size(818, 474);
			this.filesDataGridView.TabIndex = 0;
			// 
			// fileDataGridViewTextBoxColumn
			// 
			this.fileDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.fileDataGridViewTextBoxColumn.DataPropertyName = "FileName";
			this.fileDataGridViewTextBoxColumn.HeaderText = "File";
			this.fileDataGridViewTextBoxColumn.Name = "fileDataGridViewTextBoxColumn";
			this.fileDataGridViewTextBoxColumn.ReadOnly = true;
			this.fileDataGridViewTextBoxColumn.Width = 48;
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(755, 492);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(75, 23);
			this.goButton.TabIndex = 1;
			this.goButton.Text = "&Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// outputTextbox
			// 
			this.outputTextbox.Location = new System.Drawing.Point(57, 494);
			this.outputTextbox.Name = "outputTextbox";
			this.outputTextbox.Size = new System.Drawing.Size(440, 20);
			this.outputTextbox.TabIndex = 2;
			// 
			// outputBrowseButton
			// 
			this.outputBrowseButton.Location = new System.Drawing.Point(503, 492);
			this.outputBrowseButton.Name = "outputBrowseButton";
			this.outputBrowseButton.Size = new System.Drawing.Size(75, 23);
			this.outputBrowseButton.TabIndex = 3;
			this.outputBrowseButton.Text = "&Browse...";
			this.outputBrowseButton.UseVisualStyleBackColor = true;
			this.outputBrowseButton.Click += new System.EventHandler(this.outputBrowseButton_Click);
			// 
			// outputLabel
			// 
			this.outputLabel.AutoSize = true;
			this.outputLabel.Location = new System.Drawing.Point(9, 497);
			this.outputLabel.Name = "outputLabel";
			this.outputLabel.Size = new System.Drawing.Size(42, 13);
			this.outputLabel.TabIndex = 4;
			this.outputLabel.Text = "Output:";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 520);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(842, 22);
			this.statusStrip.TabIndex = 5;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(827, 17);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "Ready";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// doneDataGridViewCheckBoxColumn
			// 
			this.doneDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.doneDataGridViewCheckBoxColumn.DataPropertyName = "Done";
			this.doneDataGridViewCheckBoxColumn.HeaderText = "Done";
			this.doneDataGridViewCheckBoxColumn.Name = "doneDataGridViewCheckBoxColumn";
			this.doneDataGridViewCheckBoxColumn.ReadOnly = true;
			this.doneDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.doneDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.doneDataGridViewCheckBoxColumn.Width = 58;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Folder";
			this.dataGridViewTextBoxColumn4.HeaderText = "Folder";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 61;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "FilePath";
			this.dataGridViewTextBoxColumn3.HeaderText = "FilePath";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 70;
			// 
			// transcoderFileBindingSource
			// 
			this.transcoderFileBindingSource.DataSource = typeof(Transcoder.TranscoderFile);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 542);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.outputLabel);
			this.Controls.Add(this.outputBrowseButton);
			this.Controls.Add(this.outputTextbox);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.filesDataGridView);
			this.Name = "MainForm";
			this.Text = "Transcoder";
			((System.ComponentModel.ISupportInitialize)(this.filesDataGridView)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFileBindingSource)).EndInit();
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
		private System.Windows.Forms.DataGridViewCheckBoxColumn doneDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
	}
}

