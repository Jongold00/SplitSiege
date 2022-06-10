using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : OffensiveTower
{
    [SerializeField] private float splashRange = 10f;
    [SerializeField] private float fullDamageRange = 5f;


    protected override void Fire()
    {
        base.Fire();
        //GetComponent<FMOD_PlayOneShot>().Play();
    }

    protected override IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);

        projectile.GetComponent<TrebuchetProjectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);

    }
}
