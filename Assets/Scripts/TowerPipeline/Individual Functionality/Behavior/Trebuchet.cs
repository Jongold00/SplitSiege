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
        /*
                Collider[] hitColliders = Physics.OverlapSphere(currentTarget.transform.position, splashRange);
                foreach (Collider curr in hitColliders)
                {
                    UnitBehavior currentUnit = curr.GetComponent<UnitBehavior>();
                    if ( curr.gameObject.TryGetComponent<UnitBehavior>( out UnitBehavior unitHit ))
                    {
                        if (Vector3.Distance( currentTarget.transform.position, unitHit.transform.position ) < fullDamageRange )
                        {
                            unitHit.TakeDamage(offensiveTowerData.GetDamage());
                        }
                        else
                        {
                            unitHit.TakeDamage( Mathf.RoundToInt(offensiveTowerData.GetDamage() * .5f) );
                        }

                    }

                }
        */

    }
}
