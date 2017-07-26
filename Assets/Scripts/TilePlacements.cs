using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacements : MonoBehaviour 
{
    System.Random random = new System.Random();

    public GameObject heal;
    public GameObject attack;
    public GameObject poison;

    public int healTileLimit = 3, attackTileLimit = 3, poisonTileLimit = 3;
    private int healTL, attackTL, poisonTL;

    public Sprite[] strengthSprites;

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

    public TileEffects GetRandom()
    {
        int randInt = random.Next(3);

        if (healTL <= 0 && attackTL <= 0 && poisonTL <= 0) {
            healTL = healTileLimit; attackTL = attackTileLimit; poisonTL = poisonTileLimit;
        }

        while (randInt == 0 && attackTL == 0 || randInt == 1 && healTL == 0 || randInt == 2 && poisonTL == 0)
            randInt = random.Next(3);

        return GetEffect(randInt, 1);
    }

    //tekee uuden tilen
    public bool CreateTile(Tile tile, int i, int strength = 1)
    {
        GameObject tilePrefab;

        if (tile.gameObject.transform.childCount != 0)
            Destroy (tile.gameObject.transform.GetChild(0).gameObject);

        if (tile.type.color == TileEffects.HEAL)
        {
            tilePrefab = heal;
            healTL--;
        }
        else if (tile.type.color == TileEffects.ATTACK)
        {
            tilePrefab = attack;
            attackTL--;
        }
        else if (tile.type.color == TileEffects.POISON)
        {
            tilePrefab = poison;
            poisonTL--;
        }
        else
        {
            Debug.Log("Error wrong tile type");
            return false;
        }

        GameObject newtile = GameObject.Instantiate(tilePrefab, tile.gameObject.transform, false);
        TileButton tilebutton = newtile.GetComponent<TileButton>();
        tilebutton.buttonnumber = i;
        tile.type.strength = strength;
        tilebutton.strengthSprite.sprite = strengthSprites[strength-1];

        return true;
    }
}
