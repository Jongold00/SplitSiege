using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    private UnitBehavior target;
    private float movementSpeed;
    private readonly float rotationSpeed = 720f;
    public event Action OnTargetHit;

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.transform);

        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = target.transform.position - transform.position;
        float distanceToMove = movementSpeed * Time.deltaTime;

        if (direction.magnitude < distanceToMove)
        {
            // placeholder
            target.TakeDamage(5);

            OnTargetHit?.Invoke();
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    public void SetTarget(UnitBehavior newTarget, float movementSpeed)
    {
        target = newTarget;
        this.movementSpeed = movementSpeed;
    }
}
