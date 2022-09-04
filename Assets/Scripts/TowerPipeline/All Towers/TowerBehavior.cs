using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TowerBehavior : MonoBehaviour
{
    protected bool active = false;

    public static event Action<GameObject> OnTowerSelected;
    public static GameObject CurrentlySelectedTower;


    public abstract void Update();

    private void OnMouseDown()
    {
        // This If statement is to prevent the tower stats popup menu loading again when a button on the popup menu is
        // clicked and the pointer is over the tower. The behaviour we want here is for the button click to be registered
        // but the click on the tower ignored
        if (CurrentlySelectedTower == this.gameObject && TowerStatsPopupMenu.instance.PopupMenuObj.activeInHierarchy)
        {
            return;
        }
        OnTowerSelected?.Invoke(gameObject);
        CurrentlySelectedTower = this.gameObject;
    }

    public abstract TowerDataSO GetTowerData();
}
