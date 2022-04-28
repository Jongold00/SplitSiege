using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    private static Mouse3D Instance;

    [SerializeField] private LayerMask mouseColiderLayerMask;
    
    private void Awake()
    {
        Instance = this;
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    private Vector3 GetMouseWorldPosition_Instance()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColiderLayerMask) ? hit.point : Vector3.zero;
    }
}
