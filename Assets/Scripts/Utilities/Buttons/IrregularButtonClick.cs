using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IrregularButtonClick : MonoBehaviour
{
    public float alphaThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;

    }

}
