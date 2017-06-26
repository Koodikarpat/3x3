using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Networking;
using System.Threading;

public class SceneChanges : MonoBehaviour { //might need renaming, this. KeepTheseScriptScript = SceneControllerScript, renamed to something more fitting.
	//connected to - Buttons, will include all the scene change button functions

	//button objects
	public Button localMultiplayer;
	public Button Online;

	//for moving the player tokens (KeepTheseScriptScript, (TokenControl script))
	public GameObject ObjectKeeper;

	//player1 - new, works again
	public GameObject TokenControl;

	//player2 random - new, MOVED FROM TOKENCONTROL
	public GameObject randomToken;
	int randomPrefabIndex;
	public GameObject[] prefabArray;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame.
	void Update () {

	}
	public void ChangeSceneLocalMultiplayer () //go to play scene (local multiplayer)
	{
		//move the right token to the play scene
		KeepTheseScript keepTheseScript = ObjectKeeper.GetComponent<KeepTheseScript> ();//get script

			//player1 - works again, new
		TokenControl.GetComponent<TokenControl>().currentToken.transform.parent = ObjectKeeper.transform; 
		keepTheseScript.currentToken = TokenControl.GetComponent<TokenControl>().currentToken;
		//because the shade-cursed thing moves. doesn't work completely yet
		Vector3 currentPosition = keepTheseScript.currentToken.transform.position;
		currentPosition.z = 0f;
		keepTheseScript.currentToken.transform.position = currentPosition;

		//player2 random -new, MOVING THIS TO SCENECHANGES lets see what breaks
		TokenControl tokenControl = TokenControl.GetComponent<TokenControl> (); //get script
		prefabArray = tokenControl.prefabArray;

		//randomizing, shouldn't be same as 1 but atm is. FIX
		randomPrefabIndex = Random.Range (0, prefabArray.Length);
		if (randomPrefabIndex == tokenControl.tokenCounter)
				randomPrefabIndex++;
		if (randomPrefabIndex > prefabArray.Length - 1)
			randomPrefabIndex = 0;
		randomToken = SetPrefab2 (randomPrefabIndex);
				

		//player2 -DO NOT TOUCH, it works
		randomToken.transform.parent = keepTheseScript.transform; //PARENT = ObjectKeeper
		keepTheseScript.randomToken = randomToken;
		//because the shade-cursed thing moves. doesn't work completely yet
		randomToken.transform.position = Vector3.zero; 
			
		//and go to the right scene
		//SceneManager.LoadScene ("mirkan scene");

	}
	public void ChangeSceneOnline () //loading Screen for Online version
	{
		
			//SceneManager.LoadScene ("LoadingScreen"); //loading screen
		Client client = new Client("172.20.146.40");
		client.Connect ();
		Thread.Sleep (1000);
		client.Disconnect ();
	}

	//player2 - makes an object out of the prefab
	GameObject SetPrefab2 (int randomPrefabIndex) //IT WORKS
	{
		Destroy (randomToken);
		return Instantiate (prefabArray [randomPrefabIndex], transform);
	}
}
