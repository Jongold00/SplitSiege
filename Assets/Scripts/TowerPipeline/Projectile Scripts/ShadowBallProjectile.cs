using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBallProjectile : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        statusEffects.Add(new ShadowballBurn());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

}
