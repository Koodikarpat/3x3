using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour { //might need renaming, this and SceneControllerScript
	//connected to - Buttons, will include all the scene change button functions
	public Button localMultiplayer;
	public Button Online;

	//for moving the player tokens (SceneControllerScript, TokenControl script)
	public GameObject SceneController;

			//player1 UNDER CONSTRUCTION--test
	public GameObject TokenControl;
	//public GameObject currentToken; 

		//player2 random  UNDER CONSTRUCTION
	//public GameObject randomTokenSprite; 

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void ChangeSceneLocalMultiplayer () //go to play scene (local multiplayer)
	{
		//move the right token to the play scene
		SceneControllerScript sceneControllerScript = SceneController.GetComponent<SceneControllerScript> ();//get script

			//player1 - works UNDER CONSTRUCTION
		TokenControl.GetComponent<TokenControl>().currentToken.transform.parent = sceneControllerScript.transform; 
		sceneControllerScript.currentToken = TokenControl.GetComponent<TokenControl>().currentToken;


			//player1 - new, testing script




			//player2 random - works UNDER CONSTRUCTION
		//Sprite randomSprite = TokenSprite.GetComponent<TokenControl>().randomSprite;
		//sceneControllerScript.randomTokenSprite = randomSprite;
			
		//and go to the right scene
		SceneManager.LoadScene ("mirkan scene");

	}
	public void ChangeSceneOnline () //loading Screen for Online version
	{
		SceneManager.LoadScene ("LoadingScreen"); //loading screen
	}
}
