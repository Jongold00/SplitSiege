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

    FMOD.Studio.PARAMETER_ID multiplierID;

    #endregion fmod

    protected override void Awake()
    {
        base.Awake();
        laserBeam = GetComponentInChildren<LaserBeam>();




        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        FMOD.Studio.EventDescription eventDescription;

        fmodInstance.getDescription(out eventDescription);


        FMOD.Studio.PARAMETER_DESCRIPTION multiplierDescription;
        eventDescription.getParameterDescriptionByName("Gameplay Status", out multiplierDescription);
        multiplierID = multiplierDescription.id;
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
                attackCD = offensiveTowerData.GetFireRate();
                Fire();
                currentMultiplier = Mathf.Min(currentMultiplier + 0.005f, maxMultiplier);
                fmodInstance.setParameterByID(multiplierID, currentMultiplier);

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
        projectile.GetComponent<Projectile>().SetTarget(CurrentTarget, offensiveTowerData.SpeedOfProjectile);
        projectile.GetComponent<Projectile>().SetDamage(currentMultiplier);
    }


}
