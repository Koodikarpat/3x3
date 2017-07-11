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
            message.tiles = GameBoard();

			// ConnectionOfUser may return null
			ConnectionOfUser(user1).SendObject(message);

			message.gameStatus = GameStatus.RemoteTurn;
			message.localPlayer = user2.player;
			message.remotePlayer = user1.player;
            message.tiles = RotateBoard(message.tiles);

			ConnectionOfUser(user2).SendObject(message);

			Console.WriteLine("A new game has begun");
		}

        private MessageTile[] GameBoard()
        {
            MessageTile[] tileArray = new MessageTile[9];
            Random rnd = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                tileArray[i] = new MessageTile();
                int random = rnd.Next(0, 3);
                int strength = rnd.Next(1, 6);

                switch (random)
                {
                    case 0:
                        tileArray[i].type = MessageTileType.attack;
                        break;
                    case 1:
                        tileArray[i].type = MessageTileType.heal;
                        break;
                    case 2:
                        tileArray[i].type = MessageTileType.poison;
                        break;
                    default:
                        break;
                }
                tileArray[i].strength = strength;
            }

            return tileArray;
        }

        private MessageTile[] RotateBoard(MessageTile[] board)
        {
            MessageTile[] rotatedBoard = new MessageTile[9];

            for (int i = 0; i < 9; i++)
            {
                rotatedBoard[8 - i] = board[i];
            }

            return rotatedBoard;
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

							// TODO ConnectionOfUser may return null
							ConnectionOfUser(messageUser).SendObject(res);

                            Console.WriteLine("position: " + res.player.position);
							res.player.position = RotatedPosition(res.player.position); // this rotates theboard for player 2

							ConnectionOfUser(TheOtherUser(messageUser)).SendObject(res);

                            Console.WriteLine("sender user was: " + messageUser.username);
                            Console.WriteLine("other user was: " + TheOtherUser(messageUser).username);
                        } else {
							var res = new Status();
							res = Status.Fail;

							// TODO ConnectionOfUser may return null
							ConnectionOfUser(messageUser).SendObject(res);
							Console.WriteLine("It's not this players turn");
						}
					} }
			};

			Console.WriteLine("Some message was received");

			@switch[message.GetType()]();
		}

		private User TheOtherUser(User theUser) {
			if (theUser.Equals(user1)) {
				return user2;
			} else {
				return user1;
			}
		}

		private int RotatedPosition(int position) {
            return 8 - position;
		}

		private Connection ConnectionOfUser(User user) {
			return ConnectionOfUserCallback(user);
		}
	}
}

