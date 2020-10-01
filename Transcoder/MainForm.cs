using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
			filesDataGridView.CellDoubleClick += filesDataGridView_CellDoubleClick;

			DragEnter += Control_DragEnter;
			DragDrop += MainForm_DragDrop;

			outputTextbox.DragEnter += Control_DragEnter;
			outputTextbox.DragDrop += outputTextbox_DragDrop;
		}

		void MainForm_Load(object sender, EventArgs e) {
			filesDataGridView.DataSource = TranscoderFiles;
			filesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			encoderComboBox.DisplayMember = "Name";
			encoderComboBox.ValueMember = "FileExtension";

			encoderComboBox.Items.AddRange(TranscoderFile.Types);

			var names = new[]
			{
				"-======Lossy Encoders======-",
				"-=====Lossless Encoders=====-",
				"-=======Lossless Copies=====-",
				"-=========Decoders========-",
				"-======Tracks/Regions======-",
			};

			encoderComboBox.Items.Insert(0, new { Name = names[0] });
			encoderComboBox.Items.Insert(encoderComboBox.Items.IndexOf(TranscoderFile.Type.QTALAC), new { Name = names[1] });
			encoderComboBox.Items.Insert(encoderComboBox.Items.IndexOf(TranscoderFile.Type.AudioCopy), new { Name = names[2] });
			encoderComboBox.Items.Insert(encoderComboBox.Items.IndexOf(TranscoderFile.Type.WAV), new { Name = names[3] });
			encoderComboBox.Items.Insert(encoderComboBox.Items.IndexOf(TranscoderFile.Type.TracksCSV), new { Name = names[4] });

			encoderComboBox.SelectedItem = TranscoderFile.Type.QTAAC;
		}

		void Control_DragEnter(object sender, DragEventArgs e) {
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}

		async void MainForm_DragDrop(object sender, DragEventArgs e) {
			var paths = e.Data.GetData(DataFormats.FileDrop) as String[];

			await Task.Run(() => {
				var tfiles = new List<TranscoderFile>(paths.Length * 30); // over-estimate 30 songs per album

				foreach (var path in paths) {
					if (Directory.Exists(path)) {
						var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
						tfiles.AddRange(files.Where(file => TranscoderFile.IsTranscodableFile(file)).Select(file => new TranscoderFile(file, path)));
					} else if (File.Exists(path) && TranscoderFile.IsTranscodableFile(path)) {
						tfiles.Add(new TranscoderFile(path));
					}
				};

				AddFiles(tfiles);
			});
		}

		void filesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var file = TranscoderFiles[e.RowIndex];

			if (file.Log.Length > 0) {
				MessageBox.Show(file.Log.ToString(), String.Format("Log: {0}", file.FilePath));
			}
		}

		void outputTextbox_DragDrop(object sender, DragEventArgs e) {
			var paths = e.Data.GetData(DataFormats.FileDrop) as String[];

			if (paths != null && Directory.Exists(paths[0])) {
				outputTextbox.Text = paths[0];
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

		void encoderComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (!(encoderComboBox.SelectedItem is TranscoderFile.Type))
                encoderComboBox.SelectedIndex++;

			var selectedType = encoderComboBox.SelectedItem as TranscoderFile.Type;
			bitrateNumericUpDown.Enabled = selectedType.IsBitrateRequired;
			fileExtensionTextBox.Enabled = selectedType.IsAudioCopy;
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

			if (String.IsNullOrWhiteSpace(outputTextbox.Text)) {
				MessageBox.Show("You must set the Output folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!(encoderComboBox.SelectedItem is TranscoderFile.Type))
            {
				MessageBox.Show("You must select a transcoding type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (bitrateNumericUpDown.Value < 64 || bitrateNumericUpDown.Value > 320) {
				MessageBox.Show("You must set a bitrate from 64-320.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (fileExtensionTextBox.Enabled && String.IsNullOrWhiteSpace(fileExtensionTextBox.Text))
            {
				MessageBox.Show("You must set a desired file extension when copying audio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			for (int i = 0; i < TranscoderFiles.Count; i++) {
				var file = TranscoderFiles[i];
				if (file.Done) {
					file.ResetFile();
					TranscoderFiles.ResetItem(i);
				}
			}
				
			setRunning(true);

			var fileIdx = 0;
			var bitrate = Convert.ToInt32(bitrateNumericUpDown.Value);
			var encoderType = encoderComboBox.SelectedItem as TranscoderFile.Type;
			var fileExtension = fileExtensionTextBox.Text;
			var outputFolder = outputTextbox.Text;
			TokenSource = new CancellationTokenSource();

			if (encoderType == TranscoderFile.Type.SplitInput)
			{
				var files = TranscoderFiles.ToList();
				fileIdx = files.Count;

				for (int i = 0; i < files.Count; i++)
				{
					selectDataGridViewRow(i);

					var file = files[i];
					IEnumerable<TranscoderFile> outputFiles = new List<TranscoderFile>();
					var ofd = new OpenFileDialog()
					{
						Title = String.Format("Select tracks/chapters for {0}", file.FileName),
						Filter = $"{Track.FileFilter}|{MatroskaChapter.FileFilter}",
						InitialDirectory = Path.GetDirectoryName(file.FilePath)
					};

					if (ofd.ShowDialog() != DialogResult.OK)
					{
						continue;
					}

					if (Path.GetExtension(ofd.FileName) == Track.FileExtension)
					{
						outputFiles = file.GetFiles(Track.GetTracks(ofd.FileName));
					}
					else if (Path.GetExtension(ofd.FileName) == MatroskaChapter.FileExtension)
                    {
						outputFiles = file.GetFiles(MatroskaChapter.GetChapters(ofd.FileName));
					}

					TranscoderFiles.Add(outputFiles);

					file.Done = true;
				}
			}

			await Task.Run(async () => {
				if (!Directory.Exists(outputFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                    catch (Exception ex)
                    {
                        updateStatus(ex.Message);
                        return;
                    }
                }

                if (encoderType.Encoder.IsEncodingRequired)
				{
					while (fileIdx < TranscoderFiles.Count)
					{
						selectDataGridViewRow(fileIdx);

						var file = TranscoderFiles[fileIdx];
						var destinationFolder = file.OutputFolderPath(outputFolder);

						if (!Directory.Exists(destinationFolder))
						{
							try
							{
								Directory.CreateDirectory(destinationFolder);
							}
							catch (Exception ex)
							{
								file.Log.AppendLine(ex.Message);
								updateStatus(ex.Message);

								if (TokenSource.IsCancellationRequested)
								{
									updateStatus("Stopped");
									return;
								}

								fileIdx++;
								continue;
							}
						}

						if (encoderType == TranscoderFile.Type.SplitInput)
							encoderType.FileExtension = Path.GetExtension(file.FileName);
						else if (encoderType == TranscoderFile.Type.AudioCopy)
							encoderType.FileExtension = fileExtension;

						using (var decoder = new Process())
						using (var encoder = new Process())
						{
							decoder.StartInfo = new ProcessStartInfo()
							{
								FileName = Path.Combine(Environment.CurrentDirectory, Encoder.FFMPEG.FilePath),
								Arguments = String.Format("-i \"{0}\" -vn -f wav {1} -", file.FilePath, TranscoderFile.Type.WAV.BitDepthArgs(file.Stream.BitDepth)),
								WindowStyle = ProcessWindowStyle.Hidden,
								CreateNoWindow = true,
								UseShellExecute = false,
								RedirectStandardInput = true,
								RedirectStandardOutput = true,
								RedirectStandardError = true
							};
							decoder.EnableRaisingEvents = true;
							decoder.ErrorDataReceived += delegate (object processSender, DataReceivedEventArgs processEventArgs)
							{
								if (processEventArgs.Data != null)
								{
									file.Log.AppendLine(String.Format("{0}", processEventArgs.Data));
								}
							};

							encoder.StartInfo = new ProcessStartInfo()
							{
								FileName = Path.Combine(Environment.CurrentDirectory, encoderType.Encoder.FilePath),
								Arguments = file.BuildCommandLineArgs(encoderType, bitrate, outputFolder),
								WindowStyle = ProcessWindowStyle.Hidden,
								CreateNoWindow = true,
								UseShellExecute = false,
								RedirectStandardInput = true,
								RedirectStandardOutput = true,
								RedirectStandardError = true
							};
							encoder.EnableRaisingEvents = true;
							encoder.OutputDataReceived += delegate (object processSender, DataReceivedEventArgs processEventArgs)
							{
								file.Log.AppendLine(String.Format("{0}", processEventArgs.Data));
								updateStatus(processEventArgs.Data);
							};
							encoder.ErrorDataReceived += delegate (object processSender, DataReceivedEventArgs processEventArgs)
							{
								if (processEventArgs.Data != null && !processEventArgs.Data.StartsWith("["))
								{
									file.Log.AppendLine(String.Format("{0}", processEventArgs.Data));
								}
								updateStatus(processEventArgs.Data);
							};
							encoder.Exited += delegate (object processSender, EventArgs processEventArgs)
							{
								file.Done = true;
								resetTranscoderFile(file);
							};

							encoder.Start();
							encoder.BeginOutputReadLine();
							encoder.BeginErrorReadLine();

							if (file.RequiresDecoding)
							{
								file.Log.AppendLine(String.Format("{0} {1}", decoder.StartInfo.FileName, decoder.StartInfo.Arguments));
								file.Log.AppendLine(String.Format("{0} {1}", encoder.StartInfo.FileName, encoder.StartInfo.Arguments));
								decoder.Start();
								decoder.BeginErrorReadLine();

								try
								{
									await decoder.StandardOutput.BaseStream.CopyToAsync(encoder.StandardInput.BaseStream, 4096, TokenSource.Token);
									await encoder.StandardInput.BaseStream.FlushAsync(TokenSource.Token);
								}
								catch (TaskCanceledException) { }

								encoder.StandardInput.Close();
							}
							else
							{
								file.Log.AppendLine(String.Format("{0} {1}", encoder.StartInfo.FileName, encoder.StartInfo.Arguments));
							}

							while (!TokenSource.IsCancellationRequested && !encoder.HasExited)
							{
								encoder.WaitForExit(500);
							}

							if (file.RequiresDecoding && !decoder.HasExited)
							{
								decoder.Kill();
							}

							if (!encoder.HasExited)
							{
								encoder.Kill();
							}

							if (encoder.ExitCode == 0 || file.RequiresDecoding || encoderType.AllowsDecoding == false)
							{
								fileIdx++; // goto next file
							}
							else
							{
								file.RequiresDecoding = true; // try again with decoding
							}
						}

						if (TokenSource.IsCancellationRequested)
						{
							updateStatus("Stopped");
							return;
						}
					}
				}
				else if (encoderType == TranscoderFile.Type.TracksCSV)
				{
					var totalDuration = new TimeSpan();

					writeCsv(Path.Combine(outputFolder, "Cutfile.csv"), (file, i, csvBuilder) =>
					{
						if (!String.IsNullOrEmpty(file.Stream.Duration))
						{
							updateStatus(String.Format("Adding file to CSV {0}, {1}", i, file.FileName));

							var duration = TimeSpan.Parse(file.Stream.Duration);
							file.StartTime = totalDuration.ToString();
							totalDuration = totalDuration.Add(duration);
							file.EndTime = totalDuration.ToString();

							csvBuilder.AppendLine(String.Join(",", (i + 1).ToString(), quoted(file.Stream.Title ?? Path.GetFileNameWithoutExtension(file.FileName)), file.StartTime, file.EndTime));
						}
						else
						{
							updateStatus(String.Format("No duration for file {0}, {1}. Skipping.", i, file.FileName));
						}
					});
				} 
				else if (encoderType == TranscoderFile.Type.RegionsCSV)
                {
					long totalSamples = 0;

					writeCsv(Path.Combine(outputFolder, "Regions.csv"), (file, i, csvBuilder) =>
					{
						if (file.Stream.Samples.HasValue)
						{
							updateStatus(String.Format("Adding file to CSV {0}, {1}", i, file.FileName));

							csvBuilder.AppendLine(String.Join(",", (i + 1).ToString(), quoted(file.Stream.Title ?? Path.GetFileNameWithoutExtension(file.FileName)), totalSamples, file.Stream.Samples));

							totalSamples += file.Stream.Samples.Value;
						}
						else
						{
							updateStatus(String.Format("No sample count for file {0}, {1}. Skipping.", i, file.FileName));
						}
					});
				}

				updateStatus("Ready");

			}, TokenSource.Token);

			setRunning(false);
		}

		private String quoted(String str)
        {
			return str.Contains(",") ? String.Format("\"{0}\"", str) : str;
		}

		private void writeCsv(string csvFilePath, Action<TranscoderFile, Int32, StringBuilder> buildCsv)
		{
			var csvBuilder = new StringBuilder();

			for (int i = 0; i < TranscoderFiles.Count; i++)
			{
				selectDataGridViewRow(i);
				var file = TranscoderFiles[i];

				buildCsv(file, i, csvBuilder);
				file.Done = true;

				if (TokenSource.IsCancellationRequested)
				{
					if (csvBuilder.Length > 0)
					{
						File.WriteAllText(csvFilePath, csvBuilder.ToString());
					}

					updateStatus("Stopped");
					return;
				}
			}

			if (csvBuilder.Length > 0)
			{
				File.WriteAllText(csvFilePath, csvBuilder.ToString());
			}
		}

		private delegate void RunningCallback(bool running);
		private void setRunning(bool running) {       
			if (InvokeRequired) {
				Invoke(new RunningCallback(setRunning), running);
				return;
			}

			Running = running;
			goButton.Text = running ? "&Stop" : "&Go";
			filesDataGridView.InvalidateColumn(0);
		}

		private delegate void SelectDataGridViewRowCallback(int index);
		private void selectDataGridViewRow(int index) {
			if (InvokeRequired) {
				Invoke(new SelectDataGridViewRowCallback(selectDataGridViewRow), index);
				return;
			}

			filesDataGridView.CurrentCell = filesDataGridView.Rows[index].Cells[0];
		}

		private delegate void ResetTranscoderFileCallback(TranscoderFile file);
		private void resetTranscoderFile(TranscoderFile file) {
			if (InvokeRequired) {
				Invoke(new ResetTranscoderFileCallback(resetTranscoderFile), file);
				return;
			}

			var index = TranscoderFiles.IndexOf(file);
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
