using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card {

    public override bool Use()
    {
        if (!base.Use()) return false;

        getCardHandler().player1HC.Heal(getCardHandler().player1HC.startingHealth / getCardHandler().player1HC.currentHealth);

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }

}
