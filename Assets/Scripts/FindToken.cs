using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindToken : MonoBehaviour {

	GameObject sceneController;

	Image player1Token; //player1: toimii

	Image player2RandomToken;//test
	public GameObject player2TokenTest; //test

	void Awake (){
		sceneController = GameObject.Find ("SceneController"); //että löytää
	}

	// Use this for initialization
	void Start () {
		SceneControllerScript sceneControllerScript = sceneController.GetComponent<SceneControllerScript> (); //toimii - kerro että tämä on tämä
		player1Token = GetComponent<Image> (); //toimii - että tietää kuva komponentin
		player1Token.sprite = sceneControllerScript.tokenSprite; //toimii - currentSprite(TokenControl)= tokenSprite(SceneChanges - SceneControllerScript)= player1Token(FindToken)

		player2RandomToken = player2TokenTest.GetComponent<Image> ();
		player2RandomToken.sprite = sceneControllerScript.randomTokenSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
