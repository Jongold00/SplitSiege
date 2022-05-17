using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : OffensiveTower
{
    // Start is called before the first frame update

    public float maxMultiplier = 5;
    float currentMultiplier = 0;

    void Start()
    {
        
    }

    public override void Update()
    {
        AcquireTarget();
        attackCD -= Time.deltaTime;

        if (currentTarget)
        {
            RotateTowardsTarget();



            if (attackCD <= 0)
            {
                attackCD = offensiveTowerData.GetFireRate();
                Fire();
                currentMultiplier = Mathf.Min(currentMultiplier + 0.005f, maxMultiplier);

            }
        }
        
        else
        {
            anim.SetTrigger("Transition");
            currentMultiplier = 0;
        }

        if (switchedTarget)
        {
            anim.SetTrigger("Transition");
            currentMultiplier = 0;
        }

        if (!active)
        {
            ResourceManager.instance.UpdateResources(offensiveTowerData.cost * -1, offensiveTowerData.faction);
            active = true;
        }
    }

    protected override IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<LaserProjectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
        projectile.GetComponent<LaserProjectile>().SetDamage(currentMultiplier);



    }


}
