using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public int turns;
    public int strength;
    public StatusEffects statusEffects;

    public Effect(int forTurns, int effStrength)
    {
        this.turns = forTurns;
        this.strength = effStrength;
    }

    public virtual bool Effective()
    {
        return false;
    }

    public virtual void Changes()
    {

    }

    public virtual void TickActivation()
    {
        Changes();

        if (turns > 0)
            turns--;
        else if (turns == 0)
            strength = 0;
    }
}
