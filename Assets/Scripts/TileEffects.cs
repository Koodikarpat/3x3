using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEffects
{
    public const int ATTACK = 0;
    public const int HEAL = 1;
    public const int POISON = 2;

    public int color;
    public int strength;

    public bool mine;

    public StatusEffects playerSE;
    public StatusEffects enemySE;

    public TileEffects(int strength) 
	{
		this.strength = strength;
        this.mine = false;
    }

    public virtual void Action(GameObject player, GameObject enemy)
    {
        if (mine && !playerSE.GetEffect(typeof(ShieldEffect)).Effective()) {
            player.GetComponent<HealthController>().TakeDamage(3);
            this.mine = false;
        }
        else {
            enemySE.GetEffect(typeof(ShieldEffect)).strength--;
            this.mine = false;
        }
    }
}
