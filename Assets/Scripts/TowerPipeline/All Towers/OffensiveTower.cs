using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveTower : TowerBehavior
{
    [SerializeField]
    protected OffensiveTowerDataSO offensiveTowerData;
    protected UnitBehavior currentTarget;


    [SerializeField]
    Transform rotatableTransform;
    [SerializeField]
    float zOffsetForRotatableTransform;

    [SerializeField]
    protected Transform projectileInstantiatePoint;
    // attackCD is assigned manually below using data pulled from the towers themselves
    protected float attackCD;


    #region Rotation Stuff

    float rotationSpeed = 100;


    #endregion
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

        if (currentTarget)
        {
            RotateTowardsTarget();
           
            if (attackCD <= 0)
            {
                Debug.Log("Firing");
                attackCD = offensiveTowerData.GetFireRate();
                Fire();

            }
        }

        if (!active)
        {
            ResourceManager.instance.UpdateResources(offensiveTowerData.cost * -1, offensiveTowerData.faction);
            active = true;
        }

    }


    void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - rotatableTransform.position);
        targetRotation.eulerAngles = new Vector3(rotatableTransform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, zOffsetForRotatableTransform);
        rotatableTransform.rotation = Quaternion.RotateTowards(rotatableTransform.rotation, targetRotation, Time.deltaTime * 100f);
    }

    public virtual void Fire()
    {
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);

        // need to implement a callback from the projectile when it hits target, maybe by subscribing the TakeDamage
        // method below directly to the Action on the projectile script
        // currentTarget.TakeDamage(offensiveTowerData.GetDamage());
    }


}
