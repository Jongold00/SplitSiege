using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerPopupMenu : MonoBehaviour
{
    // This script should be placed on a canvas object,
    // inside of which is the popup menu itself and related buttons/images

    [SerializeField]
    private TowerUIElements[] towerImagesAndTooltips;
    [SerializeField]
    private GameObject popupMenuObj;
    public GameObject PopupMenuObj { get => popupMenuObj; private set => popupMenuObj = value; }
    private RectTransform rectTransformOfPopupMenu;

    #region Singleton

    public static BuildTowerPopupMenu instance;


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
    }

    public void EnableAllButtons()
    {
        foreach (TowerUIElements obj in towerImagesAndTooltips)
        {
            obj?.Button.SetActive(true);
        }
    }

    public void DisableAllTooltips()
    {
        foreach (TowerUIElements obj in towerImagesAndTooltips)
        {
            obj?.Tooltip.SetActive(false);
        }
    }

    public void EnableButton(int indexOfButton)
    {
        towerImagesAndTooltips[indexOfButton].Button.SetActive(true);
    }

    public void DisableButton(int indexOfButton)
    {
        towerImagesAndTooltips[indexOfButton].Button.SetActive(false);
    }

    public void EnableTooltip(int indexOfTooltip)
    {
        towerImagesAndTooltips[indexOfTooltip].Tooltip.SetActive(true);
    }

    public void DisableTooltip(int indexOfTooltip)
    {
        towerImagesAndTooltips[indexOfTooltip].Tooltip.SetActive(false);
    }
    public void SwitchTowerImageForTooltip(int indexOfButton)
    {
        EnableAllButtons();
        DisableButton(indexOfButton);
        DisableAllTooltips();
        EnableTooltip(indexOfButton);
    }

    public void DisplayPopupMenuAtViewportOfObj(GameObject obj)
    {
        PopupMenuObj.SetActive(true);
        DisableAllTooltips();
        EnableAllButtons();

        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
        rectTransformOfPopupMenu.anchoredPosition = viewportPoint;
    }

    public void HidePopupMenu()
    {
        PopupMenuObj.SetActive(false);
    }
}
