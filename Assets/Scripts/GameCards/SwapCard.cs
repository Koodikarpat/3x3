/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCard : Card
{
    public override void Use()
    {
        GameObject[] ourCards = new GameObject[getCardHandler().currentCards.Count];
        getCardHandler().currentCards.CopyTo(ourCards);

        GameObject[] opponentCards = new GameObject[getCardHandler().opponentCH.currentCards.Count];
        getCardHandler().opponentCH.currentCards.CopyTo(opponentCards);

        for (int i = 0; i < opponentCards.Length; i++) {
            opponentCards[i].transform.SetParent(getCardHandler().cardSlots[i].gameObject.transform);
            opponentCards[i].transform.localPosition = new Vector3(0, 0, 0);
            getCardHandler().currentCards[i] = opponentCards[i];
        }

        for (int i = 0; i < ourCards.Length; i++) {
            ourCards[i].transform.SetParent(getCardHandler().opponentCH.cardSlots[i].gameObject.transform);
            ourCards[i].transform.localPosition = new Vector3(0, 0, 0);
            getCardHandler().opponentCH.currentCards[i] = ourCards[i];
        }

        skipUseDraw = true;
        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }
}
*/