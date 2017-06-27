using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacements : MonoBehaviour 
{
    public GameObject heal;
    public GameObject attack;
    public GameObject poison;

    // Use this for initialization
    public TileEffects GetRandom()
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
    //tekee uuden tilen
    public void CreateTile(Tile tile, int i)
    {
        GameObject newtile;

        if (tile.gameObject.transform.GetChildCount () != 0)
        Destroy (tile.gameObject.transform.GetChild(0).gameObject);

        if (tile.type.color == TileEffects.HEAL)
        {
            newtile = GameObject.Instantiate(heal, tile.gameObject.transform);

        }
        else if (tile.type.color == TileEffects.ATTACK)
        {
            newtile = GameObject.Instantiate(attack, tile.gameObject.transform);
        }
        else if (tile.type.color == TileEffects.POISON)
        {
            newtile = GameObject.Instantiate(poison, tile.gameObject.transform);
        }
        else
        {
            Debug.Log("Error wrong tile type");
            return;
        }
        TileButton tilebutton = newtile.GetComponent<TileButton>();
        tilebutton.buttonnumber = i;
    }
     
}
