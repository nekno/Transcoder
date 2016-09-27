using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
		protected BindingList<TranscoderFile> TranscoderFiles { get; set; }
		protected CancellationTokenSource TokenSource { get; set; }
		protected bool Running { get; set; }

		public MainForm() {
			InitializeComponent();

			TranscoderFiles = new BindingList<TranscoderFile>();
			TokenSource = new CancellationTokenSource();

			Load += MainForm_Load;
			DragEnter += MainForm_DragEnter;
			DragDrop += MainForm_DragDrop;

			filesDataGridView.DragEnter += filesDataGridView_DragEnter;
			filesDataGridView.DragDrop += filesDataGridView_DragDrop;
			filesDataGridView.CellDoubleClick += filesDataGridView_CellDoubleClick;
		}

		void MainForm_Load(object sender, EventArgs e) {
			filesDataGridView.DataSource = TranscoderFiles;
			filesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		void MainForm_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}

		async void MainForm_DragDrop(object sender, DragEventArgs e) {
			var paths = e.Data.GetData(DataFormats.FileDrop) as String[];

			await Task.Run(() => {
				var tfiles = new List<TranscoderFile>(paths.Length * 10); // underestimate 10 songs per album

				foreach (var path in paths) {
					if (Directory.Exists(path)) {
						var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
						tfiles.AddRange(files.Select(file => new TranscoderFile(file, path)));
					} else if (File.Exists(path)) {
						tfiles.Add(new TranscoderFile(path));
					}
				};

				AddFiles(tfiles);
			});
		}

		void filesDataGridView_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}
		
		void filesDataGridView_DragDrop(object sender, DragEventArgs e) {
			var paths = e.Data.GetData(DataFormats.FileDrop) as String[];

			if (paths != null && Directory.Exists(paths[0])) {
				outputTextbox.Text = paths[0];
			}
		}

		void filesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var file = TranscoderFiles[e.RowIndex];

			if (!String.IsNullOrEmpty(file.Log)) {
				MessageBox.Show(file.Log, String.Format("Log: {0}", file.FilePath));
			}
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

		void outputBrowseButton_Click(object sender, EventArgs e) {
			var fbd = new FolderBrowserDialog();

			if (fbd.ShowDialog() == DialogResult.OK) {
				outputTextbox.Text = fbd.SelectedPath;
			}
		}

		async void goButton_Click(object sender, EventArgs e) {
			if (Running) {
				if (TokenSource.IsCancellationRequested) return;

				TokenSource.Token.Register(() => {
					setRunning(false);
				});

				TokenSource.Cancel();
				return;
			}

			for (int i = 0; i < TranscoderFiles.Count; i++) {
				var file = TranscoderFiles[i];
				if (file.Done) {
					file.Done = false;
					TranscoderFiles.ResetItem(i);
				}
			}
				
			setRunning(true);			

			await Task.Run(() => {
				for (int i = 0; i < TranscoderFiles.Count; i++) {
					var file = TranscoderFiles[i];

					using (var p = new Process()) {
						p.StartInfo = new ProcessStartInfo() {
							FileName = Path.Combine(Environment.CurrentDirectory, @"Tools\qaac\qaac.exe"),
							Arguments = String.Format("\"{0}\" --threading -v192 -d \"{1}\"", file.FilePath, Path.Combine(outputTextbox.Text, file.Folder)),
							WindowStyle = ProcessWindowStyle.Hidden,
							CreateNoWindow = true,
							UseShellExecute = false,
							RedirectStandardOutput = true,
							RedirectStandardError = true
						};
						p.EnableRaisingEvents = true;
						p.OutputDataReceived += delegate(object processSender, DataReceivedEventArgs processEventArgs) {
							file.Log += String.Format("{0}", processEventArgs.Data);
							updateStatus(processEventArgs.Data);
						};
						p.ErrorDataReceived += delegate(object processSender, DataReceivedEventArgs processEventArgs) {
							if (processEventArgs.Data != null && !processEventArgs.Data.StartsWith("[")) {
								file.Log += String.Format("{0}", processEventArgs.Data);
							}
							updateStatus(processEventArgs.Data);
						};
						p.Exited += delegate(object processSender, EventArgs processEventArgs) {
							file.Done = true;
							resetTranscoderFile(i);
						};

						selectDataGridViewRow(i);

						p.Start();
						p.BeginOutputReadLine();
						p.BeginErrorReadLine();

						while (!TokenSource.IsCancellationRequested && !p.HasExited) {
							p.WaitForExit(500);
						}

						if (!p.HasExited) {
							p.Kill();
						}

						if (TokenSource.IsCancellationRequested) {
							updateStatus("Stopped");
							return;
						}
					}
				}

				updateStatus("Ready");

			}, TokenSource.Token);

			setRunning(false);
		}

		private delegate void RunningCallback(bool running);
		private void setRunning(bool running) {       
			if (InvokeRequired) {
				Invoke(new RunningCallback(setRunning), running);
				return;
			}

			Running = running;
			goButton.Text = running ? "&Stop" : "&Go";
		}

		private delegate void SelectDataGridViewRowCallback(int index);
		private void selectDataGridViewRow(int index) {
			if (InvokeRequired) {
				Invoke(new SelectDataGridViewRowCallback(selectDataGridViewRow), index);
				return;
			}

			filesDataGridView.CurrentCell = filesDataGridView.Rows[index].Cells[0];
		}

		private delegate void ResetTranscoderFileCallback(int index);
		private void resetTranscoderFile(int index) {
			if (InvokeRequired) {
				Invoke(new ResetTranscoderFileCallback(resetTranscoderFile), index);
				return;
			}

			TranscoderFiles.ResetItem(index);
		}

		private delegate void UpdateStatusCallback(String status);
		private void updateStatus(String status) {
			if (InvokeRequired) {
				Invoke(new UpdateStatusCallback(updateStatus), status);
				return;
			}

			statusLabel.Text = status;
		}
	}
}
