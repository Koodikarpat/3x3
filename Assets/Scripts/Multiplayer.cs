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
	public GameObject turnControllerObject;

	private Stack<object> messageQueue;

	// use this for initialization
	void Start()
	{
        Application.runInBackground = true;
        isOnline = true; //TODO: rautalankafixi
		if(isOnline)
			StartOnlineGame();

		messageQueue = new Stack<object>();
	}

	void Stop() // TODO the client must be disconnected when the scene is exited
	{
		if (isOnline) {
			client.Disconnect();
		}
	}

	public void StartOnlineGame()
	{
		
		System.Random random = new System.Random (); // TODO
		String username = SystemInfo.deviceUniqueIdentifier + random.Next (9999);
        Debug.Log(username);
        localPlayer = new Player();
        localPlayer.username = username;
		client = new Client ("172.20.146.40", username: username); // this method has an optional 'token'
		client.Connect();
		client.StartGame(OnGameUpdate);
	}

	public void MovePiece(int button) // call this when local player moves their piece
	{
		Debug.Log("Send Move");
		var msg = new Player();
		msg = localPlayer;
		msg.position = button;
		client.Move(msg);
	}

	private void OnGameUpdate(object message) // client callbacks this function when a message is received
	{
		messageQueue.Push(message);
	}

	private void UpdateGame(object message) // the 'global state' of the 'game' is maintained by this function when isOnline, its called by Update()
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
						remotePlayerObject.GetComponent<PlayerAbilities>().MoveButton(move.player.position, remote: true);
					}

					// TODO: update tiles
					// TODO: animations by server
				} },
			{ typeof(GameInit), () => {
					Debug.Log("A GameInit message was received");
					var gameInit = (GameInit)message;
					if (gameInit.gameStatus == GameStatus.YourTurn) { // TODO init turncontroller
						isLocalTurn = true;
						// local player gets to start so turn controller is set correctly
					} else {
						isLocalTurn = false;
						// remote player starts Warning: If game init is sent more than once during a game this will cause a race condition
						turnControllerObject.GetComponent<TurnControl>().ChangeTurn();
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
	void Update()
	{
		try {
			UpdateGame(messageQueue.Pop());
		} catch {
			// InvalidOperationException: stack is empty
		}
	}
}
