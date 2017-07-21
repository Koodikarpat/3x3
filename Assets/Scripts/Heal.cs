using UnityEngine;
using System.Collections;

public class Heal : TileEffects
{
	public Heal(int strength) : base(strength)
	{
		color = TileEffects.HEAL;
		//hakee värin paletista

	}
	public override void Action (GameObject player, GameObject enemy)
	{
        playerSE = player.GetComponent<StatusEffects>();
        enemySE = enemy.GetComponent<StatusEffects>();

        player.GetComponent<HealthController> ().Heal (strength);
        //Debug.Log ("Heal");

        base.Action(player, enemy);
	}
}

