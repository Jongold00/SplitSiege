using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TrebuchetProjectile : Projectile
{
    private float movementSpeed;
    Rigidbody rb;

    float currentTimeToImpact;

    Vector3 currentTargetPosition;
    Vector3 currentTargetVelocity;

    Vector3 lastTargetVelocity = Vector3.zero;
    Vector3 lastFramesVelocity = Vector3.zero;

    Vector3 currentTargetAcceleration = Vector3.zero;

    [SerializeField] private float aoeRadius;

    protected override void Update()
    {

    }

    void Start()
    {
        // To ensure no projectiles remain in the scene. Useful currently while working on
        // the treb projectile which is sometimes missing the target
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            // This is used to place the target slightly ahead of the enemies current position
            Vector3 forwardDirection = target.transform.forward;

            // this is used to place the target underneath the map, below the targets current position
            // due to the strange behaviour of the rigidbody physics when it gets too close to the target
            Vector3 upwardDirection = target.transform.up;

            currentTargetPosition = target.transform.position + (forwardDirection * 0.25f) - (upwardDirection * 10f);
            currentTargetVelocity = target.nav.velocity;
            Debug.DrawLine(transform.position, currentTargetPosition, Color.blue);
        }
        else
        {
            return;
        }


        if (currentTimeToImpact > 0)
        {
            currentTimeToImpact -= Time.fixedDeltaTime;
        }


        //print(currentTimeToImpact);
        if (lastTargetVelocity == Vector3.zero)
        {
            currentTargetAcceleration = Vector3.zero;
        }
        else
        {
            // The y axis is used to determine when projectile is close to the ground and should "hit" the enemy
            // and explode. 
            if (transform.position.y > 0.5f)
            {
                currentTargetAcceleration = (target.nav.velocity - lastTargetVelocity) / Time.fixedDeltaTime;

                Vector3 calculateVelocity = CalculateMomentaryVelocity(currentTimeToImpact);
                calculateVelocity.y = rb.velocity.y;


                float ClampFactor = 1 / Vector3.Distance(transform.position, currentTargetPosition);
                ClampFactor = Mathf.Clamp(ClampFactor, .1f, 2f);
                //print(ClampFactor);
                

                calculateVelocity.x = Mathf.Clamp(calculateVelocity.x, lastFramesVelocity.x - ClampFactor, lastFramesVelocity.x + ClampFactor);
                calculateVelocity.z = Mathf.Clamp(calculateVelocity.z, lastFramesVelocity.z - ClampFactor, lastFramesVelocity.z + ClampFactor);

                //print(Vector3.Magnitude(calculateVelocity));

                rb.velocity = calculateVelocity;
            }
            else
            {
                HitTargetsInsideAoeRadius(damage);
                DisableGameObjAndEnableParticle();
                target = null;
                Destroy(gameObject, 5f);
                return;
            }

        }
        lastTargetVelocity = target.nav.velocity;
        lastFramesVelocity = rb.velocity;

    }

    public override void SetTarget(UnitBehavior newTarget, float movementSpeed)
    {
        rb = GetComponent<Rigidbody>();
        target = newTarget;
        this.movementSpeed = movementSpeed;
        rb.velocity = new Vector3(0, 8f, 0);

        currentTimeToImpact = CalculateInitialFlightTime();    

    }

    private Vector3 CalculateMomentaryVelocity(float timeToImpact)
    {

        Vector3 estimatedTargetPosition = currentTargetPosition + (currentTargetVelocity * timeToImpact) + (0.5f * currentTargetAcceleration * (timeToImpact * timeToImpact));

        return (estimatedTargetPosition - transform.position) / timeToImpact;
    }

    private float CalculateInitialFlightTime()
    {
        return (-2 * rb.velocity.y) / Physics.gravity.y;
    }

    protected void HitTargetsInsideAoeRadius(float dmg)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (var hitCollider in hitColliders)
        {
            UnitBehavior unit = hitCollider.GetComponent<UnitBehavior>();
            if (unit != null && unit)
            {
                unit.TakeDamage(dmg);
            }
        }
    }
}
