using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCard : Card
{
    public override bool Use()
    {
        if (!base.Use()) return false;

        for (var i = 0; i < Strength; i++) {
            if (getCardHandler().opponentCH.currentCards.Count > 0) {
                int random = Random.Range(0, getCardHandler().opponentCH.currentCards.Count);
                while (getCardHandler().opponentCH.currentCards[random] == null) {
                    if (getCardHandler().opponentCH.currentCards.Count > 0)
                        return true;

                    random = Random.Range(0, getCardHandler().opponentCH.currentCards.Count);
                }
                Destroy(getCardHandler().opponentCH.currentCards[random].gameObject);
            } else {
                Debug.Log("No cards");
            }
        }

        return true;
    }

    void OnMouseUp()
    {
        Use();
    }
}
