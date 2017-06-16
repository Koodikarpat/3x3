using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindToken : MonoBehaviour {
	//path= TokenControl - SceneControllerScript - SceneChanges

	//pysyvä objecti
	GameObject sceneController;

	//player1
	Image player1Token; //player1: toimii
	public GameObject player1;

	//player2
	Image player2RandomToken;//test -	WORKS!	
	public GameObject player2TokenTest; //test WORKS!	

	//health-images
	Image player1HealthImage;
	public GameObject player1Health;
	Image player2HealthImage;
	public GameObject player2Health;

	//get the object 
	void Awake (){
		sceneController = GameObject.Find ("SceneController"); //että löytää
	}

	// Use this for initialization
	void Start () {
		//get the script
		SceneControllerScript sceneControllerScript = sceneController.GetComponent<SceneControllerScript> (); //toimii - kerro että tämä on tämä

		//player 1
		player1Token = player1.GetComponent<Image> (); //toimii - että tietää minkä kuva komponentin
		player1Token.sprite = sceneControllerScript.tokenSprite; //toimii - currentSprite(TokenControl)= tokenSprite(SceneChanges - SceneControllerScript)= player1Token(FindToken)
		//player 2 -atm random
		player2RandomToken = player2TokenTest.GetComponent<Image> ();
		player2RandomToken.sprite = sceneControllerScript.randomTokenSprite;

		//health-images - player1
		player1HealthImage = player1Health.GetComponent<Image> ();
		player1HealthImage.sprite = sceneControllerScript.tokenSprite;
		//health-images - player2
		player2HealthImage = player2Health.GetComponent<Image> ();
		player2HealthImage.sprite = sceneControllerScript.randomTokenSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
