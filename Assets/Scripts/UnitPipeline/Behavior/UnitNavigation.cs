using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class UnitNavigation : MonoBehaviour
{

    PathCreator nav;


    public float yOffset;
    public float speed = 1.0f;
    public EndOfPathInstruction endOfPathInstruction;

    public UnitDataSO unitData;

    float distanceTravelled = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        nav = FindObjectOfType<PathCreator>();
        speed = unitData.unitSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;

        Vector3 desiredPosition = nav.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        desiredPosition.y += yOffset;

        transform.position = desiredPosition;


        Vector3 desiredRotation = nav.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).eulerAngles;
        desiredRotation.z = 0;


        transform.rotation = Quaternion.Euler(desiredRotation);


    }

    public Vector3 GetPositionInSeconds(float seconds)
    {
        return nav.path.GetPointAtDistance(distanceTravelled + (speed * seconds), EndOfPathInstruction.Stop);
    }

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }
}
