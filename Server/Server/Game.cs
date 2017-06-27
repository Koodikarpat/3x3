using System;
using Networking;
using System.Collections.Generic;

namespace Server
{
	public class Game
	{
		public Connection player1;
		public Connection player2;

		public Game (Connection p1, Connection p2)
		{
			player1 = p1;
			player2 = p2;

			player1.user.player.turn = true;
			player2.user.player.turn = false;

			player1.user.player.position = new Position(0, 0);
		}

		public void OnMessage(Connection messageConnection, object message)
		{
			var @switch = new Dictionary<Type, Action> {
				{ typeof(Move), () => {
						if (messageConnection.user.player.turn) {
							var res = new OnMove();
							res.gameStatus = GameStatus.RemoteTurn;
							res.playTileAnimation = "not implemented";
							res.localPlayer = messageConnection.user.player;
							res.remotePlayer = TheOtherConnection(messageConnection).user.player;

							messageConnection.SendObject(res);

							var update = new OnMove();
							update.gameStatus = GameStatus.YourTurn;
							update.playTileAnimation = "not implemented";
							update.localPlayer = TheOtherConnection(messageConnection).user.player;
							update.remotePlayer = messageConnection.user.player;

							messageConnection.SendObject(update);
						} else {
							var res = new Status();
							res = Status.Fail;
							messageConnection.SendObject(res);
							Console.WriteLine("It's not this players turn");
						}
					} }
			};

			@switch[message.GetType()]();
		}

		private Connection TheOtherConnection(Connection theConnection) {
			if (theConnection.Equals(player1)) {
				return player2;
			} else {
				return player1;
			}
		}
	}
}

