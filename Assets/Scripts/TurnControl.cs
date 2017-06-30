using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {
	//kumman vuoro?
	public bool Player1{get;private set;}
	public bool Player2{get;private set;}


	//timer test- works!
	float timeLeft = 20f;
	//ajankääntö- works!
	float turnTime;

	private Slider mySlider; //kello teksti
	//idk - vuoro
	Text playerTurn;
	public GameObject playerTurnText; 
	public GameObject TimeSlider;
	public GameObject Player1Object;
	public GameObject Player2Object;

	// Use this for initialization
	void Start () {
        mySlider = TimeSlider.GetComponent<Slider>();
        turnTime = timeLeft;
		playerTurn = playerTurnText.GetComponent<Text> ();

        Player1 = true;
		Player2 = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("L:" + timeLeft);
		//timer/ajakääntö -works!
		timeLeft -= Time.deltaTime;
        float value = timeLeft / 20;
        mySlider.value = value;

        {
			if (timeLeft < 0) 
			{

				ChangeTurn ();
			}
		}
	}
	public void ChangeTurn () //Vuoronvaihto
	{
		Player1 = !Player1;
		Player2 = !Player2;
		if (Player1) {
			playerTurn.text = ("Player 1");
			Player1Object.GetComponent <StatusEffects> ().tick ();
			//player-1-controlled
		}
		if (Player2) {
			playerTurn.text = ("Player 2");
			Player2Object.GetComponent <StatusEffects> ().tick ();
			//player-2-controlled
		}

		timeLeft = turnTime;
	}
}
// vuoronvaihto - napit - siirto, clickaus?
//pelaajanappi - 1
//pelaajanappi - 2 

