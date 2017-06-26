﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace Networking
{
	public class Parser
	{
		private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { 
			//TypeNameHandling = TypeNameHandling.All, // This doesn't work
			//TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full // This is stupid
		};

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
				Console.WriteLine("phew");

				Console.WriteLine (json);

				Object message = JsonConvert.DeserializeObject(json, jsonSerializerSettings);
				Console.WriteLine("phew");

				callback(message);
			} catch (Exception e) {
				Console.WriteLine(e);
				return 1;
			}
			return 0;
		}
	}	
}
