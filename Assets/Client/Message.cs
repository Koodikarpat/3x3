using System;
using System.Collections.Generic;

namespace Networking
{
	public class Message
	{
		// types used in messages
		public struct Position
		{
			public uint x;
			public uint y;
		}

		public struct Player
		{
			public string profileId;
			public Position position;
		}

		public struct Inventory
		{
			public string selectedSkin;
		}

		public struct Profile
		{
			public string profileId;
			public string username;
			public Inventory inventory;
		}

		public enum Status { Ok, Fail };
		public enum GameStatus { None, Waiting, YourTurn, RemoteTurn, Ended };

		// message types
		public struct OnMove
		{
			public GameStatus gameStatus;
			public Player localPlayer;
			public Player remotePlayer;
			public string playTileAnimation;
		}

		public class AuthenticationRequest
		{
			public string username;
			public string token;
			public uint ProtocolVersion;
		}

		public struct AuthenticationResponse
		{
			public Status status;
		}

		public Type Request; // You can request these: Status, GameStatus, Profile(profileId)

		public object Response;
	}
}
