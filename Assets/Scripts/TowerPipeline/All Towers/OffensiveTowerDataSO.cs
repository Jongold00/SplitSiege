using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Offensive Tower Stats", fileName = "Tower Template")]
public class OffensiveTowerDataSO : TowerDataSO
{
    protected float damage;
    protected float damageMultiplier = 1;
    [SerializeField] private Transform projectilePrefab;
    public Transform ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }

    [SerializeField] private float speedOfProjectile;
    public float SpeedOfProjectile { get => speedOfProjectile; set => speedOfProjectile = value; }

    public float projectileSpawnOffset = 1.0f;
    [SerializeField] private bool aoe;
    public bool Aoe { get => aoe; set => aoe = value; }
    [SerializeField] private float aoeRadius;
    public float AoeRadius { get => aoeRadius; set => aoeRadius = value; }


    private void Awake()
    {
        OffensiveTowerData = this;
    }

    public float GetDamage()
    {
        return damage * damageMultiplier;
    }
    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public void ApplyDamageMultiplier(float mult)
    {
        damageMultiplier *= mult;
    }
}
