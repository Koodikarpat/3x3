using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerCard : Card
{
    public override void Use()
    {
        ButtonSelection bs = (ButtonSelection)FindObjectOfType(typeof(ButtonSelection));
        bs.CreateTiles();

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }
}
