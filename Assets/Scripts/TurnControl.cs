using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {
	//kumman vuoro?
	bool Player1 = true;
	bool Player2 = false;
	//timer test- works!
	public float timeLeft = 10.0f;

	public Text textComponent; 
	public Text playerTurn;


	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//timer test -works!
		timeLeft -= Time.deltaTime;
		textComponent.text = ""+ Mathf.Floor(timeLeft);
		if(timeLeft < 0)
			 
		{
			// tämä muuttuu vielä
			 //ChangeTurn();
			 //if textComponent.text = "JEE";
			//PlayerTurn.text = ("Player 1");
				//if bool= false //tai if TurnControl.Player2 ? 
		}
	}
}
// funktio vuoronvaihto idk still writing - aika resetoi, vaihtaa nappi kontrolli, kortti kontrolli, teksti
// void ChangeTurn
// if bool=true 
// if bool=false

		
//timeLeft= <0 
	//boolean=true	
	//if boolean=true
		//boolean=false
