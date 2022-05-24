using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : SupportTower
{
    public override void ApplyBuff(OffensiveTower tower)
    {
        // 20042022 - Need to look at the below methods and probably separate them and apply some deeper logic
        // Do we really want to always apply a damage boost and attack speed boost at the same time?
        // Design doc says the graveyard tower:
        // - Decreases attack speed of nearby good towers
        // - Increases attack speed of nearby evil towers
        // Also would be good to do more testing on the values rather than always buffing by the amount of
        // the previous damage multiplier


        // tower.applyDamageMultiplier(towerData.GetDamageMultiplier());
        // tower.applyAttackSpeedMultiplier(towerData.GetASMultiplier());
    }

}
    