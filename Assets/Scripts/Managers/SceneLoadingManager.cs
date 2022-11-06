using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{

    #region Singleton

    public static SceneLoadingManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            instance = this;
        }
    }


    #endregion Singleton

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(AsyncSceneLoad(sceneName));
    }

    public void LoadSceneWithoutLoadScreen(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    IEnumerator AsyncSceneLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += Operation_completed;

        operation.allowSceneActivation = false;
        
        ActivateAllChildren.ChangeStateOfChildren(loadingScreen,true);
        

        while (operation.progress < 0.9f)
        {
            loadingSlider.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }

        loadingSlider.value = 1;
        
        //Prevents the loading screen from flashing and out of its almost no load time
        yield return new WaitForSeconds(.05f);
        
        operation.allowSceneActivation = true;
        
    }

    private void Operation_completed(AsyncOperation obj)
    {
        ActivateAllChildren.ChangeStateOfChildren(loadingScreen,false);
    }
}
