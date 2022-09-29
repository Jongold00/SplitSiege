using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITowerBuilder
{
    public event Action OnBuildComplete;
    public bool BuildingComplete { get; }

}
