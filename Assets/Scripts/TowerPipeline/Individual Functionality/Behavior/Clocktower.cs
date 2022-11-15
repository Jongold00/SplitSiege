using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clocktower :  OffensiveTower
{
    [SerializeField] private float slowAmount, slowDuration;
    
    
    protected override void Fire()
    {
        anim.SetFloat("Speed", 1 / offensiveTowerData.GetFireRate());
        attackCD = offensiveTowerData.GetFireRate();

        FMOD_PlayOneShot shootSFX;
        if (TryGetComponent<FMOD_PlayOneShot>(out shootSFX))
        {
            shootSFX.Play();

        }
    }

    //overides the update to be able to fire at all enemies in range at once. 
    public override void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            if (curr.TryGetComponent(out UnitBehavior currentUnit))
            {
                currentUnit.AttachStatusEffect(new Slow(slowAmount,slowDuration));
            }
        }
    }


}

