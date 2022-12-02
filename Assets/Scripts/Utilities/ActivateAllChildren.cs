using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAllChildren 
{
    
    public static void ChangeStateOfChildren(GameObject parent, bool state)
    {
        if(parent == null) return;
        
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(state);
        }
    }
}
