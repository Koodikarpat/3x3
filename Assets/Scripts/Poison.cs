using UnityEngine;
using System.Collections;

public class Poison : TileEffects
{
	public Poison(int strength) : base(strength)
	{
		color = TileEffects.POISON;
		//hakee värin paletista
	
	}
	public override void Action (GameObject player, GameObject enemy)
	{
        playerSE = player.GetComponent<StatusEffects>();
        enemySE = enemy.GetComponent<StatusEffects>();

        enemySE.AddStatusEffect(new PoisonEffect(1, strength));
        
        base.Action(player, enemy);
    }

}