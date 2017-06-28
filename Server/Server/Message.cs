using System;
using System.Collections.Generic;

namespace Networking
{
	public class Message // never send a Message using SendObject
	{
		public Status status; // status can be sent from either the server or the client

		public AuthenticationRequest authenticationRequest; // can be sent by the client
		public AuthenticationResponse authenticationResponse; // can be sent by the server

		public Move move;
		public OnMove onMove; // can be sent by the server
	}

	// types used in messages, do not send these

	public class Player
	{
		public string profileId;
		public int position;
		public bool turn;
	}

	public class Inventory
	{
		public string selectedSkin;
	}

	public class Profile
	{
		public string profileId;
		public string username;
		public Inventory inventory;
	}

	public enum Status { Ok, Fail };
	public enum GameStatus { None, Waiting, YourTurn, RemoteTurn, Ended };

	// message types, use these to send messages

	public class OnMove
	{
		public GameStatus gameStatus;
		public Player localPlayer;
		public Player remotePlayer;
		public string playTileAnimation;
	}

	public class Move
	{
		public Player player;
	}

	public class AuthenticationRequest
	{
		public string username;
		public string token;
		public uint protocolVersion;
	}

	public class AuthenticationResponse
	{
		public Status status;
	}

}
