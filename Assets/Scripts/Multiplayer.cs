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
    PlayerAbilities localPlayerAbilities;
    PlayerAbilities remotePlayerAbilities;
    public GameObject turnControllerObject;
    public GameObject tileObject;
    TilePlacements tilePlacements;
    ButtonSelection buttonSelection;
    TurnControl turnControlScript;

	private Stack<object> messageQueue;

	// use this for initialization
	void Start()
	{
        Application.runInBackground = true;
		if(isOnline)
			StartOnlineGame();

		messageQueue = new Stack<object>();
        tilePlacements = tileObject.GetComponent<TilePlacements>();
        buttonSelection = tileObject.GetComponent<ButtonSelection>();
        localPlayerAbilities = localPlayerObject.GetComponent<PlayerAbilities>();
        remotePlayerAbilities = remotePlayerObject.GetComponent<PlayerAbilities>();
        turnControlScript = turnControllerObject.GetComponent<TurnControl>();
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
                        ChangeTile(move);
                        isLocalTurn = false;
                        turnControlScript.ChangeTurn();
                    } else { // remote player made a move
						remotePlayerAbilities.MoveButton(move.player.position, remote: true);
                        ChangeTile(move, enemy: true);
                        isLocalTurn = true;
                        turnControlScript.ChangeTurn();
                    }
					// TODO: animations by server
				} },
            { typeof(TurnChange), () => {
                    Debug.Log("A TurnChange message was received");
                    var turnChange = (TurnChange)message;
                    if (turnChange.turn == GameStatus.YourTurn) {  // your turn
                        Debug.Log("localturn");
						isLocalTurn = true;
                        turnControlScript.ChangeTurn();
                    } else { // remote player's turn
                        Debug.Log("remoteturn");
						isLocalTurn = false;
                        turnControlScript.ChangeTurn();
                    }
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
						turnControlScript.ChangeTurn();
                    }

					localPlayer = gameInit.localPlayer;
					remotePlayer = gameInit.remotePlayer;

                    TileArray(gameInit); // create gameboard tiles

					// TODO: init pieces with correct skins

				} }
		};

		@switch[message.GetType()]();
	}

    void TileArray(GameInit gameInit)
    {
        for (int i = 0; i < 9; i++)
        {
            switch (gameInit.tiles[i].type)
            {
                case MessageTileType.attack:
                    buttonSelection.tiles[i].type = tilePlacements.GetEffect(0, gameInit.tiles[i].strength);
                    buttonSelection.tiles[i].position = buttonSelection.tiles[i].gameObject.transform.position;
                    tilePlacements.CreateTile(buttonSelection.tiles[i], i);
                    continue;
                case MessageTileType.heal:
                    buttonSelection.tiles[i].type = tilePlacements.GetEffect(1, gameInit.tiles[i].strength);
                    buttonSelection.tiles[i].position = buttonSelection.tiles[i].gameObject.transform.position;
                    tilePlacements.CreateTile(buttonSelection.tiles[i], i);
                    continue;
                case MessageTileType.poison:
                    buttonSelection.tiles[i].type = tilePlacements.GetEffect(2, gameInit.tiles[i].strength);
                    buttonSelection.tiles[i].position = buttonSelection.tiles[i].gameObject.transform.position;
                    tilePlacements.CreateTile(buttonSelection.tiles[i], i);
                    continue;
            }
        }
    }

    int TileType(MessageTileType type)
    {
        switch (type)
        {
            case MessageTileType.attack:
                return 0;
            case MessageTileType.heal:
                return 1;
            case MessageTileType.poison:
                return 2;
        }
        Debug.Log("Wrong tile type, setting tile to attack type instead");
        return 0;
    }

    void ChangeTile(Move move, bool enemy = false)
    {
        int type = TileType(move.newTile.type);
        if (!enemy) localPlayerAbilities.ChangeTile(type, move.newTile.strength);
        else remotePlayerAbilities.ChangeTile(type, move.newTile.strength);
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
