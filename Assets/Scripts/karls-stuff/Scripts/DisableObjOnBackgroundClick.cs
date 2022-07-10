using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableObjOnBackgroundClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject objToDisable;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
            objToDisable.SetActive(false);
            this.gameObject.SetActive(false);
    }
}
