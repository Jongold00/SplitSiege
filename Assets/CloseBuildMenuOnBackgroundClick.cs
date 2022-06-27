using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseBuildMenuOnBackgroundClick : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (BuildTowerPopupMenu.instance.PopupMenuObj.activeInHierarchy)
        {
            Debug.Log("Disabling");
            BuildTowerPopupMenu.instance.HidePopupMenu();
            this.gameObject.SetActive(false);
        }
    }
}
