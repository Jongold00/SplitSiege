using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Build : MonoBehaviour
{
    public event Action OnBuildComplete;
    [SerializeField] private float buildSpeed;
    [SerializeField] private float startYBuildOffset;
    private Vector3 targetPosWhileBuilding;
    public bool BuildingComplete { get; private set; }
    [SerializeField] GameObject buildParticlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(buildParticlePrefab, transform.position, buildParticlePrefab.transform.rotation);
        obj.GetComponent<BuildParticle>().SubscribeToOnBuildComplete(this);
        targetPosWhileBuilding = transform.position;
        ChangeYPosition(startYBuildOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (!BuildingComplete)
        {
            MoveTowardPosWhileBuilding(targetPosWhileBuilding);
        }
    }

    private void MoveTowardPosWhileBuilding(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, buildSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            BuildingComplete = true;
            OnBuildComplete?.Invoke();
        }
    }

    private void ChangeYPosition(float distance)
    {
        Vector3 currentPos = transform.position;
        currentPos.y += distance;
        transform.position = currentPos;
    }
}
