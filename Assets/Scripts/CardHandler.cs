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

    public Multiplayer multiplayerC;

    private List<bool> drawnPrefabs = new List<bool>();

    private int ourPlayer;

    void Awake()
    {
        if (turnControl == null)
            turnControl = FindObjectOfType<TurnControl>();

        if (multiplayerC == null)
            multiplayerC = FindObjectOfType<Multiplayer>();

        if (opponentCH == null) {
            CardHandler[] handlers = FindObjectsOfType<CardHandler>();
            foreach (CardHandler ch in handlers) {
                if (ch != this)
                    opponentCH = ch;
            }
        }

        for (int i = 0; i < cardPrefabs.Length; i++)
            drawnPrefabs.Add(false);

        if (turnControl.p1CHandler == this)
            ourPlayer = 1;
        else if (turnControl.p2CHandler == this)
            ourPlayer = 2;
        else
            Debug.Log("Unkown player card handler");

        // DrawCards(/*new int[] { 0, 0, 0 }*/);
    }

    /// <summary>
    /// Draw cards
    /// </summary>
    /// <param name="types">Card types to spawn (indexes of cardPrefabs array)</param>
    public void DrawCards(int[] types = null)
    {
        if (Draws > 0 && !HasCards())
        {
            Debug.Log("ei ole kortteja");
            Draws--;
            NewCards(types);
        }
        else
        {
            Debug.Log("poistetaan kortit");
            RemoveCards();
        }
    }

    /// <summary>
    /// Instantiate new cards
    /// </summary>
    /// <param name="types"></param>
    public void NewCards(int[] types)
    {
        List<int> tps;
        int randomCard = 0;
        if (types == null) {
            Debug.Log("ei tyyppejä");
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
        {
            Debug.Log("on tyypit");
            tps = new List<int>(types);
        }

        Debug.Log("cardslotsLength " + cardSlots.Length);
        for (int c = 0; c < cardSlots.Length; c++) {
            GameObject card = Instantiate(cardPrefabs[tps[c]], cardSlots[c].gameObject.transform.position + cardPrefabs[tps[c]].transform.position, cardSlots[c].gameObject.transform.rotation);
            card.transform.SetParent(cardSlots[c].gameObject.transform);
            card.GetComponent<Card>().MEISGOOD = tps[c];
            Debug.Log("lisättiin kortti, prefab " + tps[c]);
            currentCards.Add(card);
        }
    }

    /// <summary>
    /// Destroy current cards
    /// </summary>
    public void RemoveCards()
    {
        foreach (GameObject card in currentCards) {
            Destroy(card.gameObject);
        }
        currentCards.Clear();
    }

    /// <summary>
    /// Returns true if there are cards
    /// </summary>
    /// <returns></returns>
    public bool HasCards()
    {
        if (!currentCards.Any())
            return false;
        else
            return true;
    }

    /// <summary>
    /// Makes cards opaque
    /// </summary>
    public void ShowCards()
    {
        foreach (var card in currentCards) {
            if (card != null) {
                Color color = card.GetComponent<SpriteRenderer>().color;
                color.a = 1.0f;
                card.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    /// <summary>
    /// Makes cards slightly transparent
    /// </summary>
    public void HideCards()
    {
        foreach (var card in currentCards) {
            if (card != null) {
                Color color = card.GetComponent<SpriteRenderer>().color;
                color.a = 0.5f;
                card.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    public bool OurTurn()
    {
        if (ourPlayer == 1 && turnControl.Player1 || ourPlayer == 2 && turnControl.Player2)
            return true;
        else
            return false; // Not our turn
    }

    /// <summary>
    /// Function called by Card's Use(). Passes their type.
    /// </summary>
    /// <param name="type"></param>
    public void CardUsed(Card type)
    {
        Debug.Log("Card used: " + type.ToString() + "  by player " + ourPlayer.ToString());

        int index = type.MEISGOOD;

        Debug.Log(index + " " + type.Strength);
       
        if(multiplayerC.isOnline)
            multiplayerC.UseCard(index ,type.Strength );
    }
}
