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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.transcoderFilesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.filePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.folderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFilesBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filePathDataGridViewTextBoxColumn,
            this.folderDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.transcoderFilesBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(719, 269);
			this.dataGridView1.TabIndex = 0;
			// 
			// mainFormBindingSource
			// 
			this.mainFormBindingSource.DataSource = typeof(Transcoder.MainForm);
			// 
			// transcoderFilesBindingSource
			// 
			this.transcoderFilesBindingSource.DataMember = "TranscoderFiles";
			this.transcoderFilesBindingSource.DataSource = this.mainFormBindingSource;
			// 
			// filePathDataGridViewTextBoxColumn
			// 
			this.filePathDataGridViewTextBoxColumn.DataPropertyName = "FilePath";
			this.filePathDataGridViewTextBoxColumn.HeaderText = "FilePath";
			this.filePathDataGridViewTextBoxColumn.Name = "filePathDataGridViewTextBoxColumn";
			this.filePathDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// folderDataGridViewTextBoxColumn
			// 
			this.folderDataGridViewTextBoxColumn.DataPropertyName = "Folder";
			this.folderDataGridViewTextBoxColumn.HeaderText = "Folder";
			this.folderDataGridViewTextBoxColumn.Name = "folderDataGridViewTextBoxColumn";
			this.folderDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(744, 349);
			this.Controls.Add(this.dataGridView1);
			this.Name = "MainForm";
			this.Text = "Transcoder";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.transcoderFilesBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn folderDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource transcoderFilesBindingSource;
		private System.Windows.Forms.BindingSource mainFormBindingSource;
	}
}

