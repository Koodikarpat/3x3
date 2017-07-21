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
        private int turn; // 1 when user1 is doing his turn, 2 when user2 is doing his turn
        private Timer timer; // timer for the turns
        public MessageTile[] tiles;

        public Game(User u1, User u2, Func<User, Connection> connectionOfUserCallback)
        {
            ConnectionOfUserCallback = connectionOfUserCallback;

            user1 = u1;
            user2 = u2;
        }

        public void Start()
        {
           
            timer = new Timer();
            timer.gameManager = this;
            timer.Initialize();
            user1.player = new Player();
            user2.player = new Player();

            user1.player.position = 7;
            user2.player.position = 1;

            user1.player.health = 20;
            user2.player.health = 20;

            user1.player.poison = 0;
            user2.player.poison = 0;

            Random rand = new Random();
            user1.player.profileId = ""+rand.Next(0,999999);
            user2.player.profileId = "" + rand.Next(0, 999999);

            turn = 1;

            var message = new GameInit();
            message.gameStatus = Networking.GameStatus.YourTurn;
            message.localPlayer = user1.player;
            message.remotePlayer = user2.player;
            message.tiles = GameBoard(9);

            tiles = message.tiles;

            // ConnectionOfUser may return null
            ConnectionOfUser(user1).SendObject(message);

            message.gameStatus = Networking.GameStatus.RemoteTurn;
            message.localPlayer = user2.player;
            message.remotePlayer = user1.player;
            message.tiles = RotateBoard(message.tiles);

            ConnectionOfUser(user2).SendObject(message);

            timer.ResetTimer();

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

            timer.ResetTimer();

            Console.WriteLine("GAMESTATUS:");
            Console.WriteLine("Player1: " + user1.player.username + " (position: " + user1.player.position + " health: " + user1.player.health
                + " poison: " + user1.player.poison + ")");
            Console.WriteLine("Player2: " + user2.player.username + " (position: " + user2.player.position + " health: " + user2.player.health
                + " poison: " + user2.player.poison + ")");
            Console.WriteLine("*******************************************");
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
                        if (true)
                        { 
                            // TODO check if it is this users turn, is the move legal
							var res = new Move();
                            res.player = move.player;
                            int start;
                            int end;
                            int enemy;
                            Console.WriteLine("*******************************************");
                            if (turn == 1)
                            {
                                start = user1.player.position;
                                end = res.player.position;
                                enemy = RotatedPosition(user2.player.position);
                                Console.WriteLine("Player 1 position is: " + start + ". Player moves to: " + end + ".");
                            }
                            else
                            {
                                start = user2.player.position;
                                end = RotatedPosition(res.player.position);
                                enemy = user1.player.position;
                                Console.WriteLine("Player 2 position is: " + start + ". And moves to: " + end + ".");
                            }

                            if(legalMove(start, end, enemy))
                            {
                                Console.WriteLine("*** *** *** ***    LEGAL    *** *** *** ***");
                            }
                            else
                            {
                                Console.WriteLine("! *** ! *** !      ILLEGAL      ! *** ! *** !");
                            }



                            //TODO STILL NEED TO RESTRICT THEM FROM OVERLAPPING!!
                            MessageTile[] newTiles = GameBoard(1); // generate 1 new tile
                            res.newTile = newTiles[0];
                            ChangeTile(res.newTile, end);


							// TODO ConnectionOfUser may return null
                            res.player.position = end;
							ConnectionOfUser(user1).SendObject(res);

                            Console.WriteLine("player position to 1: " + res.player.position);
                            res.player.position = RotatedPosition(end); // this rotates the board for player 2
                            Console.WriteLine("player position flipped to 2: " + res.player.position);
							ConnectionOfUser(user2).SendObject(res);

                            //timer.ResetTimer();

                            Console.WriteLine("sender user was: " + messageUser.username);
                            Console.WriteLine("other user was: " + TheOtherUser(messageUser).username);
                            ChangeTurn();
                        }
                        else
                        {
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

        private void ChangeTile(MessageTile tile, int pos)
        {
            if (turn == 1)
            {
                tiles[user1.player.position] = tile;
                user1.player.position = pos;
            }
            else
            {
                tiles[user2.player.position] = tile;
                user2.player.position = pos;
            }
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

        private bool legalMove(int start, int end, int enemy)
        {
            if (end == enemy)
                return false;

            if ((start - 3 == end) || (start + 3 == end))
                return true;

            if (start + 1 == end)
            {
                if ((start == 2) || (start == 5))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (start - 1 == end)
            {
                if ((start == 3) || (start == 6))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}

