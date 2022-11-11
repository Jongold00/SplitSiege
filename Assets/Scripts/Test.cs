using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    RectTransform myRectTransform;
    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        //Debug.Log(RendererExtensions.IsFullyVisibleFrom(myRectTransform, Camera.main));
    }
}