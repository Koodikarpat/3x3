using System;
using Networking;
using System.Collections.Generic;

namespace Server
{
	public class Game
	{
		private Func<User, Connection> ConnectionOfUserCallback;

		public User user1;
		public User user2;

		public Game (User u1, User u2, Func<User, Connection> connectionOfUserCallback)
		{
			ConnectionOfUserCallback = connectionOfUserCallback;

			user1 = u1;
			user2 = u2;
		}

		public void Start()
		{
			user1.player = new Player();
			user2.player = new Player();

			user1.player.position = 7;
			user2.player.position = 1;

			var message = new GameInit();
			message.gameStatus = GameStatus.YourTurn;
			message.localPlayer = user1.player;
			message.remotePlayer = user2.player;
			//message.tiles = TODO: implement tile init

			// ConnectionOfUser may return null
			ConnectionOfUser(user1).SendObject(message);

			message.gameStatus = GameStatus.RemoteTurn;
			message.localPlayer = user2.player;
			message.remotePlayer = user1.player;

			ConnectionOfUser(user2).SendObject(message);

			Console.WriteLine("A new game has begun");
		}

		public void Stop()
		{
		}

		public void OnMessage(User messageUser, object message)
		{
			var @switch = new Dictionary<Type, Action> {
				{ typeof(Move), () => {
						var move = (Move)message;
						if (true) { // TODO check if it is this users turn, is the move legal
							var res = new Move();
							res.player = move.player;
							// TODO new tile

							// TODO: flip game board

							// TODO ConnectionOfUser may return null
							ConnectionOfUser(messageUser).SendObject(res);
							ConnectionOfUser(TheOtherUser(messageUser)).SendObject(res);
						} else {
							var res = new Status();
							res = Status.Fail;

							// TODO ConnectionOfUser may return null
							ConnectionOfUser(messageUser).SendObject(res);
							Console.WriteLine("It's not this players turn");
						}
					} }
			};

			@switch[message.GetType()]();
		}

		private User TheOtherUser(User theUser) {
			if (theUser.Equals(user1)) {
				return user2;
			} else {
				return user1;
			}
		}

		private Connection ConnectionOfUser(User user) {
			return ConnectionOfUserCallback(user);
		}
	}
}

