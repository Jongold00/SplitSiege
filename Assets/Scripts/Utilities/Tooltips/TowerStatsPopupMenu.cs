using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerStatsPopupMenu : PopupUI
{
    // This script should be placed on a canvas object,
    // inside of which is the popup menu itself and related buttons/images


    [SerializeField]
    private GameObject popupMenuObj;
    public GameObject PopupMenuObj { get => popupMenuObj; private set => popupMenuObj = value; }

    private RectTransform rectTransformOfPopupMenu;

    private TowerBehavior selectedTower;
    public TowerBehavior SelectedTower { get => selectedTower; set => selectedTower = value; }

    public TextMeshProUGUI label;

    public GameObject goodRangeIndicator;
    public GameObject evilRangeIndicator;

    private Transform goodRangeIndicatorOriginalParent;
    private Transform evilRangeIndicatorOriginalParent;

    #region Singleton

    public static TowerStatsPopupMenu instance;


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


    #endregion Singleton

    private void Start()
    {
        rectTransformOfPopupMenu = PopupMenuObj.GetComponent<RectTransform>();
        goodRangeIndicatorOriginalParent = goodRangeIndicator.transform.parent;
        evilRangeIndicatorOriginalParent = evilRangeIndicator.transform.parent;
    }

    private void OnEnable()
    {
        TowerBehavior.OnTowerSelected += HandleSelectedTower;
        TowerBehavior.OnTowerSelected += ShowRangeIndicator;
        Socket.OnSocketSelected += HandleSocketSelected;

    }

    private void OnDisable()
    {
        TowerBehavior.OnTowerSelected -= HandleSelectedTower;
        TowerBehavior.OnTowerSelected -= ShowRangeIndicator;
        Socket.OnSocketSelected -= HandleSocketSelected;
    }


    public void DisplayPopupMenuAtViewportOfObj(GameObject obj)
    {
        Vector2 offset = new Vector2(0, 80f);
        PopupMenuObj.SetActive(true);

        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
        rectTransformOfPopupMenu.anchoredPosition = viewportPoint + offset;
    }

    public void HidePopupMenu()
    {
        goodRangeIndicator.transform.SetParent(goodRangeIndicatorOriginalParent);
        evilRangeIndicator.transform.SetParent(evilRangeIndicatorOriginalParent);
        goodRangeIndicator.SetActive(false);
        evilRangeIndicator.SetActive(false);
        PopupMenuObj.SetActive(false);
    }

    private void FormatPopup()
    {
        label.text = selectedTower.name;
    }

    private void HandleSelectedTower(GameObject obj)
    {
        TowerStatsPopupMenu.instance.HidePopupMenu();
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

    private void HandleSocketSelected(GameObject socket)
    {
        HidePopupMenu();
    }
}
