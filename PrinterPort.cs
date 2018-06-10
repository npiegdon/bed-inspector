using System;
using System.IO.Ports;
using System.Threading;

namespace BedLeveler
{
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
			Port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One) { Handshake = Handshake.None };

			Port.Open();
			if (!Port.IsOpen) throw new NotSupportedException("Couldn't connect");

			Listener = new Thread(new ThreadStart(Listen));
			Listener.Start();
		}

		public void Send(string data)
		{
			lock (Port) { if (Port != null && Port.IsOpen) Port.WriteLine(data); }
		}

		private void Listen()
		{
			while (Running)
			{
				string data = null;
				lock (Port) { if (Port != null && Port.IsOpen) if (Port.BytesToRead > 0) data = Port.ReadLine(); }

				if (data != null) Callback(data);
				else Thread.Sleep(50);
			}
		}

		public void Dispose()
		{
			lock (Port)
			{
				if (Port != null)
				{
					if (Port.IsOpen) Port.Close();
					else Port.Dispose();
				}
			}

			Running = false;
			Listener.Join();
		}
	}
}
