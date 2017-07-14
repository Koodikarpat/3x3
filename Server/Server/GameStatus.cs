using System;
using Networking;
using System.Collections.Generic;

namespace Server
{
    //remember to keep these values up to date during the game
    public class GameStatus
    {
        public MessageTile[] tiles; // contains the current set of tiles on the board
        public PlayerStatus player1; // status of the first connected player
        public PlayerStatus player2; // ^^ second connected player
        public bool isPlayer1Turn; // true = player1's turn, false = player2's turn

        public void PrintStatus()
        {
            Console.WriteLine("-----");
            Console.WriteLine("GAMESTATUS:");
            Console.WriteLine("GameBoard: ");
            for (int i = 0; i < tiles.Length; i++)
            {
                if (i != tiles.Length - 1) Console.Write(tiles[i].type + ": " + tiles[i].strength + " | ");
                else Console.WriteLine(tiles[i].type + ": " + tiles[i].strength + " | ");
            }
            Console.WriteLine("Player1: " + player1.username + " (position: " + player1.position + " health: " + player1.health
                + " poison: " + player1.poison + ")");
            Console.WriteLine("Player2: " + player2.username + " (position: " + player2.position + " health: " + player2.health
                + " poison: " + player2.poison + ")");
            Console.WriteLine("Is it player1's turn? " + isPlayer1Turn);
            Console.WriteLine("-----");
        }

        public void ChangeTile(MessageTile tile, int pos, bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                tiles[player1.position] = tile;
                player1.position = pos;
            }
            else
            {
                tiles[player2.position] = tile;
                player2.position = pos;
            }
            PrintStatus();
        }
    }

    public class PlayerStatus
    {
        public string username;
        public int position;
        public int health;
        public int poison;

        public void Initialze(string name, int pos, int hp, int ps)
        {
            username = name;
            position = pos;
            health = hp;
            poison = ps;
        }
    }
}
