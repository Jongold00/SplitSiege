using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Laser : OffensiveTower
{
    public float maxMultiplier = 5;
    float currentMultiplier = 0;
    LaserBeam laserBeam;
    UnitBehavior unitBehavior;


    #region fmod

    public EventReference fmodEvent;
    FMOD.Studio.EventInstance fmodInstance;

    #endregion fmod

    protected override void Awake()
    {
        base.Awake();
        laserBeam = GetComponentInChildren<LaserBeam>();


        ResetFmodInstance();





    }

    void ResetFmodInstance()
    {
        fmodInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        fmodInstance.release();


        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        fmodInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
        fmodInstance.start();
        fmodInstance.setPaused(true);
    }

    public override void Update()
    {
        AcquireTarget();

        if (CurrentTarget != null && laserBeam != null)
        {
            laserBeam.Target = CurrentTarget.MainHitPointOfUnit;
        }
        attackCD -= Time.deltaTime;

        if (CurrentTarget)
        {
            RotateTowardsTarget();



            if (attackCD <= 0)
            {
                fmodInstance.setPaused(false);

                Fire();
                currentMultiplier = Mathf.Min(currentMultiplier + 0.005f, maxMultiplier);
            }
        }
        
        else
        {
            ResetFmodInstance();


            anim.SetTrigger("Transition");
            currentMultiplier = 0;
        }

        if (switchedTarget)
        {
            ResetFmodInstance();


            anim.SetTrigger("Transition");
            currentMultiplier = 0;
        }

        if (!active)
        {
            active = true;
        }
    }

    protected override void Fire()
    {
        attackCD = offensiveTowerData.GetFireRate();

        anim.SetFloat("Speed", 1 / offensiveTowerData.GetFireRate());

        StartCoroutine(SpawnProjectile());
    }

    protected override IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(offensiveTowerData.projectileSpawnOffset * offensiveTowerData.GetFireRate());
        GameObject projectile = Instantiate(offensiveTowerData.ProjectilePrefab.gameObject, projectileInstantiatePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(CurrentTarget, offensiveTowerData.SpeedOfProjectile);
        projectile.GetComponent<Projectile>().SetDamage(currentMultiplier);
    }


}
