using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour 
{
	public GameObject buttonGroup;
	//Keeping track of the location of the player gameobject; "what button is the player on at the moment"
	public int currentButton;
	public GameObject turnControlObject;
	public GameObject enemy;
    public GameObject puff;
	public GameObject multiplayerController;

	ButtonSelection buttons;
    TilePlacements tilePlacements;

	void Start () 
	{
		buttons = buttonGroup.GetComponent<ButtonSelection> ();
        tilePlacements = buttonGroup.GetComponent<TilePlacements> ();

		//startin locations
		//Debug.Log (transform.name + " " + buttons.tiles[currentButton].position);
		transform.position = buttons.tiles[currentButton].gameObject.transform.position;
	}

	void Update () 
	{
	}

	public void MoveButton(int button)
	{
		// the movement itself
		// move must always be legal, if online game it must also be your turn
		if (isLegalMove (currentButton, button)) {
			if ((multiplayerController.GetComponent<Multiplayer> ().isOnline && multiplayerController.GetComponent<Multiplayer> ().isLocalTurn && turnControlObject.GetComponent<TurnControl> ().Player1) || !multiplayerController.GetComponent<Multiplayer> ().isOnline) {
				TurnControl turncontrol = turnControlObject.GetComponent<TurnControl> ();
				turncontrol.ChangeTurn ();

				buttons.tiles [currentButton].type = tilePlacements.GetRandom ();
				tilePlacements.CreateTile (buttons.tiles [currentButton], currentButton);
  
				transform.position = buttons.tiles [button].position;
				currentButton = button;

				// if online game
				if (multiplayerController.GetComponent<Multiplayer> ().isOnline) {
					// make online move
					multiplayerController.GetComponent<Multiplayer> ().MovePiece (button);
				}

				buttons.tiles [currentButton].type.Action (gameObject, enemy);
				Animator Animator = buttons.tiles [currentButton].gameObject.GetComponentInChildren<Animator> ();
				Animator.SetTrigger ("Step on");

				puff.GetComponent<ParticleSystem> ().Play ();
				puff.transform.position = buttons.tiles [button].position;
			}
		}
	}

	bool isLegalMove(int start, int end)

	// restricting player movements to just one button away and only horizontally and vertically.

	{
		if (end == enemy.GetComponent<PlayerAbilities> ().currentButton)
			return false;

		if ((start - 3 == end) || (start + 3 == end))
			return true;
		
		if (start + 1 == end) 
		{
			if ((start == 2) || (start == 5)) 
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}
			
		if (start - 1 == end) 
		{
			if ((start == 3) || (start == 6)) 
			{
				return false;
			}
			else 
			{
				return true;
			}
		}
		return false;

		//TODO STILL NEED TO RESTRICT THEM FROM OVERLAPPING!!
	}
}
