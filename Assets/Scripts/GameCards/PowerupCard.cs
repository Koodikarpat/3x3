using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCard : Card
{
    public override void Use()
    {
        getCardHandler().player1SE.AddStatusEffect(new PowerupEffect(Turns, Strength));

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }

}
