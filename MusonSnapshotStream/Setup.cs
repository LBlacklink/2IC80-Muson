using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MusonSnapshotStream {
	public partial class Setup : Form {
		public Setup() {
			InitializeComponent();

			UpdatePeriod();
		}

		private void scanButton_Click(object sender, EventArgs e) {

			ipBox.Enabled = false;
			scanButton.Enabled = false;
			ipBox.Text = "Scanning...";

			scanWorker.RunWorkerAsync();
		}

		private void scanWorker_DoWork(object sender, DoWorkEventArgs e) {
			string localIPBase = GetLocalIPAddressBase();

			List<Task<bool>> tasks = new List<Task<bool>>();
			for (int i = 1; i < 255; i++)
				tasks.Add(PingHostAsync($"{localIPBase}.{i}"));

			Task<bool[]> batch = Task.WhenAll(tasks);
			batch.Wait();

			List<string> IPs = new List<string>();
			for (int i = 0; i < tasks.Count; i++) {
				if (batch.Result[i]) {
					IPs.Add(localIPBase + "." + i.ToString());
				}
			}

			tasks = new List<Task<bool>>();
			foreach (string IP in IPs)
				tasks.Add(GetSnapshotAsync(IP));

			batch = Task.WhenAll(tasks);
			batch.Wait();

			List<string> camIPs = new List<string>();
			for (int i = 0; i < tasks.Count; i++) {
				if (batch.Result[i]) {
					camIPs.Add(IPs[i]);
				}
			}

			if(camIPs.Count > 0)
				e.Result = camIPs[0];
			else
				e.Result = null;
		}

		static string GetLocalIPAddressBase() {
			string localIP = string.Empty;
			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)) {
				socket.Connect("8.8.8.8", 65530);
				IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
				localIP = endPoint.Address.ToString();
			}

			string[] ipParts = localIP.Split('.');
			return $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}";
		}

		static async Task<bool> PingHostAsync(string IP) {
			bool pingable = false;
			using (Ping pinger = new Ping()) {
				try {
					PingReply pingReply = await pinger.SendPingAsync(IP, 3000);
					pingable = pingReply.Status == IPStatus.Success;
				} catch (PingException) {

				}
			}
			return pingable;
		}

		static async Task<bool> GetSnapshotAsync(string IP) {
			string url = "http://" + IP + ":8000/api/v1/snap.cgi?chn=0";

			try {
				WebClient webClient = new WebClient();
				byte[] imageBytes = await webClient.DownloadDataTaskAsync(url);

				return imageBytes.Length > 0;
			} catch (Exception) {
				//We take any exception as connection failure
				return false;
			}
		}

		private void scanWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if(e.Error != null) {
				throw e.Error;
			}
			

			ipBox.Enabled = true;
			scanButton.Enabled = true;

			if (e.Result == null) {
				//No camera found
				ipBox.Text = "No camera found.";
			} else {
				string ip = e.Result as string ?? throw new Exception("scanWorker result was unexpectedly not a string");
				ipBox.Text = ip;
			}
		}

		private void speedTrackBar_ValueChanged(object sender, EventArgs e) {
			UpdatePeriod();
		}

		private void UpdatePeriod() {
			updateLabel.Text = $"Update period: {speedTrackBar.Value} seconds";

			warningLabel.Visible = speedTrackBar.Value < 5;
		}

		private void ipBox_TextChanged(object sender, EventArgs e) {
			string text = ipBox.Text;

			bool success = true;
			try {
				string[] parts = text.Split('.');

				if (parts.Length != 4) throw new Exception();

				foreach(string part in parts) {
					int num = int.Parse(part);
					
					if(num < 0 || num > 255) {
						throw new Exception();
					}
				}
			} catch (Exception) {
				success = false;
			}

			startButton.Enabled = success;
			DOSButton.Enabled = success;
		}

		private void startButton_Click(object sender, EventArgs e) {
			string url = "http://" + ipBox.Text + ":8000/api/v1/snap.cgi?chn=0";

			Stream stream = new Stream(url);
			stream.FormClosing += delegate { this.Show(); };
			stream.Show();
			this.Hide();
		}

		private void DOSButton_Click(object sender, EventArgs e) {
			string url = "http://" + ipBox.Text + "/api/v1/lan-probe/";

			DOS dos = new DOS(url);
			dos.FormClosing += delegate { this.Show(); };
			dos.Show();
			this.Hide();
		}
	}
}
