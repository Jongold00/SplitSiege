using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Unit", fileName = "Unit Template")]
public class UnitDataSO : ScriptableObject
{
    public string unitName;
    public int unitHealth;
    public float unitSpeed;
    public GameObject unitPrefab;
    public int goldValue;
    public int creditCost;
    public int damageToCastle;


    public void Init(string n, int h, float s, GameObject pf, int v, int credits, int castleDamage)
    {
        unitName = n;
        unitHealth = h;
        unitSpeed = s;
        unitPrefab = pf;
        goldValue = v;
        creditCost = credits;
        damageToCastle = castleDamage;
    }

}


