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
    [SerializeField] GameObject confirmOrCancelObj;
    TowerDataSO towerToBuild;
    [SerializeField] HoverDetector hoverDetector;
    public bool isMouseOverMenu { get => hoverDetector.IsHovering; }
    private GameObject buildMenuContainer;
    private Vector3 startingLocalScale;
    private RectTransform rectTransform;

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
        rectTransform = GetComponent<RectTransform>();
        startingLocalScale = rectTransform.localScale;
        buildMenuContainer = GetComponentInParent<Transform>().gameObject;
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
        PopupMenuObj.SetActive(true);
        ResetPopupMenuToStartingLayout();
        buildMenuContainer.transform.position = obj.transform.position;


        // Ensures size of canvas is scaled based on distance from camera so it always appears to be same size to the user
        Canvas canvas = GetComponent<Canvas>();
        float dist = Vector3.Distance(canvas.transform.position, Camera.main.transform.position);
        canvas.transform.localScale = startingLocalScale * dist / (rectTransform.rect.height / 20);

        OnPopupDisplayed?.Invoke(popupMenuObj);
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