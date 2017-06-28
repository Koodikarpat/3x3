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

		private Action<Connection, object> onMessageCallback;
		private Action<Connection> onStopCallback;

		private Connection thisConnection;

		private Stopwatch pollTimer;

		private Parser parser;

		public User user;

		public bool connected;

		public Connection()
		{
		}

		public void Start(Connection conn, object socket, Action<Connection,object> onMessage, Action<Connection> onClose) 
		{
			pollTimer = new Stopwatch ();
			pollTimer.Start();

			parser = new Parser();

			thisConnection = conn;
			onMessageCallback = onMessage;
			onStopCallback = onClose;

			client = (TcpClient)socket;

			clientWriter = new BinaryWriter(client.GetStream());
			clientReader = new BinaryReader(client.GetStream());

			connected = true;

			user = null;

			// create a new worker that will handle the connection
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
			user = null;
			client.Close();
			onStopCallback(thisConnection);
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
			object req = e.Result;

			var @switch = new Dictionary<Type, Action> { { typeof(AuthenticationRequest), () => {
						var authReq = (AuthenticationRequest)req;
						if (authReq.protocolVersion != PROTO_VERSION) {
							Log ("Protocol version mismatch");
						}

						user = new User (authReq.username);
						user.Authenticate (authReq.token);
						if (user.IsAuthenticated ()) {
							Log("Authentication Succesfull");
							var res = new AuthenticationResponse ();
							res.status = Networking.Status.Ok;
							SendObject (res);
						} else {
							user = null;
							Log ("Authentication failed");
							var res = new AuthenticationResponse ();
							res.status = Status.Fail;
							SendObject (res);
						}
					}
				}, { typeof(Status), () => {
						onMessageCallback (thisConnection, req);
					}
				}, { typeof(Move), () => {
						onMessageCallback (thisConnection, req);
					}
				}
			};

			if (req != null) @switch[req.GetType()]();
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
			Console.WriteLine("Anonymous connection: " + msg);
		}
	}
}
