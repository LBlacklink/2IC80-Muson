namespace MusonSnapshotStream {
	partial class Stream {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.updateTimer = new System.Windows.Forms.Timer(this.components);
			this.webWorker = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// updateTimer
			// 
			this.updateTimer.Enabled = true;
			this.updateTimer.Interval = 5000;
			this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
			// 
			// webWorker
			// 
			this.webWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.webWorker_DoWork);
			this.webWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.webWorker_RunWorkerCompleted);
			// 
			// Stream
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ClientSize = new System.Drawing.Size(954, 537);
			this.Name = "Stream";
			this.ShowIcon = false;
			this.Text = "Stream";
			this.Resize += new System.EventHandler(this.Stream_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer updateTimer;
		private System.ComponentModel.BackgroundWorker webWorker;
	}
}

