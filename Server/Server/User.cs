using System;
using Networking;

namespace Server
{
	public class User
	{
		private readonly string username;
		private readonly string correctToken;

		private bool authenticated;

		public Player player;

		public User(String name)
		{
			username = name;
			correctToken = "password";
			authenticated = false;
		}

		// currently any user with the pasword "password" is authenticated
		public int Authenticate(String token) // returns 0 if succesfull
		{
			if (token == correctToken) { // authenticates user
				authenticated = true;
				return 0;
			} else { // authentication failed
				this.authenticated = false;
				return 1;
			}
		}

		public bool IsAuthenticated()
		{
			return authenticated;
		}

		public int ReqisterUser() // returns 0 if succesfull
		{
			if (!authenticated && username != "nobody") {
				// TODO: add user to database
				return 0;
			} else {
				return 1;
			}
		}
	}
}
