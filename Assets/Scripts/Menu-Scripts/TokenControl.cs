using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {
	//SceneChanges, SceneControllerScript and FindToken all pull from here

	//player1 - pulls from array, works
	public SpriteRenderer currentSprite; //current token - (spriterenderer and other scripts pull from here)
	public Sprite[] spriteArray; 

	//player2 random
	public Sprite randomSprite; 
	int randomSpriteIndex; 

	//starting point for array - now token 1
	int tokenCounter = 2; 

	//buttons
	public Button leftButton; //left button
	public Button rightButton; //right button

	// Use this for initialization
	void Start () {
		//currentSprite - player1
		currentSprite = GetComponent<SpriteRenderer> (); //current token 
		currentSprite.sprite = spriteArray [tokenCounter]; //first token to show, starts from token 1 now

		//randomSprite - player2
		randomSpriteIndex = Random.Range(0, spriteArray.Length); 
		randomSprite = spriteArray [randomSpriteIndex]; 

		//buttons - left
		Button lbtn = leftButton.GetComponent<Button>(); //works, do not touch
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//buttons - right
		Button rbtn = rightButton.GetComponent<Button>(); //works, do not touch
		rbtn.onClick.AddListener (RightTaskOnClick);

	} 
	//left button task (player1)
	void LeftTaskOnClick() //do not touch
	{
		tokenCounter--;
		if (tokenCounter < 0) 
			tokenCounter = spriteArray.Length -1;
		currentSprite.sprite = spriteArray [tokenCounter];

	}
	//right button task (player1)
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


