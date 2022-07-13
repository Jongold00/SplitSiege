using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    float slowPercentage;

    public Slow(float slowAmount, float slowDuration)
    {
        id = 1;
        stackType = StackType.Multiplicative;
        slowPercentage = slowAmount;
        duration = slowDuration;
        maxDuration = slowDuration;
    }


    public float GetSlowMultiplier()
    {
        return slowPercentage / 100;
    }

    public override void Tick(UnitBehavior attached, float deltaT)
    {
        base.Tick(attached, deltaT);
    }
}
