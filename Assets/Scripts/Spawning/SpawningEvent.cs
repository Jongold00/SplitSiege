using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawningEvent : ScriptableObject
{
    [SerializeField]
    float weight;

    [SerializeField]
    float cooldown;

    [SerializeField]
    GameObject[] spawns;



    public int GetCost()
    {
        return 4;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public GameObject[] GetSpawns()
    {
        return spawns;
    }

    public bool Success()
    {
        float roll = Random.Range(0f, 1f);
        bool outcome = roll < weight;
        //Debug.Log("rolling event " + name + ",  roll is " + roll + "... outcome is " + outcome);
        return outcome;
    }




    



}
