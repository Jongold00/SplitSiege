using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    float slowPercentage;

    public Slow(float slowAmount, float slowDuration)
    {
        Debug.Log("slow created");

        slowPercentage = slowAmount;
        duration = slowDuration;
        maxDuration = slowDuration;
    }

    public override void OnApply(UnitBehavior attached)
    {
        Debug.Log("slow applied");
        base.OnApply(attached);
    }

    public float GetSlowMultiplier()
    {
        return slowPercentage / 100;
    }
}
