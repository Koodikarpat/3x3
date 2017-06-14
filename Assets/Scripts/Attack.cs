using UnityEngine;
using System.Collections;

public class Attack : TileEffects
{
	public Attack()

	{
		color = Palette.RED;
		//hakee värin paletista
	
	}
	public override void Action (GameObject player, GameObject enemy)
	{
		enemy.GetComponent<HealthController> ().TakeDamage (3);
		Debug.Log ("TakeDamage");
	}
}

