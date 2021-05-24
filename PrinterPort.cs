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
		readonly byte Delimiter;
		public event Action<byte[]> LineReceived;

		public LineSplitter(byte delimiter) => Delimiter = delimiter;

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

	public class PrinterPortSettings
	{
		public int baud = 115200;
		public Parity parity = Parity.None;
		public int data = 8;
		public StopBits stop = StopBits.One;
		public Handshake handshake = Handshake.None;
		public bool dtr = true;
		public bool rts = true;

		public PrinterPortSettings() { }

		public PrinterPortSettings(string settings)
		{
			if (string.IsNullOrWhiteSpace(settings)) return;

			var lines = settings.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				var tokens = line.Trim().ToLowerInvariant().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length != 2) continue;

				string v = tokens[1].Trim();
				switch (tokens[0])
				{
					case "baud": baud = (int.TryParse(v, out int b) && b > 0) ? b : baud; break;
					case "parity": parity = Enum.TryParse(v, true, out Parity p) ? p : parity; break;
					case "data": data = (int.TryParse(v, out int d) && d > 0) ? d : data; break;
					case "stop": stop = Enum.TryParse(v, true, out StopBits s) ? s : stop; break;
					case "handshake": handshake = Enum.TryParse(v, true, out Handshake h) ? h : handshake; break;
					case "dtr": dtr = bool.TryParse(v, out bool dt) ? dt : dtr; break;
					case "rts": rts = bool.TryParse(v, out bool rt) ? rt : rts; break;
				}
			}
		}

		public override string ToString()
		{
			return $@"baud {baud}
parity {Enum.GetName(typeof(Parity), parity).ToLowerInvariant()}
data {data}
stop {Enum.GetName(typeof(StopBits), stop).ToLowerInvariant()}
handshake {Enum.GetName(typeof(Handshake), handshake).ToLowerInvariant()}
dtr {dtr.ToString().ToLowerInvariant()}
rts {rts.ToString().ToLowerInvariant()}";
		}
	}

	class PrinterPort : IDisposable
	{
		readonly SerialPort Port;

		readonly Thread Listener;
		readonly Action<string> Callback;

		volatile bool Running = true;

		public PrinterPort(string portName, PrinterPortSettings settings, Action<string> callback)
		{
			Callback = callback ?? throw new ArgumentOutOfRangeException(nameof(callback), "Bad callback");

			if (string.IsNullOrWhiteSpace(portName)) throw new ArgumentOutOfRangeException(nameof(portName), "Bad port");
			Port = new SerialPort(portName, settings.baud, settings.parity, settings.data, settings.stop)
			{
				Handshake = settings.handshake,
				DtrEnable = settings.dtr,
				RtsEnable = settings.rts,
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
			LineSplitter splitter = new((byte)'\n');
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
					if (read == 0) { Thread.Sleep(10); continue; }

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
