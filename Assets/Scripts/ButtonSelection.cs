using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject TurnController;
	public GameObject multiplayerController;

	public Tile[] tiles = new Tile[9];

    //void Awake()
    //{

    //    for (int i = 0; i < 9; i++)
    //    {
    //        tiles[i].position = tiles[i].gameObject.transform.position;

    //        TilePlacements tilePlacements = GetComponent<TilePlacements>();
    //        tiles[i].type = tilePlacements.GetRandom();

    //        //nappien randomoitu asettelu
    //        //tiles[i].gameObject.GetComponentInChildren<Text> ().text = ""+tiles[i].type.strength;

    //        tilePlacements.CreateTile(tiles[i], i);
    //    }
    //}

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
		} else if (turnControl.Player2) {
			if (!multiplayerController.GetComponent<Multiplayer>().isOnline) { // local game
				currentPlayer = player2;
			} else { // online game
				Debug.Log("Local player tried to move remote players piece");
				return;
			}
		} else {
			Debug.Log ("Virhe - Molempien Vuoro");
			return;
		}
		PlayerAbilities pa = currentPlayer.GetComponent<PlayerAbilities>();
		pa.MoveButton (button);
	}
}