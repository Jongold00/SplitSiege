using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trap : MonoBehaviour
{
    public Action<GameObject, Trap> OnTrapCollision;
    [SerializeField] String[] tagsToCollideWith;
    private NavNode navNodePlacedOn;
    public NavNode NavNodePlacedOn { get => navNodePlacedOn; set => navNodePlacedOn = value; }

    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private GameObject buildingInProgressParticles;
    [SerializeField] private GameObject buildingFinishedParticles;
    MeshRenderer meshRenderer;

    private float timeToBuildTrap;
    public float TimeToBuildTrap { get => timeToBuildTrap; set => timeToBuildTrap = value; }

    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.enabled = false;
        Invoke("BuildingFinished", timeToBuildTrap);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Array.Exists(tagsToCollideWith, x => x == other.tag) && meshRenderer.enabled)
        {
            OnTrapCollision?.Invoke(other.gameObject, this);  
        }
    }

    public void Explode()
    {
        explosionParticles.SetActive(true);

        meshRenderer.enabled = false;
        ParticleController particleController = explosionParticles.GetComponent<ParticleController>();
        StartCoroutine(DestroyParentAfterParticlesHaveFinished(particleController));
        
    }

    IEnumerator DestroyParentAfterParticlesHaveFinished(ParticleController particleController)
    {
        if (particleController == null)
        {
            Destroy(transform.parent.gameObject);
        }

        yield return new WaitForSeconds(1);
    }

    private void BuildingFinished()
    {
        ParticleSystem[] particleSystems = buildingInProgressParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in particleSystems)
        {
            p.Stop();
        }
        buildingFinishedParticles.SetActive(true);
        meshRenderer.enabled = true;
    }
}
