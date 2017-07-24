using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCard : Card
{
    public override void Use()
    {
        int random = Random.Range(0, getCardHandler().opponentCH.currentCards.Count);
        if (random < getCardHandler().opponentCH.currentCards.Count) {
            if (getCardHandler().opponentCH.currentCards[random] == null) {
                if (random < getCardHandler().opponentCH.currentCards.Count)
                    random++;
                else
                    random--;
            }
            Destroy(getCardHandler().opponentCH.currentCards[random].gameObject);
        }
        else
            Debug.Log("No such index");

        base.Use();
    }

    void OnMouseUp()
    {
        Use();
    }
}
