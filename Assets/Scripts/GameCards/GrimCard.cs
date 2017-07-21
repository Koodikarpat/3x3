using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrimCard : Card {

    public SpriteRenderer damagePercent;
    public List<Sprite> holdtimeSprites = new List<Sprite>();

    public int difficultyModifier = 2; // Divides the luck, 1 - 3 range recommended. 1 being easiest, 3 being hardest and 2 being the middleground.
    private float onDownTime, onUpTime;
    private bool doPercentageSprites = false;

    void Start()
    {
        if (damagePercent == null) { // Create one programmatically if not set in editor
            GameObject go = new GameObject();
            go.name = "DamagePercent";
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            damagePercent = sr;
        }
    }

    public override void Use()
    {
        float timePassed = (onUpTime - onDownTime);
        int luck = Random.Range(4, 10 + 1); // Min luck, max luck (+ 1 because Random.Range is like that).
        int probabilityThreshold = Random.Range(1, (11 * luck / difficultyModifier)); // Single random variable was not random enough, added "luck".
        float probability = Mathf.Floor(timePassed * 10);
        probability = Mathf.Floor(probability / 10);
        float selfDamageProbability = (probability * 10);

        if (selfDamageProbability <= 0)
            selfDamageProbability = 10;

        // Debug.Log("Threshold " + probabilityThreshold + " SelfDamageProbability " + selfDamageProbability);

        if (selfDamageProbability > probabilityThreshold)
            getCardHandler().player1HC.TakeDamage(Strength / 2);
        else if (selfDamageProbability < probabilityThreshold) {
            getCardHandler().player2HC.TakeDamage((int)(Strength * (selfDamageProbability / 100)));
        }

        damagePercent.sprite = null; // Clear the sprite for damagePercent

        base.Use();
    }

    void OnMouseDown()
    {
        onDownTime = Time.time;
        doPercentageSprites = true;
        StartCoroutine(percentageSprite());
    }

    void OnMouseUp()
    {
        doPercentageSprites = false;
        onUpTime = Time.time;
        Use();
    }

    private IEnumerator percentageSprite()
    {
        int spriteToUse = 0;
        int lastProbability = 0;
        while (doPercentageSprites) {
            float currentTime = Time.time;
            float timePassed = (currentTime - onDownTime);
            float probab = Mathf.Floor(timePassed * 10);
            probab = Mathf.Floor(probab / 10);
            int probability = (int)(probab * 10);

            // Debug.Log("Probability " + probability + " Diff " + (probability - lastProbability));

            if (probability - lastProbability >= 10) {
                lastProbability = probability;
                if (spriteToUse < holdtimeSprites.Count)
                    spriteToUse++;

                if (holdtimeSprites.ElementAtOrDefault(spriteToUse) != null)
                    damagePercent.sprite = holdtimeSprites[spriteToUse];

                // Debug.Log("New sprite set");
            }

            yield return null;
        }
        yield return null;
    }
}
