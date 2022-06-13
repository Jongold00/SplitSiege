using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : Projectile
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDamage(float damage)
    {
        base.damage = damage;
        transform.localScale = new Vector3(damage / 5, damage / 5, damage / 5);
    }

}
