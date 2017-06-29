using System;
using System.Collections;
using System.Collections.Generic;
using Networking;
using UnityEngine;

public class Multiplayer : MonoBehaviour {

	private Client client;
	private Player localPlayer;
	private Player remotePlayer;

	public bool isOnline;
	public bool isLocalTurn{ get; private set;}

	public GameObject localPlayerObject;
	public GameObject remotePlayerObject;

	// use this for initialization
	void Start()
	{
		isOnline = false;
		StartOnlineGame();
	}

	void Stop() // the socket must be closed when the scene is exited
	{
		if (isOnline) {
			client.Disconnect();
		}
	}

	public void StartOnlineGame()
	{
		isOnline = true;
		client = new Client("172.20.147.12");
		client.Connect();
		client.StartGame(GameUpdate);
	}

	public void MovePiece(int button) // call this when local player moves their piece
	{
		var msg = new Player();
		msg = localPlayer;
		msg.position = button;
		client.Move(msg);
	}

	private void GameUpdate(object message)
	{
		var @switch = new Dictionary<Type, Action> {
			{ typeof(Move), () => {
					var move = (Move)message;
					if (isLocalTurn) {  // this  move is a response to a local move
						isLocalTurn = false;
					} else { // remotePlayer made a move
						isLocalTurn = true;

						// TODO!: move player2 piece
					}

					// TODO: update tiles
					// TODO: animations by server
				} },
			{ typeof(GameInit), () => {
					var gameInit = (GameInit)message;
					if (gameInit.gameStatus == GameStatus.YourTurn) {
						isLocalTurn = false;
					} else {
						isLocalTurn = false;
					}

					// TODO: init tiles

					// TODO!: init pieces

				} }
		};

		@switch[message.GetType()]();
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
