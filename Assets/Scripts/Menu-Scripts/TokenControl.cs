using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {

	public SpriteRenderer currentSprite; //current token
	public Sprite[] spriteArray; //current thingy letssee if this works

	public Button leftButton; //left button
	public Button rightButton; //right button
	int tokenCounter = 2; //starting point - now token 1

	//to keep
	void Awake ()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	// Use this for initialization
	void Start () {
			 
		currentSprite = GetComponent<SpriteRenderer> (); //current token
		currentSprite.sprite = spriteArray [tokenCounter]; //first token to show, starts from token 1 now

		Button lbtn = leftButton.GetComponent<Button>(); //left button
		lbtn.onClick.AddListener (LeftTaskOnClick);
		//run if/else that if at min value, disable button to scroll left

		Button rbtn = rightButton.GetComponent<Button>(); //right button
		rbtn.onClick.AddListener (RightTaskOnClick);
		//run if/else that if at max value, disable button to scroll right.
	} 

	void LeftTaskOnClick() //left button
	{
		tokenCounter--;
		if (tokenCounter < 0) //tyhjä välissä idk what to do
			tokenCounter = spriteArray.Length -1;
		currentSprite.sprite = spriteArray [tokenCounter];

	}

	void RightTaskOnClick () //right button..
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


