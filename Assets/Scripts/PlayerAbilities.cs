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

	ButtonSelection buttons;



	void Start () 
	{
		buttons = buttonGroup.GetComponent<ButtonSelection> ();
	}

	void Update () 
	{
		
	}

	public void MoveButton(int button)
	{

		//The movement itself.
		if (isLegalMove (currentButton, button)) 
		{

			TurnControl turncontrol = turnControlObject.GetComponent<TurnControl> ();
			turncontrol.ChangeTurn ();

			//Changing the buttons in the beginnning and when the player leaves a tile.
			buttons.tiles[currentButton].type = TilePlacements.GetRandom ();
			buttons.tiles[currentButton].gameObject.GetComponentInChildren<Text> ().text = ""+buttons.tiles[currentButton].type.strength;
			ColorBlock buttonColors = buttons.tiles[currentButton].gameObject.GetComponent<Button> ().colors;
			buttonColors.normalColor = buttons.tiles[currentButton].type.color;
			buttons.tiles[currentButton].gameObject.GetComponent<Button> ().colors = buttonColors;
	
			transform.position = buttons.tiles[button].position;
			currentButton = button;

			buttons.tiles[currentButton].type.Action (gameObject, enemy);

		}


	}

	bool isLegalMove(int start, int end)

	//Restricking player movements to just one button away and only horizontally and vertically.

	{
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

		//STILL NEED TO RESTRICT THEM FROM OVERLAPPING!!
	}
}
