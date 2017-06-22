using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {
	//SceneChanges, KeepTheseScript and FindToken all pull from here

	//player1 - pulls from array, works again
	public GameObject currentToken; //current token - (currentToken GameObject and other scripts pull from here)


	//token array
	public GameObject[] prefabArray; 
	public int tokenCounter = 1; 	//starting point for array - can change freely

	//----------------------------------------------------------------

	//buttons
	public Button leftButton; //left button
	public Button rightButton; //right button

	// Use this for initialization
	void Start () {
		
		//player1 - works again
		currentToken = SetPrefab1(tokenCounter); //first token to show

		//---------------------------------------------------------------------------------
		//buttons - left
		Button lbtn = leftButton.GetComponent<Button>(); //works, do not touch
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//buttons - right
		Button rbtn = rightButton.GetComponent<Button>(); //works, do not touch
		rbtn.onClick.AddListener (RightTaskOnClick);
		//----------------------------------------------------------------------------
	} 
	//player1 - makes an object out of the prefab
	GameObject SetPrefab1 (int tokenCounter) //DO NOT TOUCH, IT FINALLY WORKS
	{
		Destroy (currentToken);
		return Instantiate (prefabArray [tokenCounter], transform);

	}
	//-------------------------------------------------------------------------------
	//left button task (player1) works again
	void LeftTaskOnClick() //do not touch
	{
		tokenCounter--;
		if (tokenCounter < 0) 
			tokenCounter = prefabArray.Length -1;
		currentToken = SetPrefab1(tokenCounter);

	}
	//right button task (player1) works again
	void RightTaskOnClick () //do not touch
	{
		tokenCounter++;
		if (tokenCounter > prefabArray.Length -1) //se toimii!! ei tyhjää välissä
			tokenCounter = 0;
		currentToken = SetPrefab1(tokenCounter);

	}
	// Update is called once per frame
	void Update () {
	}
}


