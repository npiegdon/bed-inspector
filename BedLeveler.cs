using System;
using System.Numerics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BedLeveler
{
	public partial class BedLeveler : Form
	{
		PrinterPort port;
		readonly List<Vector3> points = new List<Vector3>();
		readonly List<Vector2> toMeasure = new List<Vector2>();

		readonly Regex Marlin1 = ParseSimplifiedDetector("Bed Position X: {X} Y: {Y} Z: {Z}");
		readonly Regex Marlin2 = ParseSimplifiedDetector("Bed X: {X} Y: {Y} Z: {Z}");
		Regex customDetection = null;

		readonly int zMeasureHeight = 3;
		readonly int feedSpeed = 6000;
		readonly string decimalSeparator = ".";
		readonly static string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

		public BedLeveler()
		{
			InitializeComponent();

			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Text += $" {version.Major}.{version.Minor}";

			UpdateCustomDetection(textCustom);

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

		private static Regex ParseSimplifiedDetector(string s)
		{
			string matchFloat = @"[-+]?[0-9]*[\." + systemDecimalSeparator + @"]?[0-9]+";

			// The curly braces happen to be a regex-special character and
			// the escaping is weird, so we use our own escaping scheme
			string expr = Regex.Escape(s.ReplaceFirst("{X}", "__X__").ReplaceFirst("{Y}", "__Y__").ReplaceFirst("{Z}", "__Z__"));
			expr = expr.ReplaceFirst("__X__", $"(?<x>{matchFloat})");
			expr = expr.ReplaceFirst("__Y__", $"(?<y>{matchFloat})");
			expr = expr.ReplaceFirst("__Z__", $"(?<z>{matchFloat})");

			Regex r = new Regex(expr);
			return r.GetGroupNumbers().Length == 4 ? r : null;
		}

		private void UpdateCustomDetection(RichTextBox box)
		{
			box.MinimalImpactFormat((t) =>
			{
				t.SelectAll();
				t.SelectionColor = SystemColors.WindowText;
				t.SelectionFont = t.Font;

				var bold = new Font(t.Font, FontStyle.Bold);
				void highlightFirst(string s, Color c)
				{
					var found = t.Text.IndexOf(s);
					if (found == -1) return;

					t.Select(found, s.Length);
					t.SelectionColor = c;
					t.SelectionFont = bold;
				}

				highlightFirst("{X}", Color.Red);
				highlightFirst("{Y}", Color.Green);
				highlightFirst("{Z}", Color.Blue);
			});

			Regex r = ParseSimplifiedDetector(box.Text);
			bool redraw = (r == null) != (customDetection == null);
			customDetection = r;

			if (redraw) bedPicture.Invalidate();
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
			port.Send($"G1 X{x} Y{y} Z{zMeasureHeight} F{feedSpeed}");
			port.Send("G30");
		}

		private void MeasureNextPoint()
		{
			if (port == null) return;
			if (toMeasure.Count == 0) return;

			// Start near the bed center (or [75, 75] if we don't have plausible bed dimensions for whatever reason)
			var (w, h) = BedDimensions;
			Vector2 current = new Vector2(w > 10 ? w / 2.0f : 75.0f, h > 10 ? h / 2.0f : 75.0f);

			// If we've already measured something, start looking for the next point around there instead
			if (points.Count > 0) current = (from p in points select new Vector2(p.X, p.Y)).Last();

			// Find the next closest point we need to test
			var next = (from p in toMeasure let distSq = (current.X - p.X) * (current.X - p.X) + (current.Y - p.Y) * (current.Y - p.Y) orderby distSq select p).First();
			toMeasure.Remove(next);

			// If we've already measured it, don't measure it again
			if ((from p in points where p.X == next.X && p.Y == next.Y select p).Any()) MeasureNextPoint();
			else MeasurePoint(next.X, next.Y);
		}

		private void DataReceived(string s)
		{
			// Omit some of the more verbose printer chatter
			const int ShortestPossibleMessage = 14;
			if (s.StartsWith("ok") && s.Length < 2 + ShortestPossibleMessage) return;
			if (s.Contains("echo") && s.Contains("busy") && s.Contains("processing") && s.Length < 18 + ShortestPossibleMessage) return;

			if (!systemDecimalSeparator.Equals(decimalSeparator))
			{
				s = s.Replace(decimalSeparator, systemDecimalSeparator);
			}

			Invoke(new Action(() => {
				outputText.AppendText(s);
				outputText.AppendText(Environment.NewLine);
				outputText.ScrollToCaret();

				Vector3? Check(Regex r)
				{
					if (r == null) return null;

					var m = r.Match(s);
					if (!m.Success) return null;

					float x = float.Parse(m.Groups["x"].Value);
					float y = float.Parse(m.Groups["y"].Value);
					float z = float.Parse(m.Groups["z"].Value);
					return new Vector3(x, y, z);
				}

				Vector3? p = null;
				if (!p.HasValue && checkMarlin1.Checked) p = Check(Marlin1);
				if (!p.HasValue && checkMarlin2.Checked) p = Check(Marlin2);
				if (!p.HasValue && checkCustom.Checked) p = Check(customDetection);
				if (!p.HasValue) return;

				points.Add(p.Value);
				MeasureNextPoint();
				bedPicture.Invalidate();
			}));
		}

		private void ConnectButton_Click(object sender, EventArgs e)
		{
			if (port == null) port = new PrinterPort(portText.Text, DataReceived);
			connectButton.Enabled = false;
			disconnectButton.Enabled = true;

			port.Send("G21");
			port.Send("G90"); // absolute positioning
			port.Send("G1 F9000");

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

			void DrawError(string s)
			{
				using (Font font = new Font("Arial", 18.0f, FontStyle.Bold))
				using (StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
					g.DrawString(s, font, Brushes.Red, bedPicture.ClientRectangle, format);
			}

			var (w, h) = BedDimensions;
			if (w < 15 || h < 15) { DrawError("Enter valid bed width and height!"); return; }

			if (checkCustom.Checked && customDetection == null) { DrawError("Make sure your custom detection string contains {X}, {Y}, and {Z} somewhere."); return; }
			if (!checkMarlin1.Checked && !checkMarlin2.Checked && !checkCustom.Checked) { DrawError("Choose at least one detection method."); return; }

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
				port.Send("G1 Z2"); //avoid gauging surface
				port.Send("G28");

				if (autolevelCheckBox.Checked)
				{
					port.Send("G29"); //run auto bed leveling. Must occur AFTER G28
				}

				var (w, h) = BedDimensions;
				MeasurePoint(w / 2.0f, h / 2.0f);
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

			MeasurePoint(0, h);
			MeasurePoint(0, 0);
			MeasurePoint(w, 0);
			MeasurePoint(w, h);
		}

		private void SetZto5(object sender, EventArgs e)
		{
			if (port != null) port.Send($"G1 Z5 {feedSpeed}");
		}

		private void RedrawBedImage(object sender, EventArgs e)
		{
			bedPicture.Invalidate();
		}

		private void CustomDetectionChanged(object sender, EventArgs e)
		{
			UpdateCustomDetection(textCustom);
		}

		private void CheckCustomChanged(object sender, EventArgs e)
		{
			textCustom.Enabled = checkCustom.Checked;
			bedPicture.Invalidate();
		}
	}
}
