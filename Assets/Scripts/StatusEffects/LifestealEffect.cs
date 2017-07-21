using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LifestealEffect : Effect
{
    public LifestealEffect(int forTurns, int strength) : base(forTurns, strength)
    {
    }

    public override bool Effective()
    {
        if (strength > 0 && turns > 0)
            return true;
        else
            return false;
    }

    public override void TickActivation()
    {
        base.TickActivation();
    }

}