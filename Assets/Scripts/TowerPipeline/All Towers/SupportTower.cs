using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SupportTower : TowerBehavior
{
    [SerializeField]
    protected SupportTowerDataSO supportTowerData;

    //RESPONSIBLE FOR RENDERING A TowerDataSO INTO A FUNCTIONAL TOWER   

    List<OffensiveTower> affectedTowers;
    public override void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, supportTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            OffensiveTower currentTower = curr.GetComponent<OffensiveTower>();
            if (currentTower && !affectedTowers.Contains(currentTower))
            {
                ApplyBuff(currentTower);
                affectedTowers.Add(currentTower);
            }
        }
    }

    public abstract void ApplyBuff(OffensiveTower tower);
}
