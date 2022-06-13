using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SpawningRound : ScriptableObject
{

    public int credits;
    public List<SpawningEvent> spawns;
    public float globalCD;

    public bool CanSpawnMore(int creditsLeft)
    {
        foreach (SpawningEvent curr in spawns)
        {
            if (creditsLeft >= curr.GetCost())
            {
                return true;
            }
        }
        return false;
    }

}
