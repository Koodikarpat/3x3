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

	public TileEffects(int strength) 
	{
		this.strength = strength;
	}

	public virtual void Action(GameObject player, GameObject enemy)
	{

		
	}

}
