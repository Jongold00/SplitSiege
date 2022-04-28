using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveTower : TowerBehavior
{
    [SerializeField]
    protected OffensiveTowerDataSO offensiveTowerData;
    protected UnitBehavior currentTarget;
    // attackCD is assigned manually below using data pulled from the towers themselves
    protected float attackCD;
    public void AcquireTarget()
    {
        float lowestDistance = 999999;
        UnitBehavior closest = null;


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            UnitBehavior currentUnit = curr.GetComponent<UnitBehavior>();
            if (currentUnit)
            {
                lowestDistance = Mathf.Min(lowestDistance, currentUnit.GetDistanceFromEnd());
                closest = currentUnit;
            }
        }

        currentTarget = closest;
    }

    public override void Update()
    {
        AcquireTarget();
        attackCD -= Time.deltaTime;
        Debug.Log("Getting target");
        if (attackCD <= 0 && currentTarget)
        {
            Debug.Log("Firing");
            attackCD = offensiveTowerData.GetFireRate();
            Fire();

        }
        if (!active)
        {
            ResourceManager.instance.UpdateResources(offensiveTowerData.cost * -1, offensiveTowerData.faction);
            active = true;
        }

    }

    public virtual void Fire()
    {
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
        // need to implement a callback from the projectile when it hits target, maybe by subscribing the TakeDamage
        // method below directly to the Action on the projectile script
        // currentTarget.TakeDamage(offensiveTowerData.GetDamage());
    }


}
