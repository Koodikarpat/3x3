using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCard : Card {

    public override bool Use()
    {
        if (!base.Use()) return false;

        ButtonSelection bs = (ButtonSelection)FindObjectOfType(typeof(ButtonSelection));

        bs.Mineplacement();

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }

}
