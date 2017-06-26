using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using Networking;

namespace Server
{
	public class Server
	{
		private TcpListener listener;
		private Thread listenThread;

		private List<Connection> connections = new List<Connection>();

		public Server(int port)
		{
			listener = new TcpListener (IPAddress.Any, port);
			listenThread = new Thread (new ThreadStart (ListenForConnection));
			listenThread.Start ();
		}

		private void ListenForConnection()
		{
			listener.Start();

			List<Connection> connections = new List<Connection>();

			while (true)
			{
				// wait for a connection
				TcpClient client = listener.AcceptTcpClient();

				// new thread for this connection
				connections.Add(new Connection());
				connections[connections.Count - 1].Start(client,OnRequest);

				Console.WriteLine ("there are now " + connections.Count + " connections in the list");
			}
		}

		private void OnRequest(object request)
		{
			Console.WriteLine("A request just flew by!");

			var message = Status.Ok;

			connections[connections.Count - 1].SendObject(message);
		}
	}
}
