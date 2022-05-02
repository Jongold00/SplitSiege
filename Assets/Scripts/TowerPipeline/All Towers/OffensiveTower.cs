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
    protected Transform projectileInstantiatePoint;
    // attackCD is assigned manually below using data pulled from the towers themselves
    protected float attackCD;


    #region Rotation Stuff

    protected  bool rotating;
    private float lerpRatio;

    Vector3 endLerp = Vector3.zero;
    Vector3 currentLerp;

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
            print(rotating);
            if (attackCD <= 1 && !rotating)
            {
                //print("attack CD" + attackCD);

                rotating = true;

                attackCD = 0.25f;

                Vector3 positionDiff = currentTarget.transform.position - transform.position;
                float zRotation = Mathf.Atan2(positionDiff.y, positionDiff.x) * Mathf.Rad2Deg;
                endLerp = new Vector3(0, 0, zRotation);

                lerpRatio = (endLerp.z - rotatableTransform.localRotation.z) / (attackCD / Time.deltaTime);

               
            }
            if (rotating)
            {

                currentLerp = Vector3.Lerp(currentLerp, endLerp, lerpRatio);
                rotatableTransform.localRotation = Quaternion.Euler(0, 0, currentLerp.z);
            }
            
           
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

    public virtual void Fire()
    {
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<BallistaProjectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);

        // need to implement a callback from the projectile when it hits target, maybe by subscribing the TakeDamage
        // method below directly to the Action on the projectile script
        // currentTarget.TakeDamage(offensiveTowerData.GetDamage());
        rotating = false;
    }


}
