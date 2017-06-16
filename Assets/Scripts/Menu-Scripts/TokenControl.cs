using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {

	//player2 testing
	public Sprite randomSprite; //testing, random token
	int randomSpriteIndex; //testing, random token

	//player1
	public SpriteRenderer currentSprite; //current token - (spriterenderer and other scripts pull from here)
	public Sprite[] spriteArray; //current thingy letssee if this works

	//buttons
	public Button leftButton; //left button
	public Button rightButton; //right button
	int tokenCounter = 2; //starting point - now token 1

	// Use this for initialization
	void Start () {
			 //currentSprite - player1
		currentSprite = GetComponent<SpriteRenderer> (); //current token 
		currentSprite.sprite = spriteArray [tokenCounter]; //first token to show, starts from token 1 now

		//randomSprite - player2
		randomSpriteIndex = Random.Range(0, spriteArray.Length); //test
		randomSprite = spriteArray [randomSpriteIndex]; //test

		//buttons - left
		Button lbtn = leftButton.GetComponent<Button>(); //works, do not touch
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//buttons - right
		Button rbtn = rightButton.GetComponent<Button>(); //works, do not touch
		rbtn.onClick.AddListener (RightTaskOnClick);

	} 
	//left button task
	void LeftTaskOnClick() //do not touch
	{
		tokenCounter--;
		if (tokenCounter < 0) 
			tokenCounter = spriteArray.Length -1;
		currentSprite.sprite = spriteArray [tokenCounter];

	}
	//right button task
	void RightTaskOnClick () //do not touch
	{
		tokenCounter++;
		if (tokenCounter > spriteArray.Length -1) //se toimii!! ei tyhjää välissä
			tokenCounter = 0;
		currentSprite.sprite = spriteArray [tokenCounter];
	}
	// Update is called once per frame
	void Update () {
	}
}


