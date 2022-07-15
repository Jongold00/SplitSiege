using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        statusEffects.Add(new Stun(2.0f));
        statusEffects.Add(new Slow(75f, 4f));

    }


}
