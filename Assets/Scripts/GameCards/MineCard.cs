using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCard : Card {

    public override void Use()
    {
        ButtonSelection bs = (ButtonSelection)FindObjectOfType(typeof(ButtonSelection));
        bs.minePlacement = true;

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }

}
