using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour 
{

	//Array for all the mapbuttons, the movement of the players will be restricted to only these buttons.
	public GameObject[] buttonArray = new GameObject[9];
	public GameObject buttonGroup;
	private Vector2[] buttonPosition = new Vector2[9];
	int currentButton;

	void Start () 
	{
		//Keeping track of the location of the player gameobject; "what button is the player on at the moment"
		currentButton = 4;

		for (int i = 0; i < 9; i++)
			buttonPosition [i] = buttonArray [i].transform.position;
		
	}

	void Update () 
	{
		//Getting the mousedown click on buttons to get player moving on the selected location.
		ButtonSelection buttonSelection = buttonGroup.GetComponent<ButtonSelection> ();
		if (Input.GetMouseButtonUp (0))
			Debug.Log ("release");

	}

	public void MoveButton(int button)
	{
		if (isLegalMove (currentButton, button)) 
		{
			transform.position = buttonPosition [button];
			currentButton = button;
		}
	}

	bool isLegalMove(int start, int end)

	//Restricking player movents to just one button away and only horizontally and vertically.

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
			
	}


}
