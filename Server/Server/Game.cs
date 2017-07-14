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
        private GameStatus gameStatus; // current state of the game
        private int turn; // 1 when user1 is doing his turn, 2 when user2 is doing his turn
        private Timer timer; // timer for the turns

		public Game (User u1, User u2, Func<User, Connection> connectionOfUserCallback)
		{
			ConnectionOfUserCallback = connectionOfUserCallback;

			user1 = u1;
			user2 = u2;
		}

		public void Start()
		{
            gameStatus = new GameStatus();
            gameStatus.player1 = new PlayerStatus();
            gameStatus.player2 = new PlayerStatus();
            timer = new Timer();
            timer.gameManager = this;
            timer.Initialize();
			user1.player = new Player();
			user2.player = new Player();

			user1.player.position = 7;
			user2.player.position = 1;

            gameStatus.player1.Initialze(user1.player.username, user1.player.position, 20, 0);
            gameStatus.player2.Initialze(user1.player.username, user2.player.position, 20, 0);

            turn = 1;

            gameStatus.isPlayer1Turn = true;

			var message = new GameInit();
			message.gameStatus = Networking.GameStatus.YourTurn;
			message.localPlayer = user1.player;
			message.remotePlayer = user2.player;
            message.tiles = GameBoard(9);

            gameStatus.tiles = new MessageTile[9];
            gameStatus.tiles = message.tiles;

			// ConnectionOfUser may return null
			ConnectionOfUser(user1).SendObject(message);

			message.gameStatus = Networking.GameStatus.RemoteTurn;
			message.localPlayer = user2.player;
			message.remotePlayer = user1.player;
            message.tiles = RotateBoard(message.tiles);

			ConnectionOfUser(user2).SendObject(message);

            timer.ResetTimer();

            gameStatus.PrintStatus();
            Console.WriteLine("A new game has begun");
		}

        public void ChangeTurn()
        {
            Console.WriteLine("turn change");
            var message = new TurnChange();
            if (turn == 1)
            {
                message.turn = Networking.GameStatus.RemoteTurn;
                ConnectionOfUser(user1).SendObject(message);
                message.turn = Networking.GameStatus.YourTurn;
                ConnectionOfUser(user2).SendObject(message);
                turn = 2;
            }
            else
            {
                message.turn = Networking.GameStatus.YourTurn;
                ConnectionOfUser(user1).SendObject(message);
                message.turn = Networking.GameStatus.RemoteTurn;
                ConnectionOfUser(user2).SendObject(message);
                turn = 1;
            }
            gameStatus.isPlayer1Turn = !gameStatus.isPlayer1Turn;
            timer.ResetTimer();
        }

        private MessageTile[] GameBoard(int tileCount)
        {
            MessageTile[] tileArray = new MessageTile[tileCount];
            Random rnd = new System.Random();

            for (int i = 0; i < tileCount; i++)
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
                            MessageTile[] newTiles = GameBoard(1); // generate 1 new tile
                            res.newTile = newTiles[0];
                            gameStatus.ChangeTile(res.newTile, move.player.position, messageUser == user1);

							// TODO ConnectionOfUser may return null
							ConnectionOfUser(messageUser).SendObject(res);

                            Console.WriteLine("position: " + res.player.position);
							res.player.position = RotatedPosition(res.player.position); // this rotates theboard for player 2

							ConnectionOfUser(TheOtherUser(messageUser)).SendObject(res);

                            timer.ResetTimer();

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

