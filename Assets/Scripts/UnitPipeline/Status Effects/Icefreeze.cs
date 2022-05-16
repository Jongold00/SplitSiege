using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icefreeze : StatusEffect
{
    public Icefreeze()
    {
        maxDuration = 2.0f;
        duration = 2.0f;
    }

    public override void OnApply(UnitBehavior attached)
    {
        attached.Stunned(2.0f);
    }

}
