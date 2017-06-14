using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile
{
	public GameObject gameObject;
	public TileEffects type;
	public int strength;
	public Vector2 position;


	public void Randomizer ()
	{
		strength = (Random.Range (1, 4));
	}
}

