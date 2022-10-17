using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildTowerPopupMenu : PopupUI
{
    // This script should be placed on a canvas object,
    // inside of which is the popup menu itself and related buttons/images

    [SerializeField]
    private TowerUIElements[] towerImagesAndTooltips;
    [SerializeField]
    private GameObject popupMenuObj;
    public GameObject PopupMenuObj { get => popupMenuObj; private set => popupMenuObj = value; }
    private RectTransform rectTransformOfPopupMenu;
    [SerializeField] GameObject confirmOrCancelObj;
    TowerDataSO towerToBuild;
    [SerializeField] HoverDetector hoverDetector;
    public bool isMouseOverMenu { get => hoverDetector.IsHovering; }

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

    public void DisableAllButtons()
    {
        foreach (TowerUIElements obj in towerImagesAndTooltips)
        {
            obj?.Button.SetActive(false);
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
    public void SwitchTowerImageForTooltipAndConfirmOrCancelMenu(int indexOfButton)
    {
        DisableAllButtons();
        DisableAllTooltips();
        EnableTooltip(indexOfButton);
        EnableConfirmOrCancelMenu();
    }

    public void SetTowerToBuild(TowerDataSO towerToBuild)
    {
        this.towerToBuild = towerToBuild;
    }

    public void DisplayPopupMenuAtViewportOfObj(GameObject obj)
    {
        OnPopupDisplayed?.Invoke();
        PopupMenuObj.SetActive(true);
        ResetPopupMenuToStartingLayout();

        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(obj.transform.position);
        rectTransformOfPopupMenu.anchoredPosition = viewportPoint;
        rectTransformOfPopupMenu = CalculateRectTransToFitScreen(rectTransformOfPopupMenu);
    }

    public void HidePopupMenu()
    {
        PopupMenuObj.SetActive(false);
    }

    public void EnableConfirmOrCancelMenu()
    {
        confirmOrCancelObj.SetActive(true);
    }

    public void DisableConfirmOrCancelMenu()
    {
        confirmOrCancelObj.SetActive(false);
    }

    public void ResetPopupMenuToStartingLayout()
    {
        DisableAllTooltips();
        DisableConfirmOrCancelMenu();
        EnableAllButtons();
    }

    public void ConfirmSelection()
    {
        TowerSocketManager.instance.BuildTower(towerToBuild);
    }
}