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

	// Use this for initialization
	void Start()
	{
		client = new Client("172.20.147.12");
		client.Connect();
	}

	void Stop() // the socket must be closed when the scene is exited
	{
		client.Disconnect();
	}
	
	public void MovePiece(int button)
	{
		var msg = new Player();
		msg = localPlayer;
		msg.position = button;
		client.Move(msg);
	}
		
	private void OnMove()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
