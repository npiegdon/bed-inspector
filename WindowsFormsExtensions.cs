using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	public static class ControlExtensions
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

		/// <summary>
		/// Prevents a control from redrawing.  This is useful when performing bulk updates.
		/// </summary>
		public static void SetRedraw(this Control control, bool allow)
		{
			const int WM_SETREDRAW = 0xB;
			SendMessage(control.Handle, WM_SETREDRAW, allow ? 1 : 0, IntPtr.Zero);
		}

		/// <summary>
		/// Allows any amount of updating/formatting while handling the details of preserving
		/// the current position and selection in the box (while also preventing flicker).
		/// </summary>
		public static void MinimalImpactFormat(this RichTextBox box, Action<RichTextBox> f)
		{
			// Grab previous state
			var (prevStart, prevLen) = (box.SelectionStart, box.SelectionLength);
			var leftId = box.GetCharIndexFromPosition(new Point(3, 3));
			var rightId = box.GetCharIndexFromPosition(new Point(box.Width - 3, box.Height - 3));

			// Prevent flicker
			box.SetRedraw(false);

			f(box);

			// Reset our view
			box.SelectionLength = 0;
			box.SelectionStart = 0;

			// Restore the previous view
			box.SelectionStart = leftId;
			box.SelectionLength = Math.Max(0, rightId - leftId);

			// Restore the previous selection
			box.SelectionLength = prevLen;
			box.SelectionStart = prevStart;

			// Resume drawing and force a redraw
			box.SetRedraw(true);
			box.Invalidate();
		}
	}

	public static class SystemExtensions
	{
		public static string ReplaceFirst(this string text, string search, string replace)
		{
			int pos = text.IndexOf(search);
			if (pos < 0) return text;

			return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
		}
	}
}
