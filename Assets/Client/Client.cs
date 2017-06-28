using System;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
	public class Client
	{
		private const uint PROTO_VERSION = 0; // Every time you make chance that breaks the interface, you should increment this
		public const int PORT = 2500;

		private readonly string serverName;

		private readonly string serverUsername;
		private readonly string serverAuthToken;

		private TcpClient server;
		private NetworkStream serverStream;
		private BinaryWriter serverWriter;
		private BinaryReader serverReader;

		private Parser parser;

		private Action<object> gameUpdateCallback;
		private bool inGame;
		private bool localAuthenticated;

		public Client(string name, string username = "player", string token = "password")
		{
			serverName = name;
			serverUsername = username;
			serverAuthToken = token;

			inGame = false;
			localAuthenticated = false;
		}

		public int Connect()
		{
			server = new TcpClient();

			parser = new Parser();

			try {
				server.Connect(serverName, PORT);
			} catch {
				Log("Connecting to: " + serverName + ":" + PORT + " failed.");
				return 1;
			}

			Log ("Connected to server: " + serverName);

			serverStream = server.GetStream();
			serverWriter = new BinaryWriter(server.GetStream());
			serverReader = new BinaryReader (server.GetStream());

			// create a new worker that will handle the connection
			BackgroundWorker waiter = new BackgroundWorker();
			waiter.DoWork += (object sender, DoWorkEventArgs e) => WaitForMessage(e);
			waiter.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
				OnMessage(e);
				if (server.Connected) waiter.RunWorkerAsync();
			};
				
			waiter.RunWorkerAsync();
			Authenticate();

			return 0;
		}

		public void Disconnect()
		{
			server.Close();
		}

		public void StartGame(Action<object> onGameUpdate)
		{
			gameUpdateCallback = onGameUpdate;
		}

		public void Move(Player player)
		{
			var message = new Move();
			message.player = player;

			SendObject(message);
		}

		private void Authenticate() {
			var request = new AuthenticationRequest();
			request.protocolVersion = PROTO_VERSION;
			request.username = serverUsername;
			request.token = serverAuthToken;

			SendObject(request);
		}

		private void WaitForMessage(DoWorkEventArgs e)
		{
			object msg = parser.RecvObject(serverReader); // blocks until a message is received
			if (msg != null) {
				e.Result = msg;
			}
		}
		
		private void OnMessage(RunWorkerCompletedEventArgs e)
		{
			object message = e.Result;

			var @switch = new Dictionary<Type, Action> {
				{ typeof(AuthenticationResponse), () => {
						localAuthenticated = true;
						Log("Authentication successfull");
					} },
				{ typeof(OnMove), () => {
						gameUpdateCallback(message);
					} }
			};

			@switch[message.GetType()]();
		}

		private void SendObject(object msg)
		{
			parser.SendObject(serverWriter, msg);
		}

		private void Log(string msg)
		{
			Debug.Log(msg);
		}
	}
}
