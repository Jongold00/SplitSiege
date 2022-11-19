using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSplashParticle : ParticleController
{
    void Update()
    {
        bool activeParticles = AnyActiveParticlesRemaining();
        if (activeParticles == false)
        {
            Destroy(this.gameObject);
        }
        
    }
}
