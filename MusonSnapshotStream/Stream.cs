using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MusonSnapshotStream {
	public partial class Stream : Form {

		string url;

		public Stream(string url) {
			this.url = url;

			InitializeComponent();
			UpdateSize();

			webWorker.RunWorkerAsync();
		}

		private void Form1_Resize(object sender, EventArgs e) {
			UpdateSize();
		}

		void UpdateSize() {
			this.Size = new Size(this.Size.Width, (int)(this.Size.Width / 16.0 * 9.0) + 25);
		}

		private void updateTimer_Tick(object sender, EventArgs e) {
			if(webWorker.IsBusy) return;

			webWorker.RunWorkerAsync();
		}

		private void webWorker_DoWork(object sender, DoWorkEventArgs e) {

			WebClient webClient = new WebClient();
			byte[] imageBytes = webClient.DownloadData(url);
			Image image = (Bitmap)new ImageConverter().ConvertFrom(imageBytes);

			e.Result = image;
		}

		private void webWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if(e.Error != null) {
				throw e.Error;
			}
			if(e.Result == null) {
				throw new Exception("Result is null");
			}

			Image image = (Image)e.Result;

			this.BackgroundImage = image;
			
		}
	}
}
