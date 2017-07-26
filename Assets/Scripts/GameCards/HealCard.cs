using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card {

    public override bool Use()
    {
        if (!base.Use()) return false;

        getCardHandler().player1HC.Heal(Mathf.CeilToInt((float)(getCardHandler().player1HC.startingHealth + getCardHandler().player1HC.currentHealth) / 3));

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }

}
