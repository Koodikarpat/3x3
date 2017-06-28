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
		private List<Game> games = new List<Game>();

		public Server(int port)
		{
			listener = new TcpListener(IPAddress.Any, port);
			listenThread = new Thread(new ThreadStart (ListenForConnection));
			listenThread.Start();
		}

		private void ListenForConnection()
		{
			listener.Start();

			List<Connection> connections = new List<Connection>();

			while (true)
			{
				// wait for a connection
				TcpClient client = listener.AcceptTcpClient();

				connections.Add(new Connection());
				var thisConnection = connections[connections.Count - 1];
				thisConnection.Start(thisConnection, client, OnMessage, OnConnectionClose);

				Console.WriteLine("There are now " + connections.Count + " connections in the list");

				if (connections.Count % 2 == 0) {
					games.Add(new Game(connections[connections.Count - 2], connections[connections.Count - 1]));
				}
			}
		}

		private void OnMessage(Connection thisConnection, object message)
		{
			if (games.Contains(GameOfUser(thisConnection))) {
				games[games.IndexOf(GameOfUser(thisConnection))].OnMessage(thisConnection, message);
			} else {
				Console.WriteLine ("A request just flew by, but this connection is not in a room");

				var res = new Status ();
				res = Status.Ok;
				thisConnection.SendObject(res);
			}
		}

		private void OnConnectionClose(Connection thisConnection)
		{
			if (connections.Remove(thisConnection)) Console.WriteLine("Connection removed succesfully");
			thisConnection = null;
		}

		private Game GameOfUser(Connection findConnection)
		{
			return games[0]; // TODO: implement more than one game Warning: more than two connections made to the server will cause a race condition
		}
	}
}
