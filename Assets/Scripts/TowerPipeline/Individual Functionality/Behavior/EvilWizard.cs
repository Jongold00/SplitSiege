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

        float lowestDistance = 999999;
        UnitBehavior closest = null;


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            UnitBehavior currentUnit = curr.GetComponent<UnitBehavior>();
            if (currentUnit)
            {
                inRange.Add(currentUnit);
                lowestDistance = Mathf.Min(lowestDistance, currentUnit.GetDistanceFromEnd());
                closest = currentUnit;
            }
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
            print("enemies in range: " + inRange.Count);

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
