namespace MusonSnapshotStream {
	partial class Setup {
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
			this.scanButton = new System.Windows.Forms.Button();
			this.ipLabel = new System.Windows.Forms.Label();
			this.ipBox = new System.Windows.Forms.TextBox();
			this.speedTrackBar = new System.Windows.Forms.TrackBar();
			this.updateLabel = new System.Windows.Forms.Label();
			this.warningLabel = new System.Windows.Forms.Label();
			this.scanWorker = new System.ComponentModel.BackgroundWorker();
			this.startButton = new System.Windows.Forms.Button();
			this.DOSButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// scanButton
			// 
			this.scanButton.Location = new System.Drawing.Point(207, 18);
			this.scanButton.Name = "scanButton";
			this.scanButton.Size = new System.Drawing.Size(47, 22);
			this.scanButton.TabIndex = 0;
			this.scanButton.Text = "Scan";
			this.scanButton.UseVisualStyleBackColor = true;
			this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
			// 
			// ipLabel
			// 
			this.ipLabel.AutoSize = true;
			this.ipLabel.Location = new System.Drawing.Point(12, 23);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(61, 13);
			this.ipLabel.TabIndex = 1;
			this.ipLabel.Text = "IP Address:";
			// 
			// ipBox
			// 
			this.ipBox.Location = new System.Drawing.Point(79, 20);
			this.ipBox.Name = "ipBox";
			this.ipBox.Size = new System.Drawing.Size(122, 20);
			this.ipBox.TabIndex = 2;
			this.ipBox.TextChanged += new System.EventHandler(this.ipBox_TextChanged);
			// 
			// speedTrackBar
			// 
			this.speedTrackBar.Location = new System.Drawing.Point(15, 84);
			this.speedTrackBar.Minimum = 1;
			this.speedTrackBar.Name = "speedTrackBar";
			this.speedTrackBar.Size = new System.Drawing.Size(249, 45);
			this.speedTrackBar.TabIndex = 3;
			this.speedTrackBar.Value = 5;
			this.speedTrackBar.ValueChanged += new System.EventHandler(this.speedTrackBar_ValueChanged);
			// 
			// updateLabel
			// 
			this.updateLabel.AutoSize = true;
			this.updateLabel.Location = new System.Drawing.Point(12, 63);
			this.updateLabel.Name = "updateLabel";
			this.updateLabel.Size = new System.Drawing.Size(136, 13);
			this.updateLabel.TabIndex = 4;
			this.updateLabel.Text = "Update period: %x seconds";
			// 
			// warningLabel
			// 
			this.warningLabel.AutoSize = true;
			this.warningLabel.ForeColor = System.Drawing.Color.Red;
			this.warningLabel.Location = new System.Drawing.Point(12, 111);
			this.warningLabel.Name = "warningLabel";
			this.warningLabel.Size = new System.Drawing.Size(242, 13);
			this.warningLabel.TabIndex = 5;
			this.warningLabel.Text = "Warning! Low periods may cause camera freezes.";
			// 
			// scanWorker
			// 
			this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scanWorker_DoWork);
			this.scanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scanWorker_RunWorkerCompleted);
			// 
			// startButton
			// 
			this.startButton.Enabled = false;
			this.startButton.Location = new System.Drawing.Point(141, 134);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(113, 22);
			this.startButton.TabIndex = 6;
			this.startButton.Text = "Start Stream";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// DOSButton
			// 
			this.DOSButton.Enabled = false;
			this.DOSButton.Location = new System.Drawing.Point(15, 134);
			this.DOSButton.Name = "DOSButton";
			this.DOSButton.Size = new System.Drawing.Size(113, 22);
			this.DOSButton.TabIndex = 7;
			this.DOSButton.Text = "Disable Camera";
			this.DOSButton.UseVisualStyleBackColor = true;
			this.DOSButton.Click += new System.EventHandler(this.DOSButton_Click);
			// 
			// Setup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(274, 168);
			this.Controls.Add(this.DOSButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.warningLabel);
			this.Controls.Add(this.updateLabel);
			this.Controls.Add(this.speedTrackBar);
			this.Controls.Add(this.ipBox);
			this.Controls.Add(this.ipLabel);
			this.Controls.Add(this.scanButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Setup";
			this.ShowIcon = false;
			this.Text = "Setup Muson Stream";
			((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button scanButton;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.TextBox ipBox;
		private System.Windows.Forms.TrackBar speedTrackBar;
		private System.Windows.Forms.Label updateLabel;
		private System.Windows.Forms.Label warningLabel;
		private System.ComponentModel.BackgroundWorker scanWorker;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button DOSButton;
	}
}