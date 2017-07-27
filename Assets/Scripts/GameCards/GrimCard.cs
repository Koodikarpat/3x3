using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GrimCard : Card
{
    public Slider holdingSlider;
    public Text dmgText;
    public int difficultyModifier = 2; // Divides the luck, 1 - 3 range recommended. 1 being easiest, 3 being hardest and 2 being the middleground.
    public GameObject effectPrefab;

    private float onDownTime, onUpTime;
    private bool doPercentageSprites = false;

    void Start()
    {
        if (holdingSlider == null) {
            holdingSlider = GetComponentInChildren<Slider>();
            holdingSlider.value = 0;
        }

        if (dmgText == null) {
            dmgText = GetComponentInChildren<Text>();
            dmgText.text = "";
        }
    }

    public override bool Use()
    {
        float timePassed = (onUpTime - onDownTime);
        int luck = Random.Range(4, 10 + 1); // Min luck, max luck (+ 1 because Random.Range is like that).
        int probabilityThreshold = Random.Range(1, (11 * luck / difficultyModifier)); // Single random variable was not random enough, added "luck".
        float probability = Mathf.Floor(timePassed * 10);
        probability = Mathf.Floor(probability / 10);
        float selfDamageProbability = (probability * 10);

        if (selfDamageProbability <= 0)
            return false;

        if (!base.Use()) return false;

        // Debug.Log("Threshold " + probabilityThreshold + " SelfDamageProbability " + selfDamageProbability);

        if (selfDamageProbability > probabilityThreshold) {
            getCardHandler().player1HC.TakeDamage(Mathf.CeilToInt(Strength * (selfDamageProbability / 100)) / 2);
            if (effectPrefab != null) {
                GameObject eff = Instantiate(effectPrefab, new Vector3(-9, -2, 0), effectPrefab.transform.rotation);
                Destroy(eff.gameObject, 5);
            }
        }
        else if (selfDamageProbability < probabilityThreshold) {
            getCardHandler().player2HC.TakeDamage(Mathf.FloorToInt(Strength * (selfDamageProbability / 100)));
            if (effectPrefab != null) {
                GameObject eff = Instantiate(effectPrefab, new Vector3(9, -2, 0), effectPrefab.transform.rotation);
                Destroy(eff.gameObject, 5);
            }
        }

        holdingSlider.value = 0; // Zero the value
        dmgText.text = "";

        return true;
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
        while (doPercentageSprites) {
            float currentTime = Time.time;
            float timePassed = (currentTime - onDownTime);
            float probab = Mathf.Floor(timePassed * 10);
            probab = Mathf.Floor(probab / 10);
            int probability = (int)(probab * 10);
            float damageProb = (probab * 10);

            // Debug.Log("Probability " + probability + " Diff " + (probability - lastProbability));

            if (probability <= 100) {
                holdingSlider.value = probability;
                dmgText.text = damageProb.ToString() + "%" + " DMG: " + Mathf.FloorToInt(Strength * (damageProb / 100)).ToString();
            }

            yield return null;
        }
        yield return null;
    }
}
