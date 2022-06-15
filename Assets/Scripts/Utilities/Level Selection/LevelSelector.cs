using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    GameObject selectionScreen;
    // Start is called before the first frame update
    public void OnMouseDown()
    {
        SelectionScreen newScreen = Instantiate(selectionScreen).GetComponent<SelectionScreen>();
        newScreen.Activate();
    }
}
