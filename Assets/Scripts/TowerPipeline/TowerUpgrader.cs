using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUpgrader : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private GameObject currentlyActive;

    // Start is called before the first frame update
    void Start()
    {
        if (objects.Length < 1)
        {
            Debug.LogError("No tower object references set on tower upgrader script");
            return;
        }

        int index = 0;
        foreach (var item in objects)
        {
            if (index == 0)
            {
                item.SetActive(true);
                currentlyActive = item;
            }
            else
            {
                item.SetActive(false);
            }

            index++;
        }
    }

    public bool IsUpgradePossible()
    {
        return (Array.IndexOf(objects, currentlyActive) + 1 >= objects.Length) ? false : true;
    }

    public void SwitchCurrentTowerWithNextLevelTower()
    {
        if (IsUpgradePossible())
        {
            int currentlyActiveIndex = Array.IndexOf(objects, currentlyActive);
            currentlyActive.SetActive(false);
            objects[currentlyActiveIndex + 1].SetActive(true);
        }
    }
}
