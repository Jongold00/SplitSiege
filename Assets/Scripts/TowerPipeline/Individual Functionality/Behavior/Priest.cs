using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : SupportTower
{
    public override void ApplyBuff(OffensiveTower tower)
    {
        tower.GetTowerData().ApplyDamageMultiplier(2);
    }

}
