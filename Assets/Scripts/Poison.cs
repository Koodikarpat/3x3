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

        if (strength > enemySE.GetEffect(typeof(PoisonEffect)).turns)
            enemySE.AddStatusEffect(new PoisonEffect(1, 1));
        //Debug.Log ("Poison");

        base.Action(player, enemy);
    }

}