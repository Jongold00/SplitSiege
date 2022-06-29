using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildTowerButton : MonoBehaviour
{
    [SerializeField]
    TowerDataSO towerData;
    Button button;

    public int towerListIndex;
    public Vector2Int cost;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void Update()
    {
        button.interactable = CanAfford();
    }

    bool CanAfford()
    {
        if (ResourceManager.instance.CheckLegalTranscation(cost.x))
        {
            return true;
        }
        else // if player can't afford this tower
        {
            print("false");
            return false;
        }
    }

    public void OnClickSelect()
    {
        // Grid system has been removed. Below will need to be rewritten to use sockets.
        // GridBuildSystem.Instance.SelectTower(towerListIndex);
    }
}
