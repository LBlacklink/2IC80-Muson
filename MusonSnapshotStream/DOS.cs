using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace MusonSnapshotStream {
	public partial class DOS : Form {

		Process process = null;

		public DOS(string url) {
			InitializeComponent();

			//Due to strange threading limiting issues this program is exported to an .exe and run as a process
			//This allows for many many more requests to be sent per second
			//The source code for this program is listed seperately.
			string test = Directory.GetCurrentDirectory() + "\\SecurityWorkspace.exe";
			process = new Process {
				StartInfo = new ProcessStartInfo {
					FileName = Directory.GetCurrentDirectory() + "\\SecurityWorkspace.exe",
					Arguments = url,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			process.Start();
		}

		private void DOS_FormClosing(object sender, FormClosingEventArgs e) {
			//Ensure the process is closed when the form is closed
			if(process != null) {
				process.Close();
			}
		}
	}
}
