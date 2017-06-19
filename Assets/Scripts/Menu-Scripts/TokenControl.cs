using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {
	//SceneChanges, SceneControllerScript and FindToken all pull from here

	//player1 - pulls from array, UNDER CONSTRUCTION
	public GameObject currentToken; //current token - (currentToken GameObject and other scripts pull from here)
	public GameObject[] prefabArray; 

	//player2 random UNDER CONSTRUCTION
	public GameObject randomToken; 
	int randomPrefabIndex; 

	//starting point for array - can change freely
	int tokenCounter = 1; 

	//buttons
	public Button leftButton; //left button
	public Button rightButton; //right button

	// Use this for initialization
	void Start () {
				//currentSprite - player1 UNDER CONSTRUCTION
		//currentSprite = GetComponent<SpriteRenderer> (); //current token 
		currentToken = prefabArray [tokenCounter]; //first token to show

		//randomSprite - player2 UNDER CONSTRUCTION
		randomPrefabIndex = Random.Range(0, prefabArray.Length); 
		randomToken = prefabArray [randomPrefabIndex]; 

		//buttons - left
		Button lbtn = leftButton.GetComponent<Button>(); //works, do not touch
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//buttons - right
		Button rbtn = rightButton.GetComponent<Button>(); //works, do not touch
		rbtn.onClick.AddListener (RightTaskOnClick);

	} 
	//left button task (player1) UNDER CONSTRUCTION
	void LeftTaskOnClick() //do not touch
	{
		tokenCounter--;
		if (tokenCounter < 0) 
			tokenCounter = prefabArray.Length -1;
		currentToken = prefabArray [tokenCounter];

	}
	//right button task (player1) UNDER CONSTRUCTION
	void RightTaskOnClick () //do not touch
	{
		tokenCounter++;
		if (tokenCounter > prefabArray.Length -1) //se toimii!! ei tyhjää välissä
			tokenCounter = 0;
		currentToken = prefabArray [tokenCounter];

	}
	// Update is called once per frame
	void Update () {
	}
}


