using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CardHandler : MonoBehaviour {

    public int Draws = 3;

    public GameObject[] cardPrefabs;
    public CardBase[] cardSlots = new CardBase[3];
    public List<GameObject> currentCards = new List<GameObject>();

    public HealthController player1HC, player2HC;
    public StatusEffects player1SE, player2SE;
    public CardHandler opponentCH;
    public TurnControl turnControl;

    private List<bool> drawnPrefabs = new List<bool>();

    void Awake()
    {
        if (turnControl == null)
            turnControl = (TurnControl)FindObjectOfType(typeof(TurnControl));

        if (opponentCH == null) {
            CardHandler[] handlers = FindObjectsOfType(typeof(CardHandler)) as CardHandler[];
            foreach (CardHandler ch in handlers) {
                if (ch != this)
                    opponentCH = ch;
            }
        }

        for (int i = 0; i < cardPrefabs.Length; i++)
            drawnPrefabs.Add(false);

        // DrawCards(/*new int[] { 0, 0, 0 }*/);
    }

    /// <summary>
    /// Draw new cards
    /// </summary>
    /// <param name="types">Card types to spawn (index of cardPrefabs array)</param>
    public void DrawCards(int[] types = null)
    {
<<<<<<< HEAD
        if (multiplayerC.isOnline)
            for (int i = 0; i < types.Length; i++)
            {
                Debug.Log(types[i]);
            }
=======
>>>>>>> d396a6e30d94ac2ad347d8f2698f4e28378404e6
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
        int randomCard = 0;
        if (types == null) {
            tps = new List<int>();
            for (int i = 0; i < cardSlots.Length; i++) {
                randomCard = Random.Range(0, cardPrefabs.Length);
                if (!drawnPrefabs[randomCard]) {
                    drawnPrefabs[randomCard] = true;
                    tps.Add(randomCard);
                }
                else {
                    if (drawnPrefabs.Any(a => a == false)) {
                        int firstToAdd = drawnPrefabs.IndexOf(drawnPrefabs.First(f => f == false));
                        drawnPrefabs[firstToAdd] = true;
                        tps.Add(firstToAdd);
                    }
                    else {
                        Debug.Log("All cards have been drawn atleast once.");
                    }
                }
            }
        }
        else
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
