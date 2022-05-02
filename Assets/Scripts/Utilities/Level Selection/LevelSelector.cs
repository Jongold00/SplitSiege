using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    GameObject SelectionScreen;
    // Start is called before the first frame update
    public void OnMouseDown()
    {
        Instantiate(SelectionScreen);
    }
}
