using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace BedLeveler
{
	// Adapted from https://www.sparxeng.com/blog/software/reading-lines-serial-port
	class LineSplitter
	{
		byte[] current;
		public readonly byte Delimiter;

		public event Action<byte[]> LineReceived;

		public LineSplitter(byte delimiter)
		{
			Delimiter = delimiter;
		}

		public void AddData(byte[] buffer)
		{
			int offset = 0;
			while (true)
			{
				int newlineIndex = Array.IndexOf(buffer, Delimiter, offset);
				if (newlineIndex < offset)
				{
					current = ConcatArray(current, buffer, offset, buffer.Length - offset);
					return;
				}
				++newlineIndex;
				byte[] full_line = ConcatArray(current, buffer, offset, newlineIndex - offset);
				current = null;
				offset = newlineIndex;
				LineReceived?.Invoke(full_line);
			}
		}

		private static byte[] ConcatArray(byte[] head, byte[] tail, int tailOffset, int tailCount)
		{
			byte[] result;
			if (head == null)
			{
				result = new byte[tailCount];
				Array.Copy(tail, tailOffset, result, 0, tailCount);
			}
			else
			{
				result = new byte[head.Length + tailCount];
				head.CopyTo(result, 0);
				Array.Copy(tail, tailOffset, result, head.Length, tailCount);
			}

			return result;
		}
	}

	class PrinterPort : IDisposable
	{
		readonly SerialPort Port;

		readonly Thread Listener;
		readonly Action<string> Callback;

		volatile bool Running = true;

		public PrinterPort(string portName, Action<string> callback)
		{
			Callback = callback ?? throw new ArgumentOutOfRangeException("Bad callback");

			if (string.IsNullOrWhiteSpace(portName)) throw new ArgumentOutOfRangeException("Bad port");
			Port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One)
			{
				Handshake = Handshake.None,
				DtrEnable = true,
				RtsEnable = true,
			};

			Port.Open();
			if (!Port.IsOpen) throw new NotSupportedException("Couldn't connect");

			Listener = new Thread(new ThreadStart(Listen));
			Listener.Start();
		}

		public void Send(string data)
		{
			lock (Port) { if (Port != null && Port.IsOpen) Port.WriteLine(data); }
		}

		private async void Listen()
		{
			LineSplitter splitter = new LineSplitter((byte)'\n');
			splitter.LineReceived += (b) =>
			{
				if (b.Length == 0) return;

				var s = Encoding.ASCII.GetString(b, 0, b.Length - 1);
				if (string.IsNullOrWhiteSpace(s)) return;

				Callback(s);
			};

			// Adapted from http://www.sparxeng.com/blog/software/must-use-net-system-io-ports-serialport
			byte[] buffer = new byte[65536];
			while (Running)
			{
				if (!Port.IsOpen) break;

				try
				{
					int read = await Port.BaseStream.ReadAsync(buffer, 0, buffer.Length);
					if (read == 0) continue;

					byte[] handoff = new byte[read];
					Buffer.BlockCopy(buffer, 0, handoff, 0, read);
					splitter.AddData(handoff);
				}
				catch (Exception) { break; }
			}
		}

		public void Dispose()
		{
			if (Port.IsOpen) Port.Close();
			else Port.Dispose();

			Running = false;
			Listener.Join();
		}
	}
}
