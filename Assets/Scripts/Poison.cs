using UnityEngine;
using System.Collections;

public class Poison : TileEffects
{
	public Poison(int strength) : base(strength)
	{
		color = Palette.PURPLE;
		//hakee värin paletista
	
	}
	public override void Action (GameObject player, GameObject enemy)
	{
		if (strength > enemy.GetComponent<StatusEffects> ().turnsLeft)
				enemy.GetComponent<StatusEffects> ().turnsLeft = strength;
		//Debug.Log ("Poison");
	}

}

