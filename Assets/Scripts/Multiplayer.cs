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
	public GameObject playerAbilitiesObject;

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

	public void MovePiece(int button)
	{
		var msg = new Player();
		msg = localPlayer;
		msg.position = button;
		client.Move(msg);
	}

	private void GameUpdate(object message)
	{
		var @switch = new Dictionary<Type, Action> {
			{ typeof(OnMove), () => {
					var onMove = (OnMove)message;
					localPlayerObject.GetComponent<PlayerAbilities>().MoveButton(onMove.localPlayer.position);
					remotePlayerObject.GetComponent<PlayerAbilities>().MoveButton(onMove.remotePlayer.position);

					if (onMove.gameStatus == GameStatus.YourTurn) {
						isLocalTurn = true;
					} else {
						isLocalTurn = false;
					}

					// TODO: update tiles
				} }
		};

		@switch[message.GetType()]();
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
