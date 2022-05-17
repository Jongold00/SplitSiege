using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected UnitBehavior target;
    public event Action OnTargetHit;

    private float rotationSpeed = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.transform);

        Vector3 direction = target.transform.position - transform.position;
        float distanceToMove = speed * Time.deltaTime;

        if (direction.magnitude < distanceToMove)
        {
            // placeholder
            target.TakeDamage(5);

            OnTargetHit?.Invoke();
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }

    public virtual void SetTarget(UnitBehavior newTarget, float moveSpeed)
    {
        target = newTarget;
        this.speed = moveSpeed;
    }
}

