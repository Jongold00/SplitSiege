using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUpgrader : MonoBehaviour
{
    // Array of each tower prefab. Element 0 should be the lowest level tower and will be the first to be active.
    [SerializeField] private GameObject[] objects;
    public GameObject[] Objects { get => objects; set => objects = value; }
    private GameObject currentlyActive;
    public int NumberOfTowerLevelsAvailable { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        NumberOfTowerLevelsAvailable = Objects.Length;

        if (Objects.Length < 1)
        {
            Debug.LogError("No tower object references set on tower upgrader script");
            return;
        }

        int index = 0;
        foreach (var item in Objects)
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
        return (Array.IndexOf(Objects, currentlyActive) + 1 >= Objects.Length) ? false : true;
    }

    public GameObject SwitchCurrentTowerWithNextLevelTower()
    {
        if (IsUpgradePossible())
        {
            int currentlyActiveIndex = Array.IndexOf(Objects, currentlyActive);
            Socket socketTowerIsPlacedOn = currentlyActive.GetComponent<TowerBehavior>().SocketTowerIsPlacedOn;
            currentlyActive.SetActive(false);
            currentlyActive = Objects[currentlyActiveIndex + 1];
            currentlyActive.SetActive(true);
            currentlyActive.GetComponent<TowerBehavior>().SocketTowerIsPlacedOn = socketTowerIsPlacedOn;
        }

        return currentlyActive;
    }
}
