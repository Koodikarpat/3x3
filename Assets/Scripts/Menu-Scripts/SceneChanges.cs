using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour { //might need renaming, this and SceneControllerScript
	//connected to - Buttons, will include all the scene change button functions

	//button objects
	public Button localMultiplayer;
	public Button Online;

	//for moving the player tokens (SceneControllerScript, (TokenControl script))
	public GameObject SceneController;

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
		SceneControllerScript sceneControllerScript = SceneController.GetComponent<SceneControllerScript> ();//get script

			//player1 - works again, new
		TokenControl.GetComponent<TokenControl>().currentToken.transform.parent = SceneController.transform; 
		sceneControllerScript.currentToken = TokenControl.GetComponent<TokenControl>().currentToken;
		//because the shade-cursed thing moves. doesn't work completely yet
		sceneControllerScript.currentToken.transform.position = Vector3.zero;

		//player2 random -new, MOVING THIS TO SCENECHANGES lets see what breaks
		TokenControl tokenControl = TokenControl.GetComponent<TokenControl> (); //get script
		prefabArray = tokenControl.prefabArray;

		randomPrefabIndex = Random.Range (0, prefabArray.Length);
		if (randomPrefabIndex == tokenControl.tokenCounter)
				randomPrefabIndex++;
		if (randomPrefabIndex > prefabArray.Length - 1)
			randomPrefabIndex = 0;
		randomToken = SetPrefab2 (randomPrefabIndex);
				

		//player2 -DO NOT TOUCH, it works
		randomToken.transform.parent = sceneControllerScript.transform; //PARENT = SCENECONTROLLER
		sceneControllerScript.randomToken = randomToken;
		//because the shade-cursed thing moves. doesn't work completely yet
		randomToken.transform.position = Vector3.zero; 
			
		//and go to the right scene
		SceneManager.LoadScene ("mirkan scene");

	}
	public void ChangeSceneOnline () //loading Screen for Online version
	{
		SceneManager.LoadScene ("LoadingScreen"); //loading screen
	}
	//player2 - makes an object out of the prefab
	GameObject SetPrefab2 (int randomPrefabIndex) //IT WORKS
	{
		Destroy (randomToken);
		return Instantiate (prefabArray [randomPrefabIndex], transform);
	}
}
