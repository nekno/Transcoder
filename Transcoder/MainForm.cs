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
		}

		void MainForm_Load(object sender, EventArgs e) {
			dataGridView1.DataSource = TranscoderFiles;
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
				for (int i = 0; i < TranscoderFiles.Count; i++ ) {
					var file = TranscoderFiles[i];
					var p = new Process() {
						StartInfo = new ProcessStartInfo() {
							FileName = Path.Combine(Environment.CurrentDirectory, @"Tools\qaac\qaac.exe"),
							Arguments = String.Format("\"{0}\" --threading -v192 -d \"{1}\"", file.FilePath, Path.Combine(outputTextbox.Text, file.Folder)),
							WindowStyle = ProcessWindowStyle.Hidden,
							CreateNoWindow = true,
							UseShellExecute = false
						}
					};

					p.EnableRaisingEvents = true;
					p.Exited += delegate(object processSender, EventArgs processEventArgs) {
						file.Done = true;
						resetTranscoderFile(i);
					};

					p.Start();
					p.WaitForExit();
				}
					/*proc = new Process();
            ProcessStartInfo pstart = new ProcessStartInfo();
            pstart.FileName = executable;
            pstart.Arguments = Commandline;
            log.LogValue("Job commandline", '"' + pstart.FileName + "\" " + pstart.Arguments);
            pstart.RedirectStandardOutput = true;
            pstart.RedirectStandardError = true;
            pstart.WindowStyle = ProcessWindowStyle.Minimized;
            pstart.CreateNoWindow = true;
            pstart.UseShellExecute = false;
            proc.StartInfo = pstart;
            proc.EnableRaisingEvents = true;
            proc.Exited += new EventHandler(proc_Exited);*/
			}, TokenSource.Token);

			setRunning(false);
		}

		void TranscoderProcess_Exited(object sender, EventArgs e)
		{
			throw new NotImplementedException();
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

		private delegate void ResetTranscoderFileCallback(int index);
		private void resetTranscoderFile(int index) {
			if (InvokeRequired) {
				Invoke(new ResetTranscoderFileCallback(resetTranscoderFile), index);
				return;
			}

			TranscoderFiles.ResetItem(index);
		}
	}
}
