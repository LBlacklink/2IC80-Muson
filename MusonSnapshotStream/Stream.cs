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

		private void Stream_Resize(object sender, EventArgs e) {
			UpdateSize();
		}

		//When resizing the Form the aspect ratio needs to be maintained which is controlled here
		void UpdateSize() {
			//Maintain 16:9 with space for the top bar
			this.Size = new Size(this.Size.Width, (int)(this.Size.Width / 16.0 * 9.0) + 25);
		}

		//Every x seconds this timer starts the webworker to fetch an image in the background
		private void updateTimer_Tick(object sender, EventArgs e) {
			//If it's still busy fetching the last one skip this update
			if(webWorker.IsBusy) return;

			webWorker.RunWorkerAsync();
		}

		//Download the image data from the listed url and save it as a result
		private void webWorker_DoWork(object sender, DoWorkEventArgs e) {

			WebClient webClient = new WebClient();
			byte[] imageBytes = webClient.DownloadData(url);
			Image image = (Bitmap)new ImageConverter().ConvertFrom(imageBytes);

			e.Result = image;
		}

		//Handle completion of the background process
		private void webWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			//Forward any potential errors
			if(e.Error != null) {
				throw e.Error;
			}
			//Result should never be null
			if(e.Result == null) {
				throw new Exception("Result is null");
			}

			Image image = (Image)e.Result;

			//The image is displayed simply as the backgroundimage of the window
			this.BackgroundImage = image;
		}
	}
}
