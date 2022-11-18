using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildParticle : BuildParticleController
{
    [SerializeField] ParticleSystem splash;
    private bool splashTriggered;

    protected override void Start()
    {
        base.Start();
        Builder.OnBuildComplete += StopAllParticles;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        Builder.OnBuildComplete -= StopAllParticles;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!AnyActiveParticlesRemaining() && splashTriggered)
        {
            Destroy(this.gameObject);
        }
    }
    public override void StopAllParticles()
    {
        splash.Play();
        splashTriggered = true;
        foreach (ParticleSystem item in allParticles)
        {
            if (item != splash)
            {
                item.Stop();
            }
        }
    }
}
