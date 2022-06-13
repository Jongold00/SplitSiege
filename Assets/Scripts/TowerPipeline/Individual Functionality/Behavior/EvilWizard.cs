using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EvilWizard : OffensiveTower
{
    [SerializeField]
    GameObject[] spells;

    HashSet<UnitBehavior> inRange = new HashSet<UnitBehavior>();

    public override void AcquireTarget()
    {
        inRange.Clear();


        float furthestSoFar = 0;
        UnitBehavior closest = null;


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            if (curr.TryGetComponent(out UnitBehavior currentUnit))
            {

                inRange.Add(currentUnit);

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

    protected override void Fire()
    {
        int spellChoice = Random.Range(0, spells.Length);

        if (spellChoice == 1)
        {
            StartCoroutine(ShootAtEveryUnitInRange(spellChoice));
        }
        else
        {
            StartCoroutine(SpawnProjectile(spellChoice));

        }


    }

    IEnumerator ShootAtEveryUnitInRange(int spellChoice)
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());

        foreach (UnitBehavior curr in inRange)
        {
            GameObject projectile = Instantiate(spells[spellChoice].gameObject, projectileInstantiatePoint.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().SetTarget(curr, offensiveTowerData.SpeedOfProjectile);
        }
    }


    IEnumerator SpawnProjectile(int spellChoice)
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(spells[spellChoice].gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
    }

}
