using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerDataSO : ScriptableObject
{
    public new string name;
    public float range;
    public int cost;
    public int faction;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float fireRateMultiplier = 1;
    public GameObject prefab;

    protected OffensiveTowerDataSO OffensiveTowerData;
    protected SupportTowerDataSO SupportTowerData;


    public float GetFireRate()
    {
        return fireRate * fireRateMultiplier;
    }

    public float GetFireRateMultiplier()
    {
        return fireRateMultiplier;
    }
    public void ApplyFireRateMultiplier(float mult)
    {
        fireRateMultiplier *= mult;
    }
}
