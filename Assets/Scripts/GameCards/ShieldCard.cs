using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCard : Card {

    public override void Use()
    {
        getCardHandler().player1SE.AddStatusEffect(new ShieldEffect(Turns, Strength));

        base.Use();
    }


    void OnMouseUp()
    {
        Use();
    }

}
