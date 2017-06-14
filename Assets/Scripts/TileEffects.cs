using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEffects  
{

	public Color color;
	public int strength;

	public TileEffects(int strength) 
	{
		this.strength = strength;
	}

	public virtual void Action(GameObject player, GameObject enemy)
	{

		
	}

}
