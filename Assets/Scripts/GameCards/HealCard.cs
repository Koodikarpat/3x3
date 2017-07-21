using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card {

    public override void Use()
    {
        getCardHandler().player1HC.Heal(getCardHandler().player1HC.startingHealth / getCardHandler().player1HC.currentHealth);
        SkipTurn = true;

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }

}
