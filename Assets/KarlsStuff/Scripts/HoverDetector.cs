using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;
    public bool IsHovering { get => isHovering; set => isHovering = value; }

    #region Singleton

    public static HoverDetector instance;


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
    private void OnDisable()
    {
        IsHovering = false;
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        IsHovering = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        IsHovering = false;
    }
}
