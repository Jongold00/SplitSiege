using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    protected bool switchedTarget = false;

    protected Animator anim;

    #region Rotation Stuff

    float rotationSpeed = 100;


    #endregion

    Action<GameObject> thingy;

    private void Awake()
    {
        offensiveTowerData = Instantiate(offensiveTowerData);

        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public virtual void AcquireTarget()
    {

        float furthestSoFar = 0;
        UnitBehavior closest = null;


        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            if (curr.TryGetComponent(out UnitBehavior currentUnit))
            {
                UnitNavigation unitNav = curr.GetComponent<UnitNavigation>();
                if (unitNav.GetDistanceTravelled() > furthestSoFar)
                {
                    furthestSoFar = unitNav.GetDistanceTravelled();
                    closest = currentUnit;
                }
            }
        }

        if (closest != currentTarget)
        {
            switchedTarget = true;
        }
        else
        {
            switchedTarget = false;
        }

        currentTarget = closest;
    }

    public override void Update()
    {

        AcquireTarget();
        attackCD -= Time.deltaTime;

        if (currentTarget)
        {
            RotateTowardsTarget();
            anim.SetBool("Firing", true);



            if (attackCD <= 0)
            {
                attackCD = offensiveTowerData.GetFireRate();
                Fire();

            }
        }
        else
        {
            anim.SetBool("Firing", false);

        }

        if (!active)
        {
            ResourceManager.instance.UpdateResources(offensiveTowerData.cost * -1, offensiveTowerData.faction);
            active = true;
        }

    }

    public override TowerDataSO GetTowerData()
    {
        return offensiveTowerData;
    }

    protected void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - rotatableTransform.position);
        targetRotation.eulerAngles = new Vector3(rotatableTransform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, zOffsetForRotatableTransform);
        rotatableTransform.rotation = Quaternion.RotateTowards(rotatableTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public virtual void Fire()
    {
        anim.SetFloat("Speed", 1 / offensiveTowerData.GetFireRate());
        anim.SetTrigger("Firing");

        print("damage: " + offensiveTowerData.GetDamage());

        StartCoroutine(SpawnProjectile());

        // need to implement a callback from the projectile when it hits target, maybe by subscribing the TakeDamage
        // method below directly to the Action on the projectile script
        // currentTarget.TakeDamage(offensiveTowerData.GetDamage());
    }

    
    protected virtual IEnumerator SpawnProjectile()
    {

        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
        projectileScript.SetDamage(offensiveTowerData.GetDamage());
    }


}
