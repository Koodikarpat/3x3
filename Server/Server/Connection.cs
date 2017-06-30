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

		private Action<Connection, object> OnMessageCallback;
		private Action<Connection> OnStopCallback;
		private Action<User> OnAuthCallback;
		private Action<User> OnDeauthCallback; // TODO: implement a method for deauthentication
		private Action<Connection, string> LogCallback;

		private Stopwatch pollTimer;

		private Parser parser;

		public User user;

		public string name { get; private set; }

		public bool connected { get; private set; }

		public Connection()
		{
			name = "Anonymous connection";
		}

		public void Start(TcpClient socket, Action<Connection, object> onMessageCallback, Action<Connection> onCloseCallback, Action<Connection, string> logCallback, Action<User> onAuthCallback, Action<User> onDeauthCallback) 
		{
			pollTimer = new Stopwatch ();
			pollTimer.Start();

			parser = new Parser();

			OnMessageCallback = onMessageCallback;
			OnStopCallback = onCloseCallback;
			OnAuthCallback = onAuthCallback;
			OnDeauthCallback = onDeauthCallback;
			LogCallback = logCallback;

			client = socket;

			name = "Unauthenticated connection";

			clientWriter = new BinaryWriter(client.GetStream());
			clientReader = new BinaryReader(client.GetStream());

			connected = true;

			user = null;

			// create a new worker that will handle waiting for data
			BackgroundWorker waiter = new BackgroundWorker();
			waiter.DoWork += (object sender, DoWorkEventArgs e) => WaitForRequest(e);
			waiter.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
				OnRequest(e);
				if (client.Connected) waiter.RunWorkerAsync();
			};
			waiter.RunWorkerAsync();
		}

		public void Stop()
		{
			Log("Closing connection");
			client.Close();
			OnStopCallback(this);
		}

		private void WaitForRequest(DoWorkEventArgs e)
		{
			object req = parser.RecvObject (clientReader); // this blocks until a request has been received
			if (req != null) {
				e.Result = req;
			}
		}

		// sends any object
		public void SendObject(object msg)
		{
			connected = parser.SendObject (clientWriter, msg);
		}

		private void OnRequest(RunWorkerCompletedEventArgs e)
		{
			if (e.Result == null) {
				Log ("Parsing fail");
				return;
			}

			object req = e.Result;

			var @switch = new Dictionary<Type, Action> { 
				{ typeof(AuthenticationRequest), () => {
						var authReq = (AuthenticationRequest)req;
						if (authReq.protocolVersion != PROTO_VERSION) {
							Log ("Protocol version mismatch");
						}

						user = new User(authReq.username);
						user.Authenticate(authReq.token);
						if (user.IsAuthenticated ()) {
							name = user.username;
							Log("Authentication Succesfull");
							var res = new AuthenticationResponse ();
							res.status = Networking.Status.Ok;
							SendObject (res);
							OnAuthCallback(user);
						} else {
							user = null;
							Log("Authentication failed");
							var res = new AuthenticationResponse();
							res.status = Status.Fail;
							SendObject(res);
						}
					} },
				{ typeof(Status), () => {
						OnMessageCallback (this, req);
					} },
				{ typeof(Move), () => {
						OnMessageCallback (this, req);
					} }
			};

			@switch[req.GetType()]();
		}

		// polls the client every POLL_INTERVAL doesnt really work as its in the same loop as reading the socket
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
			LogCallback(this, msg);
		}
	}
}
