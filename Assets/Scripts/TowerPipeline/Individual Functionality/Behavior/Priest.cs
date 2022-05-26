using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : SupportTower
{
    public override void ApplyBuff(OffensiveTower tower)
    {
        print(name + " applied a buff to " + tower.name);
        OffensiveTowerDataSO casted = (OffensiveTowerDataSO)tower.GetTowerData();
        casted.ApplyFireRateMultiplier(0.5f);
    }

}
