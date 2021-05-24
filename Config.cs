using System;
using System.Windows.Forms;

namespace BedLeveler
{
	public partial class Config : Form
	{
		public string Box { get => settingsBox.Text; set => settingsBox.Text = value; }

		public Config() => InitializeComponent();
		private void OnLoad(object sender, EventArgs e) => settingsBox.Select(0, 0);
		private void Reset(object sender, EventArgs e) => settingsBox.Text = new PrinterPortSettings().ToString();
	}
}
