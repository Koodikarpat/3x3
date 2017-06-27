using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Networking
{
	public class Parser
	{
		public Parser()
		{
		}

		public bool SendObject(BinaryWriter writer, object message) // returns connected status
		{

			var serializable = new Message();

			var @switch = new Dictionary<Type, Action> {
				{ typeof(AuthenticationRequest), () => {
						serializable.authenticationRequest = (AuthenticationRequest)message;
					} },
				{ typeof(AuthenticationResponse), () => {
						serializable.authenticationResponse = (AuthenticationResponse)message;
					} }
			};

			@switch[message.GetType()]();

			string json = JsonConvert.SerializeObject(serializable);

			try {
				writer.Write(json);
			} catch {
				return false;
			}
			return true;
		}

		public int RecvObject(BinaryReader reader, Action<object> callback) { // returns 0 if succesfull
			string json = "{}";

			try {
				json = reader.ReadString ();
			} catch {
			}

			Console.WriteLine (json);

			try {
				var deserialised = JsonConvert.DeserializeObject<Message>(json);

				if (deserialised.authenticationRequest != null) { // Only one of the Message fields can be populated in one message
					var message = new AuthenticationRequest();
					message = deserialised.authenticationRequest;
					callback(message);
				} else if (deserialised.authenticationResponse != null) {
					var message = new AuthenticationResponse();
					message = deserialised.authenticationResponse;
					callback(message);
				} else {
					// Parsing message failed
				}
			} catch (Exception e) {
				// Most likely deserialisation failed
				Console.WriteLine(e);
				return 1;
			}
			return 0;
		}
	}	
}
