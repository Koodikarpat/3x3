using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerCard : Card
{
    public override bool Use()
    {
        if (!base.Use()) return false;

        ButtonSelection bs = (ButtonSelection)FindObjectOfType(typeof(ButtonSelection));
        bs.CreateTiles();

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }
}
