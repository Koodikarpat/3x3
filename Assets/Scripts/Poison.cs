using UnityEngine;
using System.Collections;

public class Poison : TileEffects
{
	public Poison()

	{
		color = Palette.PURPLE;
		//hakee värin paletista
	
	}
	public override void Action (GameObject player)
	{
		player.GetComponent<HealthController> ().TakeDamage (5);
		Debug.Log ("Poison");
	}

}

