using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ParticleController : MonoBehaviour
{
    protected ParticleSystem[] allParticles;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void StopAllParticles()
    {
        foreach (ParticleSystem item in allParticles)
        {
            item.Stop();
        }
    }

    public void StopAParticle(ParticleSystem particleToStop)
    {
        particleToStop.Stop();
    }

    public bool AnyActiveParticlesRemaining()
    {
        return allParticles.Any((x) => x.isPlaying);
    }
}
