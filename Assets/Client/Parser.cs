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

		public object RecvObject(BinaryReader reader) { // returns null if failed
			string json = "{}";

			try {
				json = reader.ReadString ();
			} catch {
				
			}

			try {
				var deserialised = JsonConvert.DeserializeObject<Message>(json);

				if (deserialised.authenticationRequest != null) { // only one of the Message fields can be populated in one message
					return (AuthenticationRequest)deserialised.authenticationRequest;
				} else if (deserialised.authenticationResponse != null) {
					return (AuthenticationResponse)deserialised.authenticationResponse;
				} else {
					// parsing message failed
					return null;
				}
			} catch {
				// deserialisation failed
				return null;
			}
		}
	}	
}
