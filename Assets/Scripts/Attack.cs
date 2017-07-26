using UnityEngine;
using System.Collections;

public class Attack : TileEffects
{
	public Attack(int strength) : base(strength)
	{
		color = TileEffects.ATTACK;
		//hakee värin paletista
	
	}
	public override void Action (GameObject player, GameObject enemy)
	{
        playerSE = player.GetComponent<StatusEffects>();
        enemySE = enemy.GetComponent<StatusEffects>();

        if (!enemySE.GetEffect(typeof(ShieldEffect)).Effective()) {

            if (playerSE.GetEffect(typeof(PowerupEffect)).Effective()) {
                enemy.GetComponent<HealthController>().TakeDamage(strength * playerSE.GetEffect(typeof(PowerupEffect)).strength);
                playerSE.GetEffect(typeof(PowerupEffect)).strength = 0;
            }

            if (playerSE.GetEffect(typeof(LifestealEffect)).Effective()) {
                player.GetComponent<HealthController>().Heal(strength);
                enemy.GetComponent<HealthController>().TakeDamage(strength);
                playerSE.GetEffect(typeof(LifestealEffect)).turns = 0;
            }
            else
                enemy.GetComponent<HealthController>().TakeDamage(strength); // No shield, no special attack, just damage enemy.
        }
        else {
            enemySE.GetEffect(typeof(ShieldEffect)).strength -= strength; // Reduce enemy shield strength, eventually breaking it.
            enemySE.Refresh();
        }
        
        base.Action(player, enemy);
	}
}

