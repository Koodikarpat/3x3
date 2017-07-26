using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCard : Card {

    public override bool Use()
    {
        if (!base.Use()) return false;

        getCardHandler().player1SE.AddStatusEffect(new ShieldEffect(Turns, Strength));

        return true;
    }


    void OnMouseUp()
    {
        Use();
    }

}
