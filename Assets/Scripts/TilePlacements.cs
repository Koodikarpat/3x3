using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacements : MonoBehaviour 
{
    public GameObject heal;
    public GameObject attack;
    public GameObject poison;

    // Use this for initialization
    public TileEffects GetEffect(int id, int strength)
	{
			switch (id) 
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
        GameObject tilePrefab;

        if (tile.gameObject.transform.childCount != 0)
            Destroy (tile.gameObject.transform.GetChild(0).gameObject);

        if (tile.type.color == TileEffects.HEAL)
        {
            tilePrefab = heal;
        }
        else if (tile.type.color == TileEffects.ATTACK)
        {
            tilePrefab = attack;
        }
        else if (tile.type.color == TileEffects.POISON)
        {
            tilePrefab = poison;
        }
        else
        {
            Debug.Log("Error wrong tile type");
            return;
        }

        GameObject newtile = GameObject.Instantiate(tilePrefab, tile.gameObject.transform, false);
        TileButton tilebutton = newtile.GetComponent<TileButton>();
        tilebutton.buttonnumber = i;
    }
     
}
