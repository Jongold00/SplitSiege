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

    public bool CanFire { get; set; }
    Build build;

    #region Rotation Stuff

    float rotationSpeed = 100;


    #endregion

    private void OnEnable()
    {
        build = GetComponent<Build>();
        build.OnBuildComplete += EnableFiring;
    }

    private void OnDisable()
    {
        build.OnBuildComplete -= EnableFiring;
    }

    private void Awake()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    public virtual void AcquireTarget()
    {
        // units pursuing the highest node are closest to the end
        int highestNodeSoFar = 0;

        // float distance between unit and their current goal node
        float lowestDistanceSoFar = 999999;
        UnitBehavior closest = null;


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            UnitBehavior currentUnit = curr.GetComponent<UnitBehavior>();
            if (currentUnit != null)
            {
                if (currentUnit.currentNode > highestNodeSoFar)
                {
                    highestNodeSoFar = currentUnit.currentNode;
                    lowestDistanceSoFar = currentUnit.GetDistanceFromEnd();
                    closest = currentUnit;

                }
                else if (currentUnit.currentNode == highestNodeSoFar && currentTarget != null && currentTarget.GetDistanceFromEnd() < lowestDistanceSoFar)
                {
                    lowestDistanceSoFar = currentUnit.GetDistanceFromEnd();
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
        base.Update();

        AcquireTarget();
        attackCD -= Time.deltaTime;
        Debug.Log("Getting target");

        if (currentTarget)
        {
            RotateTowardsTarget();

            if (attackCD <= 0 && CanFire)
            {
                Debug.Log("Firing");
                attackCD = offensiveTowerData.GetFireRate();
                Fire();

            }
        }
        else
        {
            anim.SetTrigger("Transition");

        }

        if (!active)
        {
            ResourceManager.instance.UpdateResources(offensiveTowerData.cost * -1, offensiveTowerData.faction);
            active = true;
        }

    }


    protected void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - rotatableTransform.position);
        targetRotation.eulerAngles = new Vector3(rotatableTransform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, zOffsetForRotatableTransform);
        rotatableTransform.rotation = Quaternion.RotateTowards(rotatableTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    protected virtual void Fire()
    {
        anim.SetFloat("Speed", 1 / offensiveTowerData.GetFireRate());
        anim.SetTrigger("Firing");

        StartCoroutine(SpawnProjectile());

        // need to implement a callback from the projectile when it hits target, maybe by subscribing the TakeDamage
        // method below directly to the Action on the projectile script
        // currentTarget.TakeDamage(offensiveTowerData.GetDamage());
    }


    protected virtual IEnumerator SpawnProjectile()
    {
        print("animation length: " + anim.GetCurrentAnimatorStateInfo(0).length); //
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
    }

    private void EnableFiring()
    {
        CanFire = true;
    }


}
