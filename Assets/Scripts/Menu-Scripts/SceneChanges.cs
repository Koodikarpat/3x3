﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour { 
	//connected to - Buttons, will include all the scene change button functions
	public Button localMultiplayer;
	public Button Online;

	//for moving the player tokens
	public GameObject SceneController;
	//player1
	public GameObject TokenSprite; 
	//player2 random 
	public GameObject randomTokenSprite; 

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

		//player1 - works
		SpriteRenderer currentSprite = TokenSprite.GetComponent<TokenControl>().currentSprite;
		sceneControllerScript.tokenSprite = currentSprite.sprite; 

		//player2 random - works
		Sprite randomSprite = TokenSprite.GetComponent<TokenControl>().randomSprite;
		sceneControllerScript.randomTokenSprite = randomSprite;
			
		//and go to the right scene
		SceneManager.LoadScene ("tommin scene");

	}
	public void ChangeSceneOnline () //loading Screen for Online version
	{
		SceneManager.LoadScene ("LoadingScreen"); //loading screen
	}
}
