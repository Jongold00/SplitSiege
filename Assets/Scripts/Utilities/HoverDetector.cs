using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetector : MonoBehaviour
{
    public bool IsHovering { get {
            return EventSystem.current.IsPointerOverGameObject();
        }}
}
