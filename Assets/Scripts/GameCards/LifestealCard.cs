using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifestealCard : Card {

    public override void Use()
    {
        getCardHandler().player1SE.AddStatusEffect(new LifestealEffect(Turns, Strength)); // Strength -1. It'll last until activated.

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }

}
