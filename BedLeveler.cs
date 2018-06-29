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
		List<Vector3> points = new List<Vector3>();
		List<Vector2> toMeasure = new List<Vector2>();

		float? LowerBound = null;
		float? UpperBound = null;

		public BedLeveler()
		{
			InitializeComponent();
			Text += $" {Application.ProductVersion}";
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

			Vector2 current = new Vector2(75, 75);
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
			statusLabel.Text = $"Connected to {portText.Text}";

			ClearClicked(this, new EventArgs());
		}

		private void DisconnectButton_Click(object sender, EventArgs e)
		{
			if (port == null) return;
			port.Dispose();
			port = null;

			disconnectButton.Enabled = false;
			connectButton.Enabled = true;
			statusLabel.Text = "Not connected";
		}

		private const int Radius = 20;

		private Vector2 ToPhysical(int x, int y)
		{
			float pX = Math.Max(0.0f, Math.Min(150.0f, 150.0f * (x-Radius) / (bedPicture.Width - 2*Radius)));
			float pY = Math.Max(0.0f, Math.Min(150.0f, 150.0f * ((bedPicture.Height - y) - Radius) / (bedPicture.Height - 2*Radius)));
			return new Vector2(pX, pY);
		}
		private Point FromPhysical(float x, float y)
		{
			return new Point((int)((bedPicture.Width - 2*Radius) * x / 150.0f) + Radius, (int)((bedPicture.Height - 2*Radius) * (150.0f - y) / 150.0f) + Radius);
		}

		public static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
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

		private void BedPaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Color.Black);
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;

			if (points.Count == 0) return;

			float min = (from p in points select p.Z).Min();
			float max = (from p in points select p.Z).Max();
			if (LowerBound.HasValue) min = LowerBound.Value;
			if (UpperBound.HasValue) max = UpperBound.Value;

			float range = max - min;

			using (Font font = new Font("Arial", 11.0f, FontStyle.Bold))
			using (StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
			foreach (var p in points)
			{
				float zPercent = Math.Max(0.0f, Math.Min(1.0f, range < 0.001 ? 1.0f : (p.Z - min) / range));
				float hue = Lerp(0, 240, zPercent);
				Color c = ColorFromHSV(hue, 1.0, 1.0);

				Point pt = FromPhysical(p.X, p.Y);
				pt.Offset(-Radius, -Radius);

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

			var physical = ToPhysical(e.X, e.Y);
			MeasurePoint(physical.X, physical.Y);
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

			var tag = ((ToolStripMenuItem)sender).Tag as string;
			if (tag == null) return;

			int steps = int.Parse(tag);
			float step = 150.0f / (steps - 1);

			for (int i = 0; i < steps; ++i)
				for (int j = 0; j < steps; ++j)
					toMeasure.Add(new Vector2(i * step, j * step));

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

			if (port != null)
			{
				port.Send("G1 Y150 X0 Z2 F6000"); port.Send("G30");
				port.Send("G1 X0 Y0 Z2 F6000"); port.Send("G30");
				port.Send("G1 Y0 X150 Z2 F6000"); port.Send("G30");
				port.Send("G1 Y150 X150 Z2 F6000"); port.Send("G30");
			}
		}

		private void SetZto5(object sender, EventArgs e)
		{
			if (port != null) port.Send("G1 Z5 F6000");
		}

		private void SetBound(object sender, EventArgs e)
		{
			var tag = ((ToolStripMenuItem)sender).Tag as string;

			float? current = null;
			if (tag.StartsWith("L")) current = LowerBound;
			else current = UpperBound;

			string input = Microsoft.VisualBasic.Interaction.InputBox($"Choose a {tag} Bound for color selection.  Leave blank to reset to automatic bound.  This is for visualization only (to match against subsequent scans).", $"Set {tag} Color Bound", current?.ToString() ?? "");

			if (string.IsNullOrWhiteSpace(input))
			{
				if (tag.StartsWith("L")) LowerBound = null;
				else UpperBound = null;
			}
			else if (float.TryParse(input, out float value))
			{
				if (tag.StartsWith("L")) LowerBound = value;
				else UpperBound = value;
			}

			bedPicture.Invalidate();
		}
	}
}
