using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    [SerializeField] int targetFramerate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFramerate;
    }
}
