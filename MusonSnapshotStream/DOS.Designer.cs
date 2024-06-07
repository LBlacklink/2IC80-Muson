namespace MusonSnapshotStream {
	partial class DOS {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.disableTimer = new System.Windows.Forms.Timer(this.components);
			this.UITimer = new System.Windows.Forms.Timer(this.components);
			this.logLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 34);
			this.progressBar.MarqueeAnimationSpeed = 5;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(253, 39);
			this.progressBar.Step = 30;
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Currently disabling camera...";
			// 
			// disableTimer
			// 
			this.disableTimer.Enabled = true;
			this.disableTimer.Interval = 1;
			// 
			// UITimer
			// 
			this.UITimer.Enabled = true;
			this.UITimer.Tick += new System.EventHandler(this.UITimer_Tick);
			// 
			// logLabel
			// 
			this.logLabel.AutoSize = true;
			this.logLabel.Location = new System.Drawing.Point(12, 85);
			this.logLabel.Name = "logLabel";
			this.logLabel.Size = new System.Drawing.Size(0, 13);
			this.logLabel.TabIndex = 3;
			// 
			// DOS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(277, 89);
			this.Controls.Add(this.logLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "DOS";
			this.ShowIcon = false;
			this.Text = "Disable Camera";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer disableTimer;
		private System.Windows.Forms.Timer UITimer;
		private System.Windows.Forms.Label logLabel;
	}
}
