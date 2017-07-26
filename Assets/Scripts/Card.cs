using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {

    [Tooltip("The amount of turns the effect lasts for.")]
    public int Turns = 2;
    [Tooltip("The strength of the effect (ex. damage amount)")]
    public int Strength = 1;
    [Tooltip("Does using the card skip a turn?")]
    public bool SkipTurn = false;

    public AudioClip useSound;
    public AudioSource audioSource;

    public bool skipUseDraw = false;

    private CardHandler cardHandler;

    private bool moving = false;

    public virtual void Use()
    {
        if (audioSource || useSound != null)
            audioSource.PlayOneShot(useSound);

        StartCoroutine(cardMovement(1));
        StartCoroutine(waitFor());
    }

    public CardHandler getCardHandler()
    {
        return transform.GetComponentInParent<CardHandler>();
    }

    private void DoTurn()
    {
        if (SkipTurn)
            getCardHandler().turnControl.ChangeTurn();
    }

    private IEnumerator cardMovement(float delta)
    {
        while (this.transform.position != new Vector3(0, 0, 0)) {
            moving = true;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(0, 0, 0), 1);
            yield return null;
        }
        moving = false;
        yield return null;
    }

    private IEnumerator waitFor()
    {
        while (moving)
            yield return new WaitForSeconds(0.1f);

        DoTurn();
        if (!skipUseDraw)
            getCardHandler().DrawCards();
        else
            skipUseDraw = false;

        yield return null;
    }
}
