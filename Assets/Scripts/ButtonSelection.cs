using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject TurnController;


	public GameObject[] buttonArray = new GameObject[9];
	//Array for all the mapbuttons, the movement of the players will be restricted to only these buttons.
	public TileEffects[] buttonTypes = new TileEffects[9];
	public Vector2[] buttonPosition = new Vector2[9];

	void Start () 
	{

		for (int i = 0; i < 9; i++) 
		{
			buttonPosition [i] = buttonArray [i].transform.position;

			buttonTypes [i] = TilePlacements.GetRandom ();

			Debug.Log (i + " " + buttonTypes [i]);

			//nappien randomoitu asettelu
			ColorBlock buttonColors = buttonArray [i].GetComponent<Button> ().colors;
			buttonColors.normalColor = buttonTypes [i].color;
			buttonArray [i].GetComponent<Button> ().colors = buttonColors;

		}

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