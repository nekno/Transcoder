using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transcoder
{
	public partial class MainForm : Form
	{
		public BindingList<TranscoderFile> TranscoderFiles { get; set; }

		public MainForm() {
			InitializeComponent();

			Load += MainForm_Load;
			DragEnter += MainForm_DragEnter;
			DragDrop += MainForm_DragDrop;
		}

		void MainForm_Load(object sender, EventArgs e) {
			TranscoderFiles = new BindingList<TranscoderFile>();
			dataGridView1.DataSource = TranscoderFiles;
		}

		void MainForm_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}

		async void MainForm_DragDrop(object sender, DragEventArgs e) {
			var paths = e.Data.GetData(DataFormats.FileDrop) as String[];

			await Task.Run(() => {
				var tfiles = new List<TranscoderFile>(paths.Length * 15);

				foreach (var path in paths) {
					if (Directory.Exists(path)) {
						var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
						tfiles.AddRange(files.Select(file => new TranscoderFile() { FilePath = file, Folder = path }));
					} else if (File.Exists(path)) {
						tfiles.Add(new TranscoderFile() { FilePath = path, Folder = Path.GetDirectoryName(path) });
					}
				};

				AddFiles(tfiles);
			});
		}

		delegate void AddFilesCallback(IEnumerable<TranscoderFile> files);
		void AddFiles(IEnumerable<TranscoderFile> files) {
			if (InvokeRequired) {
				Invoke(new AddFilesCallback(AddFiles), files);
				return;
			}

			foreach (var file in files) {
				TranscoderFiles.Add(file);
			}
		}
	}
}
