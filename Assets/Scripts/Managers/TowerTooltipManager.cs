using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerTooltipManager : PopupUI
{
    private TowerBehavior selectedTower;
    public TowerBehavior SelectedTower { get => selectedTower; set => selectedTower = value; }

    public TextMeshProUGUI label;

    public GameObject goodRangeIndicator;
    public GameObject evilRangeIndicator;


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
        TowerBehavior.OnTowerSelected += ShowRangeIndicator;
    }

    private void OnDisable()
    {
        TowerBehavior.OnTowerSelected -= HandleSelectedTower;
        TowerBehavior.OnTowerSelected -= ShowRangeIndicator;

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

    private void ShowRangeIndicator(GameObject obj)
    {
        BuildTowerPopupMenu.instance.HidePopupMenu();
        OnPopupDisplayed?.Invoke();
        Vector3 indicatorPos = new Vector3(0, 0.1f, 0);
        TowerDataSO data = obj.GetComponent<TowerBehavior>().GetTowerData();

        Vector3 indicatorScale = new Vector3(data.range * 2, data.range * 2, 1f);

        switch (data.faction)
        {
            case 0:
                goodRangeIndicator.SetActive(true);
                goodRangeIndicator.transform.SetParent(obj.transform);
                goodRangeIndicator.transform.localPosition = indicatorPos;
                goodRangeIndicator.transform.localScale = indicatorScale;


                evilRangeIndicator.SetActive(false);
                break;
            case 1:
                evilRangeIndicator.SetActive(true);
                evilRangeIndicator.transform.SetParent(obj.transform);
                evilRangeIndicator.transform.localPosition = indicatorPos;
                evilRangeIndicator.transform.localScale = indicatorScale;


                goodRangeIndicator.SetActive(false);
                break;
        }
    }
}
