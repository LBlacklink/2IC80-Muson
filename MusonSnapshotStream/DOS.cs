using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace MusonSnapshotStream {
	public partial class DOS : Form {

		string url;

		HttpClient httpClient;

		ulong count = 0;

		const int reqPerUpdate = 10000;

		DateTime lastUpdate = DateTime.Now;

		Process process;

		public DOS(string url) {
			this.url = url;
			httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

			InitializeComponent();

			process = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = "C:\\Users\\lboij\\source\\repos\\MusonSnapshotStream\\MusonSnapshotStream\\SecurityWorkspace.exe",
					Arguments = url,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			process.Start();
		}

		private void UITimer_Tick(object sender, EventArgs e) {
			
		}
	}
}
