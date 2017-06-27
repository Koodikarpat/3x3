using System;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using Networking;


namespace Server
{
	public class Connection
	{
		private const int POLL_INTERVAL = 5000;
		private const uint PROTO_VERSION = 0; // every time you make chance that breaks the interface, you should increment this

		private TcpClient client;
		private BinaryWriter clientWriter;
		private BinaryReader clientReader;

		private Action<object> onStatusCallback;

		private Stopwatch pollTimer;

		private Parser parser;

		public User user;

		public bool connected;

		public Connection()
		{
		}

		public void Start(object socket, Action<object> callback) 
		{
			pollTimer = new Stopwatch ();
			pollTimer.Start();

			parser = new Parser();

			onStatusCallback = callback;

			client = (TcpClient)socket;

			clientWriter = new BinaryWriter(client.GetStream());
			clientReader = new BinaryReader(client.GetStream());

			connected = true;

			// create a new worker that will handle the connection
			BackgroundWorker waiter = new BackgroundWorker();
			waiter.DoWork += (object sender, DoWorkEventArgs e) => WaitForRequest();
			waiter.RunWorkerAsync();
		}

		public void Stop()
		{
			Log ("Closing connection");
			user = null;
			client.Close();
		}

		private void WaitForRequest()
		{
			while (connected && client.Connected) {
				var didUnderstand = parser.RecvObject(clientReader, OnRequest); // this blocks until a request has been received
				if (didUnderstand != 0) {
					Log("Client communication failed");

					var message = new Status();
					message = Status.Fail;
					SendObject(message);

					Stop();
				}
				PollClient();
			}
			Stop();
		}

		// sends any object
		public void SendObject(object msg)
		{
			connected = parser.SendObject (clientWriter, msg);
		}

		private void OnRequest(object req)
		{
			Log("New request");
			var @switch = new Dictionary<Type, Action> {
				{ typeof(AuthenticationRequest), () => {
						Log("It's an AuthenticationRequest");
						var authReq = (AuthenticationRequest)req;
						if (authReq.protocolVersion != PROTO_VERSION) {
							Log ("Protocol version mismatch");
						}
						
						user = new User (authReq.username);
						user.Authenticate (authReq.token);
						if (user.IsAuthenticated ()) {
							Log("Authentication succeeded");
							var res = new AuthenticationResponse ();
							res.status = Networking.Status.Ok;
							SendObject(res);
						} else {
							Log("Authentication failed");
							var res = new AuthenticationResponse ();
							res.status = Status.Fail;
							SendObject(res);
						}
					} },
				{ typeof(Status), () => {
						onStatusCallback(req); // this callback should take place on the main thread, currently that doesn't happen
					} }
			};

			@switch[req.GetType()]();
		}

		// polls the client every POLL_INTERVAL
		private void PollClient()
		{ 
			if (pollTimer.ElapsedMilliseconds > POLL_INTERVAL) {
				pollTimer.Restart();

				var message = new Status();
				message = Status.Ok;
				// are you still there?
				SendObject(message);
			}
		}

		private void Log(string msg)
		{
			Console.WriteLine(msg);
		}
	}
}
