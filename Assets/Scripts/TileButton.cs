using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public int buttonnumber;
    ButtonSelection buttonSelection;

    public SpriteRenderer strengthSprite;

    void Start()
    {
       GameObject TileGroup = GameObject.Find("TileGroup");
       buttonSelection = TileGroup.GetComponent<ButtonSelection>();

        if (strengthSprite == null)
            strengthSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        buttonSelection.click(buttonnumber);
    }
}