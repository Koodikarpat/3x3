using UnityEngine;
using System.Collections;

public class Heal : TileEffects
{
	public Heal(int strength) : base(strength)
	{
		color = Palette.GREEN;
		//hakee värin paletista

	}
	public override void Action (GameObject player, GameObject enemy)
	{
		player.GetComponent<HealthController> ().Heal (strength);
		//Debug.Log ("Heal");
	}
}

