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

		private List<Connection> connections = new List<Connection>(); // open connections
		private List<Game> games = new List<Game>(); // ongoing games
		private List<User> users = new List<User>(); // authenticated users
		// I have no clue if lists can be used in the way they are used in this class

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
				thisConnection.Start(client, onMessageCallback: OnMessage, onCloseCallback: OnConnectionClose);

				Console.WriteLine("There are now " + connections.Count + " connections in the list");
			}
		}

		private void OnMessage(Connection thisConnection, object message)
		{
			if (games.Contains(GameOfUser(thisConnection.user))) {
				games[games.IndexOf(GameOfUser(thisConnection.user))].OnMessage(thisConnection.user, message);
			} else {
				Console.WriteLine ("A request just flew by, but this user is not in a game");
			}
		}

		private void OnUserAuthenticated(User user)
		{
			users.Add(user);

			// if two users, connect them to a game TODO: implement better matchmaking

			if (users.Count % 2 == 0) {
				if (GameOfUser (user) != null) { // if this user is not aleready in a game
					games.Add (new Game (users [users.Count - 2], users [users.Count - 1], connectionOfUserCallback: ConnectionOfUser));
				}
			}
		}

		private void OnUserDeauthenticated(User user)
		{
			GameOfUser(user).Stop();

			if (users.Remove(user)) { // true if remove successfull
				Console.WriteLine("User deauthenticated");
			}
		}

		private void OnConnectionClose(Connection connection)
		{
			if (connections.Remove(connection)) { // true if remove successfull
				Console.WriteLine ("Connection closed");
			}
		}

		private void OnConnectionLog(Connection thisConnection, string msg)
		{
			Console.WriteLine(thisConnection.name + msg);
		}

		private void OnGameEnd(Game game) {
			if (games.Remove(game)) { // true if remove successfull
				Console.WriteLine("Game ended");
			}
		}

		private Connection ConnectionOfUser(User findUser) { // returns null if not connected
			return connections.Find(connection => connection.user == findUser); 
		}

		private Game GameOfUser(User findUser) // returns null if not in game
		{
			return games.Find(game => (game.user1 == findUser) && (game.user2 == findUser));
		}
	}
}
