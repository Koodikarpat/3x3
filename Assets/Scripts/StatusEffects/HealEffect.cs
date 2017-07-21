using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HealEffect : Effect
{
    public HealEffect(int forTurns, int strength) : base(forTurns, strength)
    {
    }

    public override void TickActivation()
    {
        if (turns <= 0)
            return;

        statusEffects.health.Heal(strength);

        base.TickActivation();
    }

}
