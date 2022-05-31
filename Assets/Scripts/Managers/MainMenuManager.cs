using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    #region Singleton

    public static MainMenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    #endregion Singleton

    [SerializeField]
    GameObject[] tabs;

    [SerializeField]
    GameObject loadingScreen;

    [SerializeField]
    Slider loadingSlider;

    public void ToggleTab(GameObject tab)
    {
        foreach (GameObject go in tabs)
        {
            if (go == tab)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(AsyncSceneLoad(sceneName));
    }

    IEnumerator AsyncSceneLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);
        float progress;
        float timeElapsed = 0.0f;


        while (!operation.isDone && timeElapsed < 2.0f)
        {
            print(timeElapsed);
            timeElapsed += Time.deltaTime;
            progress = Mathf.Min(operation.progress / 0.9f, timeElapsed / 1.5f);

            loadingSlider.value = progress;
            yield return null;
        }
        operation.allowSceneActivation = true;


    }
}
