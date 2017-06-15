using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {
	//kumman vuoro?
	public bool Player1 = true;
	public bool Player2 = false;
	//timer test- works!
	float timeLeft = 20.99f;
	//ajankääntö- works!
	float turnTime;

	private Text timer; //kello teksti
	//idk - vuoro
	Text playerTurn;
	public GameObject playerTurnText; 
	public GameObject timerText;
	public GameObject Player1Object;
	public GameObject Player2Object;

	// Use this for initialization
	void Start () {
		timer = timerText.GetComponent<Text> ();
		turnTime = timeLeft;
		playerTurn = playerTurnText.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("L:" + timeLeft);
		//timer/ajakääntö -works!
		timeLeft -= Time.deltaTime;
		timer.text = "" + Mathf.Floor (timeLeft);

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

