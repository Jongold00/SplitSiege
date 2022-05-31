using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildParticle : ParticleController
{
    [SerializeField] ParticleSystem splash;
    private bool splashTriggered;
    private Build build;

    public Build Build 
    { get => build; set { build = value; } }

    protected override void Start()
    {
        base.Start();
        Build.OnBuildComplete += TriggerSplash;
    }
    private void OnDestroy()
    {
        Build.OnBuildComplete -= TriggerSplash;

    }

    // Update is called once per frame
    void Update()
    {
        if (!AnyActiveParticlesRemaining() && splashTriggered)
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
}
