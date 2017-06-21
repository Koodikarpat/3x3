using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindToken : MonoBehaviour {
	//path= TokenControl - SceneControllerScript - SceneChanges

	//pysyvä objecti
	GameObject sceneController;

	//player1: toimi -ei enää
	//Image player1Token; 
	public GameObject player1; //don't touch this, important

	//player2: old
	//Image player2RandomToken;//test -	WORKS!	
	//public GameObject player2TokenTest; //test WORKS!	

	//health-images
	Image player1HealthImage;
	public GameObject player1Health;
	//testing
	SpriteRenderer token1Image;

	//Image player2HealthImage;
	//public GameObject player2Health;

	//get the object 
	void Awake (){
		sceneController = GameObject.Find ("SceneController"); //että löytää

	}

	// Use this for initialization
	void Start () {
		//get the script
		SceneControllerScript sceneControllerScript = sceneController.GetComponent<SceneControllerScript> (); //toimii - kerro että tämä on tämä

		//player1 new script works
		sceneControllerScript.currentToken.transform.parent = player1.transform; 

			//player 1 -old
			//player1Token = player1.GetComponent<Image> (); //toimii - että tietää minkä kuva komponentin
			//player1Token.sprite = sceneControllerScript.tokenSprite; //toimii - currentSprite(TokenControl)= tokenSprite(SceneChanges - SceneControllerScript)= player1Token(FindToken)


			//player 2 -atm random - old
			//player2RandomToken = player2TokenTest.GetComponent<Image> ();
			//player2RandomToken.sprite = sceneControllerScript.randomTokenSprite;

			//health-images - player1 - old
			//player1HealthImage = player1Health.GetComponent<Image> ();
			//player1HealthImage = sceneControllerScript.currentToken; NOT THE OLD WHAT WAS THIS

		//health-images - player1 - new, UNDER CONSTRUCTION
		player1HealthImage = player1Health.GetComponent<Image> (); //is the image component of the object

		token1Image = sceneControllerScript.currentToken.GetComponent<SpriteRenderer> (); //is the image component of currentToken
		player1HealthImage.sprite = token1Image.sprite;

		//health-images - player2 - old
		//player2HealthImage = player2Health.GetComponent<Image> ();
		//player2HealthImage.sprite = sceneControllerScript.randomTokenSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
