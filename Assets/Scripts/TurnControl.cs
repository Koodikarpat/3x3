using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {
	//kumman vuoro?
	public bool Player1 = true;
	public bool Player2 = false;
	//timer test- works!
	float timeLeft = 10.99f;
	//ajankääntö- works!
	float turnTime;

	private Text timer; //kello teksti
	//idk - vuoro
	Text playerTurn;
	public GameObject playerTurnText; 
	public GameObject timerText;

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
		if (timeLeft < 0) {
			timeLeft += turnTime;
			// onko tämä tarpeen?
			ChangeTurn();
		}
	}
	public void ChangeTurn () //Vuoronvaihto
	{
		Player1 = !Player1;
		Player2 = !Player2;
		if (Player1) {
			playerTurn.text = ("Player 1");
			//player-1-controlled
		}
		if (Player2){
			playerTurn.text = ("Player 2");
			//player-2-controlled
		}
		}
}
// vuoronvaihto - napit - siirto, clickaus?
//pelaajanappi - 1
//pelaajanappi - 2 

