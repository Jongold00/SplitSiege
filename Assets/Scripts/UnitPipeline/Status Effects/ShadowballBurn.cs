using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowballBurn : StatusEffect
{
    float damage;

    public ShadowballBurn()
    {
        damage = 0.1f;
        duration = 1.0f;
        maxDuration = 1.0f;
    }

    public override void Tick(UnitBehavior attached, float deltaT)
    {
        base.Tick(attached, deltaT);

        attached.TakeDamage(damage);
          
    }


}
