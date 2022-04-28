using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Support Tower Stats", fileName = "Tower Template")]
public class SupportTowerDataSO : TowerDataSO
{
    private void Awake()
    {
        SupportTowerData = this;
    }
}
