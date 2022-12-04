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

    private void OnTriggerEnter(Collider other)
    {
        if (Array.Exists(tagsToCollideWith, x => x == other.tag))
        {
            OnTrapCollision?.Invoke(other.gameObject, this);  
        }
    }

    public void Explode()
    {
        explosionParticles.SetActive(true);
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
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
}
