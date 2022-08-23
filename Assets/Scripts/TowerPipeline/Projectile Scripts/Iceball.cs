using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Projectile
{
    public float aoeRadius;
    // Start is called before the first frame update
    void Start()
    {
        statusEffects.Add(new Stun(2.0f));
        statusEffects.Add(new Slow(75f, 4f));

    }

    
    protected override void HitTarget()
    {
        target.TakeDamage(damage);
        target.AttachStatusEffect(new Stun(2.0f));

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (var hitCollider in hitColliders)
        {
            UnitBehavior unit = hitCollider.GetComponent<UnitBehavior>();
            if (unit != null && unit && unit != target)
            {
                print(statusEffects[1].GetRemainingDuration());
                unit.AttachStatusEffect(new Slow(75f, 4f));
                unit.TakeDamage(damage / 2);

            }
        }

        Destroy(this.gameObject);
        GetComponent<FMOD_PlayOneShot>().Play();
    }
    
}
