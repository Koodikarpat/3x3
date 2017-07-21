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

    public bool minePlacement = false;

    void Awake()
    {
        CreateTiles();
    }

    public void CreateTiles()
    {
        if (!multiplayerController.GetComponent<Multiplayer>().isOnline) {
            for (int i = 0; i < tiles.Length; i++) {
                tiles[i].position = tiles[i].gameObject.transform.position;

                TilePlacements tilePlacements = GetComponent<TilePlacements>();
                tiles[i].type = tilePlacements.GetRandom();

                //nappien randomoitu asettelu
                //tiles[i].gameObject.GetComponentInChildren<Text> ().text = ""+tiles[i].type.strength;

                tilePlacements.CreateTile(tiles[i], i);
            }
        }
    }

	public void click(int button)
	{
        GameObject currentPlayer;
        TurnControl turnControl = TurnController.GetComponent<TurnControl>();

        if (turnControl.Player1) {
            currentPlayer = player1;
        }
        else if (turnControl.Player2) {
            if (!multiplayerController.GetComponent<Multiplayer>().isOnline) { // local game
                currentPlayer = player2;
            }
            else { // online game
                Debug.Log("Local player tried to move remote players piece");
                return;
            }
        }
        else {
            Debug.Log("Virhe - Molempien Vuoro");
            return;
        }

        if (!minePlacement) {
            //Moving player to a selected location.
            PlayerAbilities pa = currentPlayer.GetComponent<PlayerAbilities>();
            pa.MoveButton(button);
        }
        else if (minePlacement) {
            tiles[button].type.mine = true;
            minePlacement = false;
        }
	}
}