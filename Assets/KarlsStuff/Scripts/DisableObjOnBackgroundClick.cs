using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisableObjOnBackgroundClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject[] objsToDisable;
    [SerializeField] Image background;
    
    private void OnEnable()
    {
        PopupUI.OnPopupDisplayed += EnableAutoHide;
    }

    private void OnDisable()
    {
        PopupUI.OnPopupDisplayed -= EnableAutoHide;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        SetAllObjsAndThisToInactive();
    }
    public void SetAllObjsAndThisToInactive()
    {
        foreach (GameObject item in objsToDisable)
        {
            item.SetActive(false);
        }

        background.enabled = false;
    }

    protected void EnableAutoHide()
    {
        background.enabled = false;
        Invoke("ActivateObject", 0.1f);
    }

    private void ActivateObject()
    {
        background.enabled = true;
    }
}
