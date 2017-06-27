using System;
using System.IO;
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
			string objectType = message.ToString();

			var serializable = new Message ();
			serializable.messageType = objectType;
			serializable.message = message;

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

				switch (deserialised.messageType)
				{
					case "Networking.AuthenticationRequest":
						Console.WriteLine("Will now cast type: " + deserialised.message.GetType() + " as type object");
						var jsonObject = (Newtonsoft.Json.Linq.JObject)deserialised.message;
						var message = (AuthenticationRequest)jsonObject.ToObject(object);
						callback(message);
						break;
				}

				Console.WriteLine("phe2");

			} catch (Exception e) {
				Console.WriteLine(e);
				return 1;
			}
			return 0;
		}
	}	
}
