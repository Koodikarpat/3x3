using UnityEngine;
using System.Collections;

public class Heal : TileEffects
{
	public Heal()

	{
		color = Palette.GREEN;
		//hakee värin paletista

	}
	public override void Action (GameObject player)
	{
		player.GetComponent<HealthController> ().Heal (2);
		Debug.Log ("Heal");
	}
}

