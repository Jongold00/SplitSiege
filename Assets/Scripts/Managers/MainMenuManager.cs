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

        loadingScreen = GameObject.FindGameObjectWithTag("loading");
        loadingScreen.GetComponent<ActivateAllChildren>().Activate();
        float progress;
        float timeElapsed = 0.0f;

        float randomizedPad = Random.Range(1.5f, 2.25f);

        while (!operation.isDone && timeElapsed < randomizedPad * 1.5f)
        {
            print(timeElapsed);
            timeElapsed += Time.deltaTime;
            progress = Mathf.Min(operation.progress / 0.9f, timeElapsed / randomizedPad);

            loadingSlider.value = progress;
            yield return null;
        }
        operation.allowSceneActivation = true;


    }
}
