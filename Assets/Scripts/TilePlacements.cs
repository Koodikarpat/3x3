using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TilePlacements {



	// Use this for initialization
	public static TileEffects[] GetRandoms()
	{
		TileEffects[] tiles = new TileEffects[9];

		for (int i = 0; i < 9; i++)
		{
			int random = Random.Range (0, 3); 
			//TileEffects [i] = 
			

			


				switch (random) 
			{
			case 2:
				tiles[i] = new Poison();
				break;

			case 1:
				tiles[i] = new Heal ();
				break;

			case 0:
				tiles[i] = new Attack ();
				break;

			}
		}
		return tiles;	
	}
}
