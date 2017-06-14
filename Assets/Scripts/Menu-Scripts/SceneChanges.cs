using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour { //connected to - Buttons, will include all the scene change button functions

	public Button localMultiplayer;
	public GameObject SceneController;
	public GameObject TokenSprite; //player1, works
	public GameObject randomTokenSprite; //player2 for now, random test

	public Button Online;


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
		SpriteRenderer currentSprite = TokenSprite.GetComponent<TokenControl>().currentSprite;//define currentsprite
		sceneControllerScript.tokenSprite = currentSprite.sprite; //define tokenSprite as currentSprite= toimii! ottaa oikean kuvan

		//player2 random, test???
		Sprite randomSprite = TokenSprite.GetComponent<TokenControl>().randomSprite;//test
		sceneControllerScript.randomTokenSprite = randomSprite;//test 
			
		SceneManager.LoadScene ("tommin scene");

	}
	public void ChangeSceneOnline ()
	{
		SceneManager.LoadScene ("LoadingScreen");
	}
}
