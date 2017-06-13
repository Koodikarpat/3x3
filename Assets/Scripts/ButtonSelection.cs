using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject TurnController;


	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	public void click(int button)
	{
		//Moving player to a selected location.
		GameObject currentPlayer;
		TurnControl turnControl = TurnController.GetComponent<TurnControl> ();
		if (turnControl.Player1) {
			currentPlayer = player1;
			//turnControl.ChangeTurn ();
		} else if (turnControl.Player2) {
			currentPlayer = player2;
			//turnControl.ChangeTurn ();
		} else {
			Debug.Log ("Virhe - Molempien Vuoro");
			return;
		}
		PlayerAbilities pa = currentPlayer.GetComponent<PlayerAbilities> ();
		pa.MoveButton (button);

	}

}