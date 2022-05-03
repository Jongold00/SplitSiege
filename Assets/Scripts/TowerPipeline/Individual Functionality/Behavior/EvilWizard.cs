using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EvilWizard : OffensiveTower
{
    [SerializeField]
    Projectile[] spells;

    public override void Fire()
    {
        int spellChoice = Random.Range(0, spells.Length - 1);
        base.Fire();

    }

}
