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

    [SerializeField] private ButtonAvailability upgradeButtonAvailability;

    private Vector3 startingPopupMenuObjLocalScale;
    RectTransform rectTransform;
    [SerializeField] private float offsetYPosition;

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
        rectTransform =  GetComponent<RectTransform>();
        startingPopupMenuObjLocalScale = rectTransformOfPopupMenu.localScale;
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
        HidePopupMenu();

        PopupMenuObj.SetActive(true);

        Vector3 currentPos = obj.transform.position;
        Vector3 newPos = new Vector3(currentPos.x, currentPos.y + offsetYPosition, currentPos.z);
        rectTransform.position = newPos;

        Canvas canvas = GetComponent<Canvas>();
        float dist = Vector3.Distance(canvas.transform.position, Camera.main.transform.position);
        canvas.transform.localScale = startingPopupMenuObjLocalScale * dist;

        SelectedTower = obj.GetComponent<TowerBehavior>();
        TowerDataSO towerDataSO = selectedTower.GetTowerData();

        if (towerDataSO.level >= selectedTower.GetComponentInParent<TowerUpgrader>().NumberOfTowerLevelsAvailable)
        {
            Debug.Log("upgrade NOT available");
            upgradeButtonAvailability.MakeButtonUnavailable();
        }
        else
        {
            Debug.Log("upgrade available");
            upgradeButtonAvailability.MakeButtonAvailable();
        }

        FormatPopup();
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
        TowerStatsPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
    }

    private void ShowRangeIndicator(GameObject obj)
    {
        BuildTowerPopupMenu.instance.HidePopupMenu();
        OnPopupDisplayed?.Invoke(popupMenuObj);
        Vector3 indicatorPos = new Vector3(0, 0.1f, 0);
        TowerDataSO data = obj.GetComponent<TowerBehavior>().GetTowerData();            

        Vector3 indicatorScale = new Vector3(
            (data.range * 2) * (1 / obj.transform.localScale.x),
            (data.range * 2) * (1 / obj.transform.localScale.y),
            1f * (1 / obj.transform.localScale.z));

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
