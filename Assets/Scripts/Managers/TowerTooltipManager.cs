using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerTooltipManager : MonoBehaviour
{
    private TowerBehavior selectedTower;
    public TowerBehavior SelectedTower { get => selectedTower; set => selectedTower = value; }

    public TextMeshProUGUI label;

    #region Singleton

    public static TowerTooltipManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        TowerBehavior.OnTowerSelected += HandleSelectedTower;
    }

    private void OnDisable()
    {
        TowerBehavior.OnTowerSelected -= HandleSelectedTower;
    }


    #endregion Singleton


    private void FormatPopup()
    {
        label.text = selectedTower.name;
    }

    private void HandleSelectedTower(GameObject obj)
    {
        TowerStatsPopupMenu.instance.HidePopupMenu(obj);
        TowerStatsPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
        SelectedTower = obj.GetComponent<TowerBehavior>();
        FormatPopup();
    }
}
