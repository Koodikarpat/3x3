using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifestealCard : Card {

    public override bool Use()
    {
        if (!base.Use()) return false;

        getCardHandler().player1SE.AddStatusEffect(new LifestealEffect(Turns, Strength)); // Strength -1. It'll last until activated.

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }

}
