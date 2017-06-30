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
	public bool isLocalTurn{ get; private set; }

	public GameObject localPlayerObject;
	public GameObject remotePlayerObject;

	// use this for initialization
	void Start()
	{
		isOnline = false;
		StartOnlineGame();
	}

	void Stop() // TODO the client must be disconnected when the scene is exited
	{
		if (isOnline) {
			client.Disconnect();
		}
	}

	public void StartOnlineGame()
	{
		isOnline = true;
		System.Random random = new System.Random (); // TODO
		String username = SystemInfo.deviceUniqueIdentifier + random.Next (9999);
		client = new Client ("172.20.147.12", username: username); // this method has an optional 'token'
		client.Connect();
		client.StartGame(GameUpdate);
	}

	public void MovePiece(int button) // call this when local player moves their piece
	{
		Debug.Log("Send Move");
		var msg = new Player();
		msg = localPlayer;
		msg.position = button;
		client.Move(msg);
	}

	private void GameUpdate(object message)
	{
		var @switch = new Dictionary<Type, Action> {
			{ typeof(Move), () => {
					Debug.Log("A Move message was received");
					var move = (Move)message;
					if (isLocalTurn) {  // this  move is a response to a local move that the loca player just made
						isLocalTurn = false;
					} else { // remote player made a move
						isLocalTurn = true;

						// TODO!: move player2 piece
						remotePlayerObject.GetComponent<PlayerAbilities>().MoveButton(move.player.position);
					}

					// TODO: update tiles
					// TODO: animations by server
				} },
			{ typeof(GameInit), () => {
					Debug.Log("A GameInit message was received");
					var gameInit = (GameInit)message;
					if (gameInit.gameStatus == GameStatus.YourTurn) { // TODO init turncontroller
						isLocalTurn = true;
					} else {
						isLocalTurn = false;
					}

					localPlayer = gameInit.localPlayer;
					remotePlayer = gameInit.remotePlayer;

					// TODO: init tiles

					// TODO: init pieces with correct skins
				} }
		};

		@switch[message.GetType()]();
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
