using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {
	//SceneChanges, SceneControllerScript and FindToken all pull from here

	//player1 - pulls from array, works again
	public GameObject currentToken; //current token - (currentToken GameObject and other scripts pull from here)
	public GameObject[] prefabArray; 

	//player2 random UNDER CONSTRUCTION
	//public GameObject randomSprite; 
	//int randomPrefabIndex; 

	//player2 random new
	public GameObject randomToken;
	int randomPrefabIndex;

	//starting point for array - can change freely
	int tokenCounter = 1; 

	//buttons
	public Button leftButton; //left button
	public Button rightButton; //right button

	// Use this for initialization
	void Start () {
				//currentSprite - player1 works again
		currentToken = SetPrefab(tokenCounter); //first token to show

		//randomSprite - player2 UNDER CONSTRUCTION - makes extra object on mainmenu
		//randomPrefabIndex = Random.Range(0, prefabArray.Length); 
		//randomSprite = SetPrefab(randomPrefabIndex); 

		//player2 new
		//random

		//buttons - left
		Button lbtn = leftButton.GetComponent<Button>(); //works, do not touch
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//buttons - right
		Button rbtn = rightButton.GetComponent<Button>(); //works, do not touch
		rbtn.onClick.AddListener (RightTaskOnClick);

	} 
	//left button task (player1) works again
	void LeftTaskOnClick() //do not touch
	{
		tokenCounter--;
		if (tokenCounter < 0) 
			tokenCounter = prefabArray.Length -1;
		currentToken = SetPrefab(tokenCounter);

	}
	//right button task (player1) works again
	void RightTaskOnClick () //do not touch
	{
		tokenCounter++;
		if (tokenCounter > prefabArray.Length -1) //se toimii!! ei tyhjää välissä
			tokenCounter = 0;
		currentToken = SetPrefab(tokenCounter);

	}//makes an object out of the prefab, new stuff
	GameObject SetPrefab (int tokenCounter)
	{
		Destroy (currentToken);
		return Instantiate (prefabArray [tokenCounter], transform);

	}
	// Update is called once per frame
	void Update () {
	}
}


