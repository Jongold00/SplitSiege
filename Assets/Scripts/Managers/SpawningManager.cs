using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawningManager : MonoBehaviour
{

    public struct UnitData
    {
        public string unitName;
        public int unitHealth;
        public float unitSpeed;
        public int goldValue;
        public int damage;
    }


    [SerializeField]
    (int index, int delay)[][] levelData = new (int, int)[6][]
    {
        new (int,int)[] { (0, 2), (0, 3), (0, 3), (0, 3)},
        new (int,int)[] { (0, 2), (0, 3) },
        new (int,int)[] { (0, 2), (0, 3) },
        new (int,int)[] { (0, 2), (0, 3) },
        new (int,int)[] { (0, 2), (0, 3) },
        new (int,int)[] { (0, 2), (0, 3) },
    };



    #region Singleton

    public static SpawningManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    #endregion Singleton

    #region UnitData
    public GameObject[] unitPrefabs;
    public List<UnitData> units = new List<UnitData>();

    UnitData basicGiant = new UnitData { goldValue = 100, unitHealth = 100, unitName = "Basic Giant", unitSpeed = 12 };


    public Transform instantiatePoint;





    #endregion UnitData

    #region RoundData





    #endregion RoundData


    public void Start()
    {
        units.Add(basicGiant);
    }

    void TestSpawn()
    {

    }
    public IEnumerator SpawnUnit(int i, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject unitGO = Instantiate(unitPrefabs[i], instantiatePoint);
        unitGO.GetComponentInChildren<UnitBehavior>().LoadData(units[i]);


    }


    public void StartRound(int roundNumber)
    {
        float accumulatedDelay = 0;
        for (int i = 0; i < levelData[roundNumber].Length; i++)
        {
            accumulatedDelay += levelData[roundNumber][i].delay;

            StartCoroutine(SpawnUnit(levelData[roundNumber][i].index, accumulatedDelay));

        }
    }

    public void StopSpawning()
    {

    }

}