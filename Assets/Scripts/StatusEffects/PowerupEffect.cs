using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PowerupEffect : Effect
{
    public PowerupEffect(int forTurns, int strength) : base(forTurns, strength)
    {
    }

    public override bool Effective()
    {
        if (turns == 0 || strength == 0)
            return false;
        else
            return true;
    }

    public override void TickActivation()
    {
        if (turns <= 0)
            return;

        base.TickActivation();
    }

}
