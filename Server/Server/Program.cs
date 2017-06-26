using System;


namespace Server
{
	class MainClass
	{
		public const int PORT = 2500;

		public static void Main(string[] args)
		{	
			// listen on PORT
			// new connections are handled in Connection.cs
			// Server server = new Server(PORT);
			new Server(PORT);
		}
	}
}
