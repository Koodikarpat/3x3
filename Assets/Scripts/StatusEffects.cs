using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffects : MonoBehaviour
{
    public HealthController health;
    public List<Effect> effects = new List<Effect>(new Effect[] { new PoisonEffect(0, 0), new ShieldEffect(0, 0), new LifestealEffect(0, 0) } );
	public Animator tickPoison; // Poison animator object
	public Text PoisonCounter; // Poison turns text object

    void Start()
    {
        if (health == null)
            health = GetComponent<HealthController>();

        foreach (Effect effect in effects) {
            effect.statusEffects = this;
            effect.Changes();
        }

        UIChange();
    }

    public void AddStatusEffect(Effect effect)
    {
        effects.First(effects => effects.GetType() == effect.GetType()).turns += effect.turns;
        effects.First(effects => effects.GetType() == effect.GetType()).strength += effect.strength;

        foreach (Effect eff in effects) 
            eff.Changes();

        UIChange();
    }

    public Effect GetEffect(Type effectType)
    {
        if (effects.Any(effects => effects.GetType() == effectType))
            return effects.First(effects => effects.GetType() == effectType);
        else
            return null;
    }

    public void Tick()
    {
        foreach (Effect effect in effects)
            effect.TickActivation();

        UIChange();
    }
		
    private void UIChange()
    {
        foreach (Effect effect in effects) {
            if (effect.GetType() == typeof(PoisonEffect))
                PoisonCounter.text = effect.turns.ToString();
        }
    }
}
