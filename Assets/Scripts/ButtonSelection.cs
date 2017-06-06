using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour 
{
	public GameObject player;

	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	public void click(int button)
	{
		//Getting the mousedown click on buttons to get player moving on the selected location.
		PlayerAbilities pa = player.GetComponent<PlayerAbilities> ();
		pa.MoveButton (button);
		//Debug.Log ("Buttons");
	}

}
