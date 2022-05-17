using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        statusEffect = new Icefreeze();
    }


}
