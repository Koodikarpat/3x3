using System;
using System.Collections.Generic;

// Message.cs and Parser.cs must be manually kept identical in Server/Server/ and Assets/Client
// Server/verifyShared.sh can be used as a git hook

namespace Networking
{
    // this class defines the types of messages that can be sent
    // dont send the message object itself using SendObject
    // instead you should send one the message types
    // WARNING: if you make changes to this class you MUST also update Parser.cs

    /*
    ATTENTION!!!
    If you want to add new message type, be sure to check each of the following scripts and make sure each of them
    has handling for the message:

    Server side:
    -Message.cs
    -Parser.cs
    -Game.cs (this only if you want the server to be able to handle this type of messages from clients)

    Client side:
    -Message.cs
    -Parser.cs
    -Client.cs
    -Multiplayer.cs (this only if you want the clients to be able to handle this type of messages from server)

    Be sure to check all those scripts if your new message type isn't working.
    */

    public class Message
    {
        public Status status; // status can be sent from either the server or the client

        public AuthenticationRequest authenticationRequest; // can be sent by the client
        public AuthenticationResponse authenticationResponse; // can be sent by the server

        public Move move; // sent by the client when the clients want's to move a piece

        public GameInit gameInit; // the server initializes the the clients

        public TurnChange turnChange; // sent by server, when it is time to change the turn
    }

    // MESSAGE TYPES, use these to send messages
    //
    // you can send messages like this
    // var message = new MESSAGETYPE();
    // message.variable = value;
    // SendObject(message);
    //
    // you can receive messages in Message.cs GameUpdate() method
    //
    // you can make changes to existing classes without much effort
    // however if you want a new message type you must add it to the Message class

    public class GameInit
    {
        public GameStatus gameStatus;
        public Player localPlayer;
        public Player remotePlayer;
        public MessageTile[] tiles;
    }

    public class Move
    {
        public Player player;
        public MessageTile newTile; // the server must select the tile
    }

    public class TurnChange
    {
        public Player playerUpNext;
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

    // TYPES USED IN MESSAGES, these cant be sent or received on their own
    // these can be changed and added with no effort, however be careful with null values

    public class Player
    {
        public string profileId;
        public string username;
        public int position;
        public Piece piece;
    }

    public class MessageTile
    {
        public MessageTileType type;
        public int strength;
    }

    public enum Status { None, Ok, Fail };
    public enum GameStatus { Waiting, YourTurn, RemoteTurn, Ended };
    public enum MessageTileType { attack, heal, poison };
    public enum Piece { pice1, piece2 }; // TODO this should represent actual pieces
}