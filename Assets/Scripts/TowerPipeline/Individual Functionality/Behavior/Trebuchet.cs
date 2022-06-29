using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : OffensiveTower
{
    [SerializeField] private float splashRange = 10f;
    [SerializeField] private float fullDamageRange = 5f;



    protected override IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<TrebuchetProjectile>().SetTarget(CurrentTarget, offensiveTowerData.SpeedOfProjectile);
    }
}
