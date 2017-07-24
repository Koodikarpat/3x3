using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedDiceCard : Card
{
    public override void Use()
    {
        int randomEffect = Random.Range(0, getCardHandler().player1SE.effects.Count);

        Effect newEffect = getCardHandler().player1SE.effects[randomEffect];
        newEffect.strength = Strength;
        newEffect.turns = Turns;
        getCardHandler().player1SE.AddStatusEffect(newEffect);
        Debug.Log("Added effect: " + newEffect.ToString());


        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }
}
