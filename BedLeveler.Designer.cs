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
            System.Windows.Forms.Label measureEveryUnits;
            System.Windows.Forms.TabPage tabBedSize;
            System.Windows.Forms.Label heightUnits;
            System.Windows.Forms.Label heightLabel;
            System.Windows.Forms.Label widthUnits;
            System.Windows.Forms.Label widthLabel;
            System.Windows.Forms.TabPage tabColors;
            System.Windows.Forms.Label autoColorLabel;
            System.Windows.Forms.Label maxZUnits;
            System.Windows.Forms.Label maxZLabel;
            System.Windows.Forms.Label minZUnits;
            System.Windows.Forms.Label minZLabel;
            System.Windows.Forms.TabControl tabs;
            this.heightText = new System.Windows.Forms.TextBox();
            this.widthText = new System.Windows.Forms.TextBox();
            this.maxZText = new System.Windows.Forms.TextBox();
            this.minZText = new System.Windows.Forms.TextBox();
            this.tabDetection = new System.Windows.Forms.TabPage();
            this.checkCustom = new System.Windows.Forms.CheckBox();
            this.checkMarlin2 = new System.Windows.Forms.CheckBox();
            this.checkMarlin1 = new System.Windows.Forms.CheckBox();
            this.textCustom = new System.Windows.Forms.RichTextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.outputText = new System.Windows.Forms.TextBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.bedPicture = new System.Windows.Forms.PictureBox();
            this.measureEveryText = new System.Windows.Forms.TextBox();
            this.measureEveryButton = new System.Windows.Forms.Button();
            this.setZ5Button = new System.Windows.Forms.Button();
            this.measureCornersButton = new System.Windows.Forms.Button();
            this.recenterButton = new System.Windows.Forms.Button();
            measureEveryUnits = new System.Windows.Forms.Label();
            tabBedSize = new System.Windows.Forms.TabPage();
            heightUnits = new System.Windows.Forms.Label();
            heightLabel = new System.Windows.Forms.Label();
            widthUnits = new System.Windows.Forms.Label();
            widthLabel = new System.Windows.Forms.Label();
            tabColors = new System.Windows.Forms.TabPage();
            autoColorLabel = new System.Windows.Forms.Label();
            maxZUnits = new System.Windows.Forms.Label();
            maxZLabel = new System.Windows.Forms.Label();
            minZUnits = new System.Windows.Forms.Label();
            minZLabel = new System.Windows.Forms.Label();
            tabs = new System.Windows.Forms.TabControl();
            tabBedSize.SuspendLayout();
            tabColors.SuspendLayout();
            tabs.SuspendLayout();
            this.tabDetection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bedPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // measureEveryUnits
            // 
            measureEveryUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            measureEveryUnits.AutoSize = true;
            measureEveryUnits.Location = new System.Drawing.Point(251, 770);
            measureEveryUnits.Name = "measureEveryUnits";
            measureEveryUnits.Size = new System.Drawing.Size(23, 13);
            measureEveryUnits.TabIndex = 10;
            measureEveryUnits.Text = "mm";
            // 
            // tabBedSize
            // 
            tabBedSize.Controls.Add(heightUnits);
            tabBedSize.Controls.Add(this.heightText);
            tabBedSize.Controls.Add(heightLabel);
            tabBedSize.Controls.Add(widthUnits);
            tabBedSize.Controls.Add(this.widthText);
            tabBedSize.Controls.Add(widthLabel);
            tabBedSize.Location = new System.Drawing.Point(4, 22);
            tabBedSize.Name = "tabBedSize";
            tabBedSize.Padding = new System.Windows.Forms.Padding(3);
            tabBedSize.Size = new System.Drawing.Size(254, 127);
            tabBedSize.TabIndex = 0;
            tabBedSize.Text = "Bed Size";
            tabBedSize.UseVisualStyleBackColor = true;
            // 
            // heightUnits
            // 
            heightUnits.AutoSize = true;
            heightUnits.Location = new System.Drawing.Point(130, 52);
            heightUnits.Name = "heightUnits";
            heightUnits.Size = new System.Drawing.Size(23, 13);
            heightUnits.TabIndex = 5;
            heightUnits.Text = "mm";
            // 
            // heightText
            // 
            this.heightText.Location = new System.Drawing.Point(75, 49);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(49, 20);
            this.heightText.TabIndex = 4;
            this.heightText.Text = "150";
            this.heightText.TextChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Location = new System.Drawing.Point(6, 52);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new System.Drawing.Size(63, 13);
            heightLabel.TabIndex = 3;
            heightLabel.Text = "Bed Height:";
            // 
            // widthUnits
            // 
            widthUnits.AutoSize = true;
            widthUnits.Location = new System.Drawing.Point(130, 26);
            widthUnits.Name = "widthUnits";
            widthUnits.Size = new System.Drawing.Size(23, 13);
            widthUnits.TabIndex = 2;
            widthUnits.Text = "mm";
            // 
            // widthText
            // 
            this.widthText.Location = new System.Drawing.Point(75, 23);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(49, 20);
            this.widthText.TabIndex = 1;
            this.widthText.Text = "150";
            this.widthText.TextChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new System.Drawing.Point(6, 26);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new System.Drawing.Size(60, 13);
            widthLabel.TabIndex = 0;
            widthLabel.Text = "Bed Width:";
            // 
            // tabColors
            // 
            tabColors.Controls.Add(autoColorLabel);
            tabColors.Controls.Add(maxZUnits);
            tabColors.Controls.Add(this.maxZText);
            tabColors.Controls.Add(maxZLabel);
            tabColors.Controls.Add(minZUnits);
            tabColors.Controls.Add(this.minZText);
            tabColors.Controls.Add(minZLabel);
            tabColors.Location = new System.Drawing.Point(4, 22);
            tabColors.Name = "tabColors";
            tabColors.Padding = new System.Windows.Forms.Padding(3);
            tabColors.Size = new System.Drawing.Size(254, 127);
            tabColors.TabIndex = 1;
            tabColors.Text = "Colors";
            tabColors.UseVisualStyleBackColor = true;
            // 
            // autoColorLabel
            // 
            autoColorLabel.AutoSize = true;
            autoColorLabel.Location = new System.Drawing.Point(6, 70);
            autoColorLabel.Name = "autoColorLabel";
            autoColorLabel.Size = new System.Drawing.Size(161, 13);
            autoColorLabel.TabIndex = 6;
            autoColorLabel.Text = "Leave blank for automatic colors";
            // 
            // maxZUnits
            // 
            maxZUnits.AutoSize = true;
            maxZUnits.Location = new System.Drawing.Point(168, 43);
            maxZUnits.Name = "maxZUnits";
            maxZUnits.Size = new System.Drawing.Size(23, 13);
            maxZUnits.TabIndex = 5;
            maxZUnits.Text = "mm";
            // 
            // maxZText
            // 
            this.maxZText.Location = new System.Drawing.Point(113, 40);
            this.maxZText.Name = "maxZText";
            this.maxZText.Size = new System.Drawing.Size(49, 20);
            this.maxZText.TabIndex = 4;
            this.maxZText.TextChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // maxZLabel
            // 
            maxZLabel.AutoSize = true;
            maxZLabel.Location = new System.Drawing.Point(6, 43);
            maxZLabel.Name = "maxZLabel";
            maxZLabel.Size = new System.Drawing.Size(95, 13);
            maxZLabel.TabIndex = 3;
            maxZLabel.Text = "Force max Z color:";
            // 
            // minZUnits
            // 
            minZUnits.AutoSize = true;
            minZUnits.Location = new System.Drawing.Point(168, 17);
            minZUnits.Name = "minZUnits";
            minZUnits.Size = new System.Drawing.Size(23, 13);
            minZUnits.TabIndex = 2;
            minZUnits.Text = "mm";
            // 
            // minZText
            // 
            this.minZText.Location = new System.Drawing.Point(113, 14);
            this.minZText.Name = "minZText";
            this.minZText.Size = new System.Drawing.Size(49, 20);
            this.minZText.TabIndex = 1;
            this.minZText.TextChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // minZLabel
            // 
            minZLabel.AutoSize = true;
            minZLabel.Location = new System.Drawing.Point(6, 17);
            minZLabel.Name = "minZLabel";
            minZLabel.Size = new System.Drawing.Size(92, 13);
            minZLabel.TabIndex = 0;
            minZLabel.Text = "Force min Z color:";
            // 
            // tabs
            // 
            tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            tabs.Controls.Add(tabBedSize);
            tabs.Controls.Add(tabColors);
            tabs.Controls.Add(this.tabDetection);
            tabs.Location = new System.Drawing.Point(12, 488);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new System.Drawing.Size(262, 153);
            tabs.TabIndex = 4;
            // 
            // tabDetection
            // 
            this.tabDetection.Controls.Add(this.checkCustom);
            this.tabDetection.Controls.Add(this.checkMarlin2);
            this.tabDetection.Controls.Add(this.checkMarlin1);
            this.tabDetection.Controls.Add(this.textCustom);
            this.tabDetection.Location = new System.Drawing.Point(4, 22);
            this.tabDetection.Name = "tabDetection";
            this.tabDetection.Size = new System.Drawing.Size(254, 127);
            this.tabDetection.TabIndex = 2;
            this.tabDetection.Text = "Detection";
            this.tabDetection.UseVisualStyleBackColor = true;
            // 
            // checkCustom
            // 
            this.checkCustom.AutoSize = true;
            this.checkCustom.Location = new System.Drawing.Point(6, 63);
            this.checkCustom.Name = "checkCustom";
            this.checkCustom.Size = new System.Drawing.Size(61, 17);
            this.checkCustom.TabIndex = 2;
            this.checkCustom.Text = "Custom";
            this.checkCustom.UseVisualStyleBackColor = true;
            this.checkCustom.CheckedChanged += new System.EventHandler(this.CheckCustomChanged);
            // 
            // checkMarlin2
            // 
            this.checkMarlin2.AutoSize = true;
            this.checkMarlin2.Checked = true;
            this.checkMarlin2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMarlin2.Location = new System.Drawing.Point(6, 40);
            this.checkMarlin2.Name = "checkMarlin2";
            this.checkMarlin2.Size = new System.Drawing.Size(71, 17);
            this.checkMarlin2.TabIndex = 1;
            this.checkMarlin2.Text = "Marlin 2.x";
            this.checkMarlin2.UseVisualStyleBackColor = true;
            this.checkMarlin2.CheckedChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // checkMarlin1
            // 
            this.checkMarlin1.AutoSize = true;
            this.checkMarlin1.Checked = true;
            this.checkMarlin1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMarlin1.Location = new System.Drawing.Point(6, 17);
            this.checkMarlin1.Name = "checkMarlin1";
            this.checkMarlin1.Size = new System.Drawing.Size(71, 17);
            this.checkMarlin1.TabIndex = 0;
            this.checkMarlin1.Text = "Marlin 1.x";
            this.checkMarlin1.UseVisualStyleBackColor = true;
            this.checkMarlin1.CheckedChanged += new System.EventHandler(this.RedrawBedImage);
            // 
            // textCustom
            // 
            this.textCustom.DetectUrls = false;
            this.textCustom.Enabled = false;
            this.textCustom.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCustom.Location = new System.Drawing.Point(23, 86);
            this.textCustom.Multiline = false;
            this.textCustom.Name = "textCustom";
            this.textCustom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textCustom.ShortcutsEnabled = false;
            this.textCustom.Size = new System.Drawing.Size(220, 22);
            this.textCustom.TabIndex = 3;
            this.textCustom.Text = "Bed X: {X} Y: {Y} Z: {Z}";
            this.textCustom.WordWrap = false;
            this.textCustom.TextChanged += new System.EventHandler(this.CustomDetectionChanged);
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(12, 14);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(100, 20);
            this.portText.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(118, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "&Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // outputText
            // 
            this.outputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.outputText.Location = new System.Drawing.Point(12, 41);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.ReadOnly = true;
            this.outputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputText.Size = new System.Drawing.Size(262, 416);
            this.outputText.TabIndex = 3;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(199, 12);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 2;
            this.disconnectButton.Text = "&Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // bedPicture
            // 
            this.bedPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bedPicture.Location = new System.Drawing.Point(284, 0);
            this.bedPicture.Name = "bedPicture";
            this.bedPicture.Size = new System.Drawing.Size(800, 800);
            this.bedPicture.TabIndex = 5;
            this.bedPicture.TabStop = false;
            this.bedPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.BedPaint);
            this.bedPicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BedClick);
            this.bedPicture.Resize += new System.EventHandler(this.RedrawBedImage);
            // 
            // measureEveryText
            // 
            this.measureEveryText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.measureEveryText.Location = new System.Drawing.Point(196, 767);
            this.measureEveryText.Name = "measureEveryText";
            this.measureEveryText.Size = new System.Drawing.Size(49, 20);
            this.measureEveryText.TabIndex = 9;
            this.measureEveryText.Text = "10";
            // 
            // measureEveryButton
            // 
            this.measureEveryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.measureEveryButton.Location = new System.Drawing.Point(12, 765);
            this.measureEveryButton.Name = "measureEveryButton";
            this.measureEveryButton.Size = new System.Drawing.Size(178, 23);
            this.measureEveryButton.TabIndex = 8;
            this.measureEveryButton.Text = "Measure every:";
            this.measureEveryButton.UseVisualStyleBackColor = true;
            this.measureEveryButton.Click += new System.EventHandler(this.GeneratePattern);
            // 
            // setZ5Button
            // 
            this.setZ5Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.setZ5Button.Location = new System.Drawing.Point(12, 689);
            this.setZ5Button.Name = "setZ5Button";
            this.setZ5Button.Size = new System.Drawing.Size(262, 23);
            this.setZ5Button.TabIndex = 6;
            this.setZ5Button.Text = "Move Z to 5.0mm";
            this.setZ5Button.UseVisualStyleBackColor = true;
            this.setZ5Button.Click += new System.EventHandler(this.SetZto5);
            // 
            // measureCornersButton
            // 
            this.measureCornersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.measureCornersButton.Location = new System.Drawing.Point(12, 736);
            this.measureCornersButton.Name = "measureCornersButton";
            this.measureCornersButton.Size = new System.Drawing.Size(262, 23);
            this.measureCornersButton.TabIndex = 7;
            this.measureCornersButton.Text = "Measure corners";
            this.measureCornersButton.UseVisualStyleBackColor = true;
            this.measureCornersButton.Click += new System.EventHandler(this.MeasureCorners);
            // 
            // recenterButton
            // 
            this.recenterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recenterButton.Location = new System.Drawing.Point(12, 660);
            this.recenterButton.Name = "recenterButton";
            this.recenterButton.Size = new System.Drawing.Size(262, 23);
            this.recenterButton.TabIndex = 5;
            this.recenterButton.Text = "Clear and return to center";
            this.recenterButton.UseVisualStyleBackColor = true;
            this.recenterButton.Click += new System.EventHandler(this.ClearClicked);
            // 
            // BedLeveler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 800);
            this.Controls.Add(tabs);
            this.Controls.Add(measureEveryUnits);
            this.Controls.Add(this.measureEveryText);
            this.Controls.Add(this.measureEveryButton);
            this.Controls.Add(this.setZ5Button);
            this.Controls.Add(this.measureCornersButton);
            this.Controls.Add(this.recenterButton);
            this.Controls.Add(this.bedPicture);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.portText);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(740, 490);
            this.Name = "BedLeveler";
            this.Text = "Printer Bed Inspector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BedLeveler_FormClosing);
            tabBedSize.ResumeLayout(false);
            tabBedSize.PerformLayout();
            tabColors.ResumeLayout(false);
            tabColors.PerformLayout();
            tabs.ResumeLayout(false);
            this.tabDetection.ResumeLayout(false);
            this.tabDetection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bedPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox portText;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.TextBox outputText;
		private System.Windows.Forms.Button disconnectButton;
		private System.Windows.Forms.PictureBox bedPicture;
		private System.Windows.Forms.TextBox measureEveryText;
		private System.Windows.Forms.Button measureEveryButton;
		private System.Windows.Forms.Button setZ5Button;
		private System.Windows.Forms.Button measureCornersButton;
		private System.Windows.Forms.Button recenterButton;
		private System.Windows.Forms.TextBox heightText;
		private System.Windows.Forms.TextBox widthText;
		private System.Windows.Forms.TextBox maxZText;
		private System.Windows.Forms.TextBox minZText;
		private System.Windows.Forms.TabPage tabDetection;
		private System.Windows.Forms.CheckBox checkCustom;
		private System.Windows.Forms.CheckBox checkMarlin2;
		private System.Windows.Forms.CheckBox checkMarlin1;
		private System.Windows.Forms.RichTextBox textCustom;
	}
}