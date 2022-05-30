using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildParticle : ParticleController
{
    [SerializeField] ParticleSystem splash;
    Build target;

    private void OnDisable()
    {
        target.OnBuildComplete -= TriggerSplash;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AnyActiveParticlesRemaining())
        {
            Destroy(this.gameObject);
        }
    }
    public void TriggerSplash()
    {
        splash.Play();
        StopAllParticlesExceptSplash();
    }

    private void StopAllParticlesExceptSplash()
    {
        foreach (ParticleSystem item in allParticles)
        {
            if (item != splash)
            {
                item.Stop();
            }
        }
    }

    public void SubscribeToOnBuildComplete(Build build)
    {
        target = build;
        target.OnBuildComplete += TriggerSplash;
    }
}
