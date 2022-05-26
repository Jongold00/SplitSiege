using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TowerBehavior : MonoBehaviour
{
    protected bool active = false;

    public static event Action<GameObject> OnTowerSelected;


    public abstract void Update();

    private void OnMouseDown()
    {
        OnTowerSelected?.Invoke(gameObject);
    }

    public abstract TowerDataSO GetTowerData();
}
