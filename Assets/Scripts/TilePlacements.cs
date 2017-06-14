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
			case 2:
				return new Poison(strength);

			case 1:
				return new Heal (strength);

			case 0:
				return new Attack (strength);

			default:
				return null;
			}	
	}
}
