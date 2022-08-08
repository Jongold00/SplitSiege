using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetector : MonoBehaviour
{
    public bool IsHovering { get {
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
            return EventSystem.current.IsPointerOverGameObject();
        }}

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
}
