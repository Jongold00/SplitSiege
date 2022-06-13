using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RiseMotionBuilder : MonoBehaviour, ITowerBuilder
{
    public event Action OnBuildComplete;
    [SerializeField] private float buildSpeed;
    [SerializeField] private float startYBuildOffset;
    private Vector3 targetPosWhileBuilding;
    public bool BuildingComplete { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
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
