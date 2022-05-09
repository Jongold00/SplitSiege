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


    protected override void Update()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            currentTargetPosition = target.transform.position;
            currentTargetVelocity = target.nav.velocity;
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
            if (Vector3.Distance(transform.position, currentTargetPosition) > 2f)
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
                Destroy(gameObject);
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
        rb.velocity = new Vector3(0, 10f, 0);



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


}
