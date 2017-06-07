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
		//Moving player to a selected location
		PlayerAbilities pa = player.GetComponent<PlayerAbilities> ();
		pa.MoveButton (button);
	}

}
