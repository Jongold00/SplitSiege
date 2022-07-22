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

    [SerializeField]
    GameObject loadingScreen;


    Slider loadingSlider;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(AsyncSceneLoad(sceneName));
    }

    IEnumerator AsyncSceneLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += Operation_completed;

        operation.allowSceneActivation = false;

        loadingScreen = GameObject.FindGameObjectWithTag("loading");
        loadingScreen.GetComponent<ActivateAllChildren>().Activate();
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
        float progress;
        float timeElapsed = 0.0f;

        float randomizedPad = Random.Range(1.5f, 2.25f);

        while (!operation.isDone && timeElapsed < randomizedPad * 1.5f)
        {

            timeElapsed += Time.deltaTime;
            progress = Mathf.Min(operation.progress / 0.9f, timeElapsed / randomizedPad);

            loadingSlider.value = progress;
            yield return null;
        }
        operation.allowSceneActivation = true;

        


    }

    private void Operation_completed(AsyncOperation obj)
    {
        loadingScreen.GetComponent<ActivateAllChildren>().DeActivate();
    }
}
