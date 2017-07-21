using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerCard : Card
{
    public override void Use()
    {
        ButtonSelection bs = (ButtonSelection)FindObjectOfType(typeof(ButtonSelection));
        bs.CreateTiles();

        SkipTurn = true;

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }
}
