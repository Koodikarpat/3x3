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
		enemy.GetComponent<HealthController> ().TakeDamage (5);
		//Debug.Log ("Poison");
	}

}

