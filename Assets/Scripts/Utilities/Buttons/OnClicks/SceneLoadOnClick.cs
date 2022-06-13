using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadOnClick : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    public void ClickMe()
    {
        SceneLoadingManager.instance.LoadScene(sceneName);
    }
}
