
namespace BedLeveler
{
	partial class Config
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
         this.settingsBox = new System.Windows.Forms.TextBox();
         this.resetButton = new System.Windows.Forms.Button();
         this.okButton = new System.Windows.Forms.Button();
         this.cancelButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // settingsBox
         // 
         this.settingsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.settingsBox.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.settingsBox.Location = new System.Drawing.Point(12, 12);
         this.settingsBox.MaxLength = 256;
         this.settingsBox.Multiline = true;
         this.settingsBox.Name = "settingsBox";
         this.settingsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.settingsBox.Size = new System.Drawing.Size(335, 218);
         this.settingsBox.TabIndex = 0;
         this.settingsBox.WordWrap = false;
         // 
         // resetButton
         // 
         this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.resetButton.Location = new System.Drawing.Point(12, 236);
         this.resetButton.Name = "resetButton";
         this.resetButton.Size = new System.Drawing.Size(140, 23);
         this.resetButton.TabIndex = 3;
         this.resetButton.Text = "&Reset to defaults!";
         this.resetButton.UseVisualStyleBackColor = true;
         this.resetButton.Click += new System.EventHandler(this.Reset);
         // 
         // okButton
         // 
         this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.okButton.Location = new System.Drawing.Point(185, 236);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(78, 23);
         this.okButton.TabIndex = 1;
         this.okButton.Text = "&OK";
         this.okButton.UseVisualStyleBackColor = true;
         // 
         // cancelButton
         // 
         this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point(269, 236);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(78, 23);
         this.cancelButton.TabIndex = 2;
         this.cancelButton.Text = "&Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         // 
         // Config
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size(359, 271);
         this.Controls.Add(this.cancelButton);
         this.Controls.Add(this.okButton);
         this.Controls.Add(this.resetButton);
         this.Controls.Add(this.settingsBox);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.MinimumSize = new System.Drawing.Size(375, 200);
         this.Name = "Config";
         this.Text = "Configure Connection";
         this.Load += new System.EventHandler(this.OnLoad);
         this.ResumeLayout(false);
         this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox settingsBox;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}