using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerDataSO : ScriptableObject
{
    public new string name;
    public float range;
    public int cost;
    public int faction;

    public GameObject prefab;

    protected OffensiveTowerDataSO OffensiveTowerData;
    protected SupportTowerDataSO SupportTowerData;




}
