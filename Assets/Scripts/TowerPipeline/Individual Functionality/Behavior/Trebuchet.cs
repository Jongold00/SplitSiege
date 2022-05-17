using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : OffensiveTower
{
    [SerializeField] private float splashRange = 10f;
    [SerializeField] private float fullDamageRange = 5f;

    [SerializeField] private float upforce = 5f;

    public override void Fire()
    {
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<TrebuchetProjectile>().SetTarget(currentTarget, offensiveTowerData.SpeedOfProjectile);
        GetComponent<FMOD_PlayOneShot>().Play();
    }
}
