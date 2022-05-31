using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    string sceneName;


    [SerializeField]
    GameObject loadingScreen;

    [SerializeField]
    Slider loadingSlider;

    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        loadingScreen = GameObject.FindGameObjectWithTag("loading");
    }

    public void Back()
    {
        anim.SetTrigger("FadeOut");
    }

    public void PlayLevel()
    {
        StartCoroutine(AsyncSceneLoad(sceneName));
    }



    IEnumerator AsyncSceneLoad(string toLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(toLoad);
        operation.allowSceneActivation = false;

        loadingScreen.GetComponent<ActivateAllChildren>().Activate();
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();

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
