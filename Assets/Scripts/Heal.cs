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

        if (playerSE.GetEffect(typeof(PowerupEffect)).Effective()) {
            player.GetComponent<HealthController>().Heal(strength * playerSE.GetEffect(typeof(PowerupEffect)).strength);
            playerSE.GetEffect(typeof(PowerupEffect)).strength = 0;
        }
        else
            player.GetComponent<HealthController>().Heal(strength);

        base.Action(player, enemy);
	}
}

