using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player1;
	public GameObject player2;
	public GameObject TurnController;

	public Tile[] tiles = new Tile[9];

    public GameObject heal;
    public GameObject attack;
    public GameObject poison;

	void Start () 
	{

        for (int i = 0; i < 9; i++)
        {
            tiles[i].position = tiles[i].gameObject.transform.position;

            tiles[i].type = TilePlacements.GetRandom();

            //nappien randomoitu asettelu
            //tiles[i].gameObject.GetComponentInChildren<Text> ().text = ""+tiles[i].type.strength;

            Debug.Log(tiles[i].type);
            Debug.Log(tiles[i].type.color);
            Debug.Log(TileEffects.HEAL);

            GameObject newtile;

            if (tiles[i].type.color == TileEffects.HEAL)
            {
                newtile = Instantiate(heal, tiles[i].gameObject.transform);
               
            }
            else if (tiles[i].type.color == TileEffects.ATTACK)
            {
                newtile = Instantiate(attack, tiles[i].gameObject.transform);
            }
            else if (tiles[i].type.color == TileEffects.POISON)
            {
                newtile = Instantiate(poison, tiles[i].gameObject.transform);
            }
            else
            {
                Debug.Log("Error wrong tile type");
                return;
            }
            TileButton tilebutton = newtile.GetComponent<TileButton>();
            tilebutton.buttonnumber = i;


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
        Debug.Log(button);
	}

}