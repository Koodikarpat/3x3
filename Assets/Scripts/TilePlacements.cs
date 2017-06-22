using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TilePlacements 
{



	// Use this for initialization
	public static TileEffects GetRandom()
	{
			int random = Random.Range (0, 3); 
			int strength = (Random.Range (1, 5));

			switch (random) 
			{
			case TileEffects.POISON:
				return new Poison(strength);

			case TileEffects.HEAL:
				return new Heal (strength);

			case TileEffects.ATTACK:
				return new Attack (strength);

			default:
				return null;
			}	
	}
}
