using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindToken : MonoBehaviour {
	//path= TokenControl - KeepTheseScript - SceneChanges

	//pysyvä objecti
	GameObject objectKeeper;
 
	//players
	public GameObject player1; //don't touch this, important
	public GameObject player2;

	//health-images -1
	Image player1HealthImage;
	public GameObject player1Health;
	SpriteRenderer token1Image;

	//health-images -2
	Image player2HealthImage;
	public GameObject player2Health;
	SpriteRenderer token2Image;


	//get the object 
	void Awake (){
		objectKeeper = GameObject.Find ("ObjectKeeper"); //että löytää

	}

	// Use this for initialization
	void Start () {
		//get the script
		KeepTheseScript keepTheseScript = objectKeeper.GetComponent<KeepTheseScript> (); //toimii - kerro että tämä on tämä

		//player1 new script works
		Vector3 currentPosition = keepTheseScript.currentToken.transform.localPosition;
		keepTheseScript.currentToken.transform.parent = player1.transform; 

		//position

		keepTheseScript.currentToken.transform.localPosition = currentPosition;


		//player2 new script, WORKS
		keepTheseScript.randomToken.transform.parent = player2.transform; 

		//------------------------------------------------------------------------------------------------------------------

		//health-images - player1 - new, IT WORKS FINALLY
		player1HealthImage = player1Health.GetComponent<Image> (); //is the image component of the object
		token1Image = keepTheseScript.currentToken.GetComponent<SpriteRenderer> (); //is the image component of currentToken
		player1HealthImage.sprite = token1Image.sprite;

		//health-images - player2 - new, writing
		player2HealthImage = player2Health.GetComponent<Image> ();
		token2Image = keepTheseScript.randomToken.GetComponent<SpriteRenderer> ();
		player2HealthImage.sprite = token2Image.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
