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

        DrawCards();
    }

    public void DrawCards()
    {
        if (Draws > 0 && !HasCards()) {
            Draws--;
            NewCards();
        }
        else
            RemoveCards();
    }

    public void NewCards()
    {
        foreach (CardBase cardslot in cardSlots) {
            int rand = Random.Range(0, cardPrefabs.Length);
            GameObject card = Instantiate(cardPrefabs[rand], cardslot.gameObject.transform.position + cardPrefabs[rand].transform.position, cardslot.gameObject.transform.rotation);
            card.transform.SetParent(cardslot.gameObject.transform);
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
