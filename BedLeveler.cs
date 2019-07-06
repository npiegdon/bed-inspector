using System;
using System.Numerics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace BedLeveler
{
	public partial class BedLeveler : Form
	{
		PrinterPort port;
		readonly List<Vector3> points = new List<Vector3>();
		readonly List<Vector2> toMeasure = new List<Vector2>();

		public BedLeveler()
		{
			InitializeComponent();
			Text += $" {Application.ProductVersion}";

			// Give our best guess at a port name
			portText.Text = System.IO.Ports.SerialPort.GetPortNames().FirstOrDefault() ?? "COM5";
		}

		(int, int) BedDimensions
		{
			get
			{
				return (int.TryParse(widthText.Text, out int width) ? width : 0,
					int.TryParse(heightText.Text, out int height) ? height : 0);
			}
		}

		(bool, int, int) CheckedDimensions
		{
			get
			{
				var (w, h) = BedDimensions;
				if (w >= 10 && h >= 10) return (true, w, h);

				MessageBox.Show("Double-check you've typed integers in the bed width and height boxes.", "Can't read bed dimensions", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return (false, 0, 0);
			}
		}


		(float?, float?) ColorBounds
		{
			get
			{
				return (float.TryParse(minZText.Text, out float min) ? min : (float?)null,
					float.TryParse(maxZText.Text, out float max) ? max : (float?)null);
			}
		}

		private void MeasurePoint(float x, float y)
		{
			if (port == null) return;
			port.Send("G90");
			port.Send($"G1 X{x} Y{y} Z1 F5000");
			port.Send("G30");
		}

		private void MeasureNextPoint()
		{
			if (port == null) return;
			if (toMeasure.Count == 0) return;

			var (w, h) = BedDimensions;
			Vector2 current = new Vector2(w > 10 ? w / 2.0f : 75.0f, h > 10 ? h / 2.0f : 75.0f);
			if (points.Count > 0) current = (from p in points select new Vector2(p.X, p.Y)).Last();

			var next = (from p in toMeasure let distSq = (current.X - p.X) * (current.X - p.X) + (current.Y - p.Y) * (current.Y - p.Y) orderby distSq select p).First();

			toMeasure.Remove(next);

			// If we've already measured it, don't measure it again
			if ((from p in points where p.X == next.X && p.Y == next.Y select p).Any()) MeasureNextPoint();
			else MeasurePoint(next.X, next.Y);
		}

		private void DataReceived(string s)
		{
			if (s.StartsWith("ok") && s.Length < 4) return;

			Invoke(new Action(() => {
				outputText.AppendText(s);
				outputText.AppendText(Environment.NewLine);
				outputText.ScrollToCaret();

				// "Bed Position X: 13.00 Y: 10.00 Z: 0.10"
				if (!s.StartsWith("Bed Position")) return;

				var tokens = s.Split(' ');
				if (tokens.Length != 8) return;

				float x = float.Parse(tokens[3]);
				float y = float.Parse(tokens[5]);
				float z = float.Parse(tokens[7]);
				points.Add(new Vector3(x, y, z));

				MeasureNextPoint();

				bedPicture.Invalidate();
			}));
		}

		private void ConnectButton_Click(object sender, EventArgs e)
		{
			if (port == null) port = new PrinterPort(portText.Text, DataReceived);
			connectButton.Enabled = false;
			disconnectButton.Enabled = true;

			ClearClicked(this, new EventArgs());
		}

		private void DisconnectButton_Click(object sender, EventArgs e)
		{
			if (port == null) return;
			port.Dispose();
			port = null;

			disconnectButton.Enabled = false;
			connectButton.Enabled = true;
		}

		static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value *= 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0) return Color.FromArgb(255, v, t, p);
			else if (hi == 1) return Color.FromArgb(255, q, v, p);
			else if (hi == 2) return Color.FromArgb(255, p, v, t);
			else if (hi == 3) return Color.FromArgb(255, p, q, v);
			else if (hi == 4) return Color.FromArgb(255, t, p, v);
			else return Color.FromArgb(255, v, p, q);
		}

		float Lerp(float a, float b, float by) { return a * by + b * (1 - by); }
		const int Radius = 20;

		private void BedPaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Color.Black);
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;

			var (w, h) = BedDimensions;
			if (w < 15 || h < 15)
			{
				using (Font font = new Font("Arial", 18.0f, FontStyle.Bold))
				using (StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
					g.DrawString("Enter valid bed width and height!", font, Brushes.Red, bedPicture.ClientRectangle, format);

				return;
			}

			if (points.Count == 0) return;

			float min = (from p in points select p.Z).Min();
			float max = (from p in points select p.Z).Max();

			var (lowerBound, upperBound) = ColorBounds;
			if (lowerBound.HasValue) min = lowerBound.Value;
			if (upperBound.HasValue) max = upperBound.Value;

			float range = max - min;

			using (Font font = new Font("Arial", 11.0f, FontStyle.Bold))
			using (StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
			foreach (var p in points)
			{
				float zPercent = Math.Max(0.0f, Math.Min(1.0f, range < 0.001 ? 1.0f : (p.Z - min) / range));
				float hue = Lerp(0, 240, zPercent);
				Color c = ColorFromHSV(hue, 1.0, 1.0);

				Point pt = new Point((int)((bedPicture.Width - 2 * Radius) * p.X / w), (int)((bedPicture.Height - 2 * Radius) * (h - p.Y) / h));

				using (GraphicsPath path = new GraphicsPath())
				{
					var rect = new Rectangle(pt, new Size(2 * Radius, 2 * Radius));
					path.AddEllipse(rect);

					using (var b = new SolidBrush(c)) g.FillPath(b, path);

					g.SetClip(path);
					g.DrawString(p.Z.ToString("0.00"), font, Brushes.Black, rect, format);
					g.ResetClip();
				}
			}
		}

		private void BedClick(object sender, MouseEventArgs e)
		{
			// Clicking the bed interrupts patterns
			toMeasure.Clear();

			var (valid, w, h) = CheckedDimensions;
			if (!valid) return;

			var pX = Math.Max(0.0f, Math.Min(w, w * (e.X - Radius) / (bedPicture.Width - 2 * Radius)));
			var pY = Math.Max(0.0f, Math.Min(h, h * (bedPicture.Height - e.Y - Radius) / (bedPicture.Height - 2 * Radius)));
			MeasurePoint(pX, pY);
		}

		private void ClearClicked(object sender, EventArgs e)
		{
			// Clearing all measurements interrupts patterns
			toMeasure.Clear();

			points.Clear();
			bedPicture.Invalidate();

			if (port != null)
			{
				port.Send("G28");
				port.Send("G30");
			}
		}

		private void GeneratePattern(object sender, EventArgs e)
		{
			toMeasure.Clear();

			var (valid, w, h) = CheckedDimensions;
			if (!valid) return;

			if (!int.TryParse(measureEveryText.Text, out int measureEvery))
			{
				MessageBox.Show("Double-check you've typed an integer into the \"measure every\" box.", "Can't read step", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			for (int i = 0; i <= w; i += measureEvery)
				for (int j = 0; j <= h; j += measureEvery)
					toMeasure.Add(new Vector2(i, j));

			MeasureNextPoint();
		}

		private void BedLeveler_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (port != null)
			{
				port.Dispose();
				port = null;
			}
		}

		private void MeasureCorners(object sender, EventArgs e)
		{
			// Clearing all measurements interrupts patterns
			toMeasure.Clear();

			var (valid, w, h) = CheckedDimensions;
			if (!valid) return;

			if (port != null)
			{
				port.Send($"G1 Y{h} X0 Z2 F6000"); port.Send("G30");
				port.Send($"G1 X0 Y0 Z2 F6000"); port.Send("G30");
				port.Send($"G1 Y0 X{w} Z2 F6000"); port.Send("G30");
				port.Send($"G1 Y{h} X{w} Z2 F6000"); port.Send("G30");
			}
		}

		private void SetZto5(object sender, EventArgs e)
		{
			if (port != null) port.Send("G1 Z5 F6000");
		}

		private void RedrawBedImage(object sender, EventArgs e)
		{
			bedPicture.Invalidate();
		}
	}
}
