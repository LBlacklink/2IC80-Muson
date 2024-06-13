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
			//Disable UI elements
			ipBox.Enabled = false;
			scanButton.Enabled = false;
			ipBox.Text = "Scanning...";

			//Start background process
			scanWorker.RunWorkerAsync();
		}

		//Run the scanning process in the background
		private void scanWorker_DoWork(object sender, DoWorkEventArgs e) {
			//Get the base e.g. 192.168.88.
			string localIPBase = GetLocalIPAddressBase();

			//Create tasks for the pings and add pings for all addresses on LAN
			List<Task<bool>> tasks = new List<Task<bool>>();
			for (int i = 1; i < 255; i++)
				tasks.Add(PingHostAsync($"{localIPBase}.{i}"));

			//Run all tasks simultaneously and wait for them to finish
			Task<bool[]> batch = Task.WhenAll(tasks);
			batch.Wait();

			//Get all IPs by listing all the IPs related to succesful ping tasks
			List<string> IPs = new List<string>();
			for (int i = 0; i < tasks.Count; i++) {
				if (batch.Result[i]) {
					IPs.Add(localIPBase + "." + i.ToString());
				}
			}

			//Create new tasks for getting a snapshot from the camera
			tasks = new List<Task<bool>>();
			foreach (string IP in IPs)
				tasks.Add(GetSnapshotAsync(IP));

			//Again run all simultaneously
			batch = Task.WhenAll(tasks);
			batch.Wait();

			//All tasks that succesfully gave an image their IPs are listed
			List<string> camIPs = new List<string>();
			for (int i = 0; i < tasks.Count; i++) {
				if (batch.Result[i]) {
					camIPs.Add(IPs[i]);
				}
			}

			//Returns the first found IP or null if none found
			if(camIPs.Count > 0)
				e.Result = camIPs[0];
			else
				e.Result = null;
		}

		//Get the first part of the local IP address which equates the local network
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

		//Try pinging URL returns if responded
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

		//Try downloading a snapshot from URL at the default location, returns success boolean.
		static async Task<bool> GetSnapshotAsync(string IP) {
			//This port is currently hardcoded
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

		//Handle the result from the backgroundthread for the scan
		private void scanWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			//Repeat potential errors
			if(e.Error != null) {
				throw e.Error;
			}

			//Reenable the UI components
			ipBox.Enabled = true;
			scanButton.Enabled = true;

			if (e.Result == null) {
				//No camera found
				ipBox.Text = "No camera found.";
			} else {
				//Put the text in the ipbox
				string ip = e.Result as string ?? throw new Exception("scanWorker result was unexpectedly not a string");
				ipBox.Text = ip;
			}
		}

		private void speedTrackBar_ValueChanged(object sender, EventArgs e) {
			UpdatePeriod();
		}

		//Update (visually) the set refresh period 
		private void UpdatePeriod() {
			//Change the text and warn users if the period is too low
			updateLabel.Text = $"Update period: {speedTrackBar.Value} seconds";

			warningLabel.Visible = speedTrackBar.Value < 5;
		}

		private void ipBox_TextChanged(object sender, EventArgs e) {
			//Check if the text in the ipbox is a valid ip address
			string text = ipBox.Text;

			bool success = true;
			try {
				//Split address into 4 parts and check if each is between 0 and 255
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

			//If the text is a valid ip address enable the buttons to continue
			startButton.Enabled = success;
			DOSButton.Enabled = success;
		}

		private void startButton_Click(object sender, EventArgs e) {
			//This IP is now hardcoded and passed to the stream
			string url = "http://" + ipBox.Text + ":8000/api/v1/snap.cgi?chn=0";

			//Start the Stream Form that does the rest of the work
			Stream stream = new Stream(url);
			stream.FormClosing += delegate { this.Show(); };
			stream.Show();
			this.Hide();
		}

		private void DOSButton_Click(object sender, EventArgs e) {
			//This IP address should be constant on all compatible cameras
			string url = "http://" + ipBox.Text + "/api/v1/lan-probe/";

			//Start the DOS Form that does the rest of the work
			DOS dos = new DOS(url);
			dos.FormClosing += delegate { this.Show(); };
			dos.Show();
			this.Hide();
		}
	}
}
