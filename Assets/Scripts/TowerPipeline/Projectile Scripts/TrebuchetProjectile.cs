using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TrebuchetProjectile : Projectile
{
    Rigidbody rb;

    float currentTimeToImpact;



    [SerializeField] private float aoeRadius;

    Vector3 projectedImpactPosition;



    protected override void Update()
    {
        if (transform.position.y <= projectedImpactPosition.y && target != null)
        {
            HitTargetsInsideAoeRadius(damage);
            DisableGameObjAndEnableParticle();
            target = null;
            Destroy(gameObject, 5f);
        }
    }

    void Start()
    {

    }


    public override void SetTarget(UnitBehavior newTarget, float movementSpeed)
    {
        if (newTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        rb = GetComponent<Rigidbody>();
        target = newTarget;

        statusEffects.Add(new Stun(1.5f));
        statusEffects.Add(new Slow(65f, 3.0f));



        currentTimeToImpact = CalculateInitialFlightTime(transform.position.y);

        projectedImpactPosition = target.GetComponent<UnitNavigation>().GetPositionInSeconds(currentTimeToImpact);

        Vector3 initialVelocity = (projectedImpactPosition - transform.position) / currentTimeToImpact;

        initialVelocity.y = 10;

        rb.velocity = initialVelocity;

    }



    private float CalculateInitialFlightTime(float initialY)
    {
        
        return (-10.0f - Mathf.Sqrt((10.0f * 10.0f) - (2 * Physics.gravity.y * initialY))) / Physics.gravity.y;
    }



    protected void HitTargetsInsideAoeRadius(float dmg)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (var hitCollider in hitColliders)
        {
            UnitBehavior unit = hitCollider.GetComponent<UnitBehavior>();
            if (unit != null && unit)
            {
                unit.AttachStatusEffects(statusEffects);
                unit.TakeDamage(dmg);
            }
        }
    }
}
