namespace BedLeveler
{
	partial class BedLeveler
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BedLeveler));
			this.portText = new System.Windows.Forms.TextBox();
			this.connectButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.measureCornersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setZ50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.x3PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x5PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x9PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x12PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x17PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x19PatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.outputText = new System.Windows.Forms.TextBox();
			this.disconnectButton = new System.Windows.Forms.Button();
			this.bedPicture = new System.Windows.Forms.PictureBox();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.setLowerBoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setUpperBoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bedPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// portText
			// 
			this.portText.Location = new System.Drawing.Point(12, 12);
			this.portText.Name = "portText";
			this.portText.Size = new System.Drawing.Size(100, 20);
			this.portText.TabIndex = 0;
			this.portText.Text = "COM5";
			// 
			// connectButton
			// 
			this.connectButton.Location = new System.Drawing.Point(118, 10);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(75, 23);
			this.connectButton.TabIndex = 1;
			this.connectButton.Text = "&Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 1013);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1280, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.measureCornersToolStripMenuItem,
            this.setZ50ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.x3PatternToolStripMenuItem,
            this.x5PatternToolStripMenuItem,
            this.x9PatternToolStripMenuItem,
            this.x12PatternToolStripMenuItem,
            this.x17PatternToolStripMenuItem,
            this.x19PatternToolStripMenuItem,
            this.toolStripMenuItem2,
            this.setLowerBoundToolStripMenuItem,
            this.setUpperBoundToolStripMenuItem});
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
			this.toolStripSplitButton1.Text = "toolStripSplitButton1";
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.clearToolStripMenuItem.Text = "Clear and Re-center";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearClicked);
			// 
			// measureCornersToolStripMenuItem
			// 
			this.measureCornersToolStripMenuItem.Name = "measureCornersToolStripMenuItem";
			this.measureCornersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.measureCornersToolStripMenuItem.Text = "Measure corners";
			this.measureCornersToolStripMenuItem.Click += new System.EventHandler(this.MeasureCorners);
			// 
			// setZ50ToolStripMenuItem
			// 
			this.setZ50ToolStripMenuItem.Name = "setZ50ToolStripMenuItem";
			this.setZ50ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.setZ50ToolStripMenuItem.Text = "Set Z=5.0";
			this.setZ50ToolStripMenuItem.Click += new System.EventHandler(this.SetZto5);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
			// 
			// x3PatternToolStripMenuItem
			// 
			this.x3PatternToolStripMenuItem.Name = "x3PatternToolStripMenuItem";
			this.x3PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x3PatternToolStripMenuItem.Tag = "3";
			this.x3PatternToolStripMenuItem.Text = "3x3 pattern";
			this.x3PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// x5PatternToolStripMenuItem
			// 
			this.x5PatternToolStripMenuItem.Name = "x5PatternToolStripMenuItem";
			this.x5PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x5PatternToolStripMenuItem.Tag = "5";
			this.x5PatternToolStripMenuItem.Text = "5x5 pattern";
			this.x5PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// x9PatternToolStripMenuItem
			// 
			this.x9PatternToolStripMenuItem.Name = "x9PatternToolStripMenuItem";
			this.x9PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x9PatternToolStripMenuItem.Tag = "7";
			this.x9PatternToolStripMenuItem.Text = "7x7 pattern";
			this.x9PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// x12PatternToolStripMenuItem
			// 
			this.x12PatternToolStripMenuItem.Name = "x12PatternToolStripMenuItem";
			this.x12PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x12PatternToolStripMenuItem.Tag = "11";
			this.x12PatternToolStripMenuItem.Text = "11x11 pattern";
			this.x12PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// x17PatternToolStripMenuItem
			// 
			this.x17PatternToolStripMenuItem.Name = "x17PatternToolStripMenuItem";
			this.x17PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x17PatternToolStripMenuItem.Tag = "17";
			this.x17PatternToolStripMenuItem.Text = "17x17 pattern";
			this.x17PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// x19PatternToolStripMenuItem
			// 
			this.x19PatternToolStripMenuItem.Name = "x19PatternToolStripMenuItem";
			this.x19PatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.x19PatternToolStripMenuItem.Tag = "19";
			this.x19PatternToolStripMenuItem.Text = "19x19 pattern";
			this.x19PatternToolStripMenuItem.Click += new System.EventHandler(this.GeneratePattern);
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(86, 17);
			this.statusLabel.Text = "Not connected";
			// 
			// outputText
			// 
			this.outputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.outputText.Location = new System.Drawing.Point(12, 38);
			this.outputText.Multiline = true;
			this.outputText.Name = "outputText";
			this.outputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.outputText.Size = new System.Drawing.Size(262, 972);
			this.outputText.TabIndex = 3;
			// 
			// disconnectButton
			// 
			this.disconnectButton.Enabled = false;
			this.disconnectButton.Location = new System.Drawing.Point(199, 10);
			this.disconnectButton.Name = "disconnectButton";
			this.disconnectButton.Size = new System.Drawing.Size(75, 23);
			this.disconnectButton.TabIndex = 4;
			this.disconnectButton.Text = "&Disconnect";
			this.disconnectButton.UseVisualStyleBackColor = true;
			this.disconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
			// 
			// bedPicture
			// 
			this.bedPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bedPicture.Location = new System.Drawing.Point(280, 10);
			this.bedPicture.Name = "bedPicture";
			this.bedPicture.Size = new System.Drawing.Size(1000, 1000);
			this.bedPicture.TabIndex = 5;
			this.bedPicture.TabStop = false;
			this.bedPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BedPaint);
			this.bedPicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BedClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
			// 
			// setLowerBoundToolStripMenuItem
			// 
			this.setLowerBoundToolStripMenuItem.Name = "setLowerBoundToolStripMenuItem";
			this.setLowerBoundToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.setLowerBoundToolStripMenuItem.Tag = "Lower";
			this.setLowerBoundToolStripMenuItem.Text = "Set lower bound";
			this.setLowerBoundToolStripMenuItem.Click += new System.EventHandler(this.SetBound);
			// 
			// setUpperBoundToolStripMenuItem
			// 
			this.setUpperBoundToolStripMenuItem.Name = "setUpperBoundToolStripMenuItem";
			this.setUpperBoundToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.setUpperBoundToolStripMenuItem.Tag = "Upper";
			this.setUpperBoundToolStripMenuItem.Text = "Set upper bound";
			this.setUpperBoundToolStripMenuItem.Click += new System.EventHandler(this.SetBound);
			// 
			// BedLeveler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1280, 1035);
			this.Controls.Add(this.bedPicture);
			this.Controls.Add(this.disconnectButton);
			this.Controls.Add(this.outputText);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.connectButton);
			this.Controls.Add(this.portText);
			this.DoubleBuffered = true;
			this.Name = "BedLeveler";
			this.Text = "Printer Bed Inspector";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BedLeveler_FormClosing);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bedPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox portText;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.TextBox outputText;
		private System.Windows.Forms.Button disconnectButton;
		private System.Windows.Forms.PictureBox bedPicture;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x12PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x9PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x5PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x3PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x17PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem measureCornersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setZ50ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem x19PatternToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem setLowerBoundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setUpperBoundToolStripMenuItem;
	}
}