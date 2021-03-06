using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class UnitNavigation : MonoBehaviour
{

    PathCreator nav;


    public float yOffset;
    float speed = 1.0f;
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
        distanceTravelled += GetSpeed() * Time.deltaTime;

        Vector3 desiredPosition = nav.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        desiredPosition.y += yOffset;

        transform.position = desiredPosition;


        Vector3 desiredRotation = nav.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).eulerAngles;
        desiredRotation.z = 0;


        transform.rotation = Quaternion.Euler(desiredRotation);

        if (distanceTravelled > nav.path.length)
        {
            EventsManager.instance.EnemyReachesEnd(unitData);
            Destroy(gameObject);
        }


    }

    public Vector3 GetPositionInSeconds(float seconds)
    {
        return nav.path.GetPointAtDistance(distanceTravelled + (GetSpeed() * seconds), EndOfPathInstruction.Stop);
    }

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    public float GetSpeed()
    {
        if (GetComponent<UnitBehavior>().isStunned())
        {
            return 0;
        }
        return speed * GetComponent<UnitBehavior>().GetTotalSpeedMultiplier();
    }

    public void SetSpeed(float set)
    {
        speed = set;
    }

}
