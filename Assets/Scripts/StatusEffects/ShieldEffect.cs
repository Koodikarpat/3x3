using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShieldEffect : Effect
{
    public ShieldEffect(int forTurns, int strength) : base(forTurns, strength)
    {
        Changes();
    }

    public override bool Effective()
    {
        if (turns == 0 || strength <= 0)
            return false;
        else
            return true;
    }

    public override void Changes()
    {
        base.Changes();
    }

    public override void TickActivation()
    {
        base.TickActivation();
    }

}
