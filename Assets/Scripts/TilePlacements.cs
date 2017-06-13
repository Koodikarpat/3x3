using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TilePlacements 
{



	// Use this for initialization
	public static TileEffects GetRandom()
	{
			int random = Random.Range (0, 3); 

			switch (random) 
			{
			case 2:
				return new Poison();

			case 1:
				return new Heal ();

			case 0:
				return new Attack ();

			default:
				return null;
			}	
	}
}
