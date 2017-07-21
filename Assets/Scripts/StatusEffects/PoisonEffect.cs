using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect {

    public PoisonEffect(int forTurns, int strength) : base(forTurns, strength)
    {
        
    }

    public override void TickActivation()
    {
        if (turns <= 0)
            return;

        statusEffects.tickPoison.SetTrigger("tickPoison");
        statusEffects.health.TakeDamage(strength);

        base.TickActivation();
    }

}
