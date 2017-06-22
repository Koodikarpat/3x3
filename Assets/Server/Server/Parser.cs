using System;
using System.IO;
using Newtonsoft.Json;

namespace Networking
{
	public class Parser
	{
		private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { 
			TypeNameHandling = TypeNameHandling.All
		};

		public readonly byte[] NEWLINE = { 0xd, 0xa }; // CRLF

		public Parser()
		{
		}

		public bool SendObject(BinaryWriter writer, object message) // returns connected status
		{
			string json = JsonConvert.SerializeObject(message, jsonSerializerSettings);
			try {
				writer.Write(json);
			} catch {
				return false;
			}
			return true;
		}

		public int RecvObject(BinaryReader reader, Action<object> callback) { // returns 0 if succesfull
			string json;
			try {
				json = reader.ReadString();

				Object message = JsonConvert.DeserializeObject(json, jsonSerializerSettings);

				callback(message);
			} catch {
				Console.WriteLine("fail");
				return 1;
			}
			return 0;
		}
	}	
}
