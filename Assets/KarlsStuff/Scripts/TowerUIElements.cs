using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUIElements
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject tooltip;

    public GameObject Button { get => button; set => button = value; }
    public GameObject Tooltip { get => tooltip; set => tooltip = value; }
}
