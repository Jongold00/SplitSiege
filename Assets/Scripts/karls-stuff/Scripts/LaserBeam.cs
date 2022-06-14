using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private ParticleController hitParticles;
    [SerializeField] private GameObject hitPointLight;
    [SerializeField] private Vector3 hitPointLightOffset;
    [SerializeField] private Transform fire;
    private Transform target = null;
    public Transform Target { get => target; set => target = value; }
    private UnitBehavior unitBehaviorOfTarget;

    private void Update()
    {
        ToggleLaserOnOff(target != null);

        if (target == null)
        {
            return;
        }

        if (unitBehaviorOfTarget == null)
        {
            unitBehaviorOfTarget = target.GetComponent<UnitBehavior>();
        }


        laser.SetPosition(0, fire.position);
        laser.SetPosition(1, Target.transform.position);
        hitParticles.ChangePositionOfAllParticles(Target.position, Quaternion.LookRotation(fire.position - Target.transform.position));
        hitPointLight.transform.position = target.position + hitPointLightOffset;

        if (unitBehaviorOfTarget != null)
        {
            ToggleLaserOnOff(unitBehaviorOfTarget.isDead());
        }
    }

    private void ToggleLaserOnOff(bool laserEnabled)
    {
        laser.gameObject.SetActive(laserEnabled);
        hitParticles.gameObject.SetActive(laserEnabled);
        fire.gameObject.SetActive(laserEnabled);
        hitPointLight.SetActive(laserEnabled);
    }

}
