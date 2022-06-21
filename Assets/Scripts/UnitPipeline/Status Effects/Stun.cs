using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public Stun(float stunDuration)
    {
        id = 2;
        stackType = StackType.Refreshing;

        duration = stunDuration;
        maxDuration = stunDuration;
    }



}
