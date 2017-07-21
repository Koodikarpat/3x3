using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCard : Card {

    public override void Use()
    {
        getCardHandler().player1SE.AddStatusEffect(new ShieldEffect(Turns, Strength)); // 2 turns since ChangeTurn gets called after effect is created, loses a turn.

        base.Use();
    }


    void OnMouseUp()
    {
        Use();
    }

}
