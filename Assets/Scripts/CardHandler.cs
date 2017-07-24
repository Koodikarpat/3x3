using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour {

    public int Draws = 3;

    public GameObject[] cardPrefabs;
    public CardBase[] cardSlots = new CardBase[3];
    public List<GameObject> currentCards = new List<GameObject>();

    public HealthController player1HC, player2HC;
    public StatusEffects player1SE, player2SE;
    public TurnControl turnControl;

    void Start()
    {
        if (turnControl == null)
            turnControl = (TurnControl)FindObjectOfType(typeof(TurnControl));

        // DrawCards(/*new int[] { 0, 0, 0 }*/);
    }

    /// <summary>
    /// Draw new cards
    /// </summary>
    /// <param name="types">Card types to spawn (index of cardPrefabs array)</param>
    public void DrawCards(int[] types = null)
    {
        if (Draws > 0 && !HasCards()) {
            Draws--;
            NewCards(types);
        }
        else
            RemoveCards();
    }

    public void NewCards(int[] types)
    {
        List<int> tps;
        if (types == null) {
            tps = new List<int>();
            for (int i = 0; i < cardPrefabs.Length; i++)
                tps.Add(Random.Range(0, cardPrefabs.Length));
        } else
            tps = new List<int>(types);

        for (int c = 0; c < cardSlots.Length; c++) {
            GameObject card = Instantiate(cardPrefabs[tps[c]], cardSlots[c].gameObject.transform.position + cardPrefabs[tps[c]].transform.position, cardSlots[c].gameObject.transform.rotation);
            card.transform.SetParent(cardSlots[c].gameObject.transform);
            currentCards.Add(card);
        }
    }

    public void RemoveCards()
    {
        foreach (GameObject card in currentCards) {
            Destroy(card.gameObject);
        }
        currentCards.Clear();
    }

    public bool HasCards()
    {
        if (currentCards.Count > 0)
            return true;
        else
            return false;
    }

    public void ShowCards()
    {
        foreach (CardBase cardslot in cardSlots)
            cardslot.gameObject.SetActive(true);
    }

    public void HideCards()
    {
        foreach (CardBase cardslot in cardSlots)
            cardslot.gameObject.SetActive(false);
    }

}
