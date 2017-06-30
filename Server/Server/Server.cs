using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
//using System.Collections.Concurrent; TODO using List<> is not thread safe Concurrent.ConcurrentBag<> should provide similar functionality
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

			Random random = new Random ();
		}

		private void ListenForConnection()
		{
			listener.Start();

			while (true)
			{
				// wait for a connection
				TcpClient client = listener.AcceptTcpClient();

				connections.Add(new Connection());
				var thisConnection = connections[connections.Count - 1];
				thisConnection.Start(client, onMessageCallback: OnMessage, onCloseCallback: OnConnectionClose, onAuthCallback:  OnUserAuthenticated, onDeauthCallback: OnUserDeauthenticated, logCallback: OnConnectionLog);

				Console.WriteLine("There are now " + connections.Count + " connections");
			}
		}

		private void OnMessage(Connection thisConnection, object message)
		{
			if (games.Contains(GameOfUser(thisConnection.user))) {
				games[games.IndexOf(GameOfUser(thisConnection.user))].OnMessage(thisConnection.user, message);
			} else {
				OnConnectionLog(thisConnection, "A request just flew by, but this user is not in a game");
			}
		}

		private void OnUserAuthenticated(User user)
		{
			users.Add(user); // TODO: don't add users who aleready are authenticated

			// if two users, connect them to a game TODO: implement better matchmaking

			if (users.Count % 2 == 0) {
				games.Add(new Game (users[users.Count - 2], users[users.Count - 1], ConnectionOfUser));
				games[games.Count - 1].Start();
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
			Console.WriteLine(thisConnection.name + ": " + msg);
		}

		private void OnGameEnd(Game game) {
			if (games.Remove(game)) { // true if remove successfull
				Console.WriteLine("Game ended");
			}
		}

		// search users based on username, multiple users authenticated with same username will cause a race condition
		private Connection ConnectionOfUser(User findUser) // returns null if not connected
		{
			return connections.Find(connection => (String.Equals(connection.user.username, findUser.username)));
		}

		private Game GameOfUser(User findUser) // returns null if not in game
		{
			return games.Find(game => (String.Equals(game.user1.username, findUser.username) || String.Equals(game.user2.username, findUser.username)));
		}
	}
}
