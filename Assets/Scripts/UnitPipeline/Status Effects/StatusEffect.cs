using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected float maxDuration = 0.0f;
    protected float duration = 0.0f;
    public int id = 0;
    public StackType stackType;

    public enum StackType
    {
        Refreshing,
        Additive,
        Multiplicative
    }


    public StatusEffect()
    {
        duration = 0.0f;
        maxDuration = 0.0f;
        stackType = StackType.Refreshing;
    }
    public bool compareID(int compare)
    {
        return compare == id;
    }
    public virtual void Tick(UnitBehavior attached, float deltaT)
    {
        duration -= deltaT;

        if (duration <= 0)
        {
            attached.StatusEffectExpired(this);
        }
    }
    
    public virtual void Refresh()
    {
        duration = maxDuration;
    }

    public virtual void OnApply(UnitBehavior attached)
    {

    }


    public float GetRemainingDuration()
    {
        return duration;
    }

}
