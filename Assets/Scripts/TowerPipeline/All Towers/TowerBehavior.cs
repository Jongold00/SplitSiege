using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehavior : MonoBehaviour
{
    protected bool active = false;

    public abstract void Update();
}
