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

	private Slider mySlider; //kelloteksti
	//idk - vuoro
	Text playerTurn;
	public GameObject playerTurnText; 
	public GameObject TimeSlider;
	public GameObject Player1Object;
	public GameObject Player2Object;

    public CardHandler p1CHandler, p2CHandler;

    public bool timerStarted;
    public Multiplayer multiplayer;

	// Use this for initialization
	void Start () {
        mySlider = TimeSlider.GetComponent<Slider>();
        turnTime = timeLeft;
		playerTurn = playerTurnText.GetComponent<Text> ();
        timerStarted = true;

        Player1 = true;
		Player2 = false;

        if (!multiplayer.isOnline)
        {
            p1CHandler.DrawCards();
            p2CHandler.DrawCards();
        }

        p1CHandler.ShowCards();
        p2CHandler.HideCards();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!timerStarted) return;

		timeLeft -= Time.deltaTime;
        float value = timeLeft / turnTime;
        mySlider.value = value;

        if (timeLeft <= 0)
        {
            timerStarted = false;
            timeLeft = turnTime;
            ChangeTurn();
        }
	}
	public void ChangeTurn () //Vuoronvaihto
	{
        if (!multiplayer.isOnline)
        {
            if (!p1CHandler.HasCards()) p1CHandler.DrawCards();
            if (!p2CHandler.HasCards()) p2CHandler.DrawCards();
        }

        p1CHandler.HideCards();
        p2CHandler.HideCards();


        Player1 = !Player1;
		Player2 = !Player2;
        Debug.Log("P1: " + Player1 + "P2: " + Player2);
        if (multiplayer.isOnline)
        {
            if (multiplayer.isPlayer1 && Player1)
            {
                playerTurn.text = ("Player 1");
                Player1Object.GetComponent<StatusEffects>().Tick();
                p1CHandler.ShowCards();
                p2CHandler.HideCards();
                //player-1-controlled
            }
            else if (!multiplayer.isPlayer1 && Player1)
            {
                playerTurn.text = ("Player 1");
                Player1Object.GetComponent<StatusEffects>().Tick();
                p1CHandler.ShowCards();
                p2CHandler.HideCards();
                //player-1-controlled
            }
            else if (Player2)
            {
                playerTurn.text = ("Player 2");
                Player2Object.GetComponent<StatusEffects>().Tick();
                p2CHandler.ShowCards();
                p1CHandler.HideCards();
                //player-2-controlled
            }
        }
        else
        {
            if (Player1)
            {
                playerTurn.text = ("Player 1");
                Player1Object.GetComponent<StatusEffects>().Tick();
                p1CHandler.ShowCards();
                //player-1-controlled
            }
            if (Player2)
            {
                playerTurn.text = ("Player 2");
                Player2Object.GetComponent<StatusEffects>().Tick();
                p2CHandler.ShowCards();
                //player-2-controlled
            }
        }
        timerStarted = true;
		timeLeft = turnTime;
	}
}
// vuoronvaihto - napit - siirto, clickaus?
//pelaajanappi - 1
//pelaajanappi - 2 

