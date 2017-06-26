using System;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using System.ComponentModel;
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

		public Client(string name, string username = "player", string token = "password")
		{
			serverName = name;
			serverUsername = username;
			serverAuthToken = token;
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

			Log ("server connected");

			serverStream = server.GetStream();
			serverWriter = new BinaryWriter(server.GetStream());
			serverReader = new BinaryReader (server.GetStream());

			// create a new worker that will handle the connection
			BackgroundWorker waiter = new BackgroundWorker();
			waiter.DoWork += (object sender, DoWorkEventArgs e) => WaitForUpdate();
			waiter.RunWorkerAsync();

			Authenticate();

			return 0;
		}

		public void Disconnect()
		{
			server.Close();
		}

		private void Authenticate() {
			var request = new AuthenticationRequest();
			request.protocolVersion = PROTO_VERSION;
			request.username = serverUsername;
			request.token = serverAuthToken;

			SendObject(request);
		}

		private void WaitForUpdate()
		{
			while (server.Connected) {
				parser.RecvObject (serverReader, OnUpdate); // blocks until a message is received
				// the callback should be run in the main thread, currently that doesn't happen 
			}
		}
		
		private void OnUpdate(object msg)
		{
		}

		private void Request(object req)
		{
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
