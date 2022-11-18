using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUpgrader : MonoBehaviour
{
    // Array of each tower prefab. Element 0 should be the lowest level tower and will be the first to be active.
    [SerializeField] private GameObject[] objects;
    private GameObject currentlyActive;
    public int NumberOfTowerLevelsAvailable { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        NumberOfTowerLevelsAvailable = objects.Length;

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

    public GameObject SwitchCurrentTowerWithNextLevelTower()
    {
        if (IsUpgradePossible())
        {
            int currentlyActiveIndex = Array.IndexOf(objects, currentlyActive);
            currentlyActive.SetActive(false);
            currentlyActive = objects[currentlyActiveIndex + 1];
            currentlyActive.SetActive(true);
        }

        return currentlyActive;
    }
}
