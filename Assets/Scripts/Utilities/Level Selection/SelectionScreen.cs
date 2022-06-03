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



    IEnumerator AsyncSceneLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        loadingScreen = GameObject.FindGameObjectWithTag("loading");
        loadingScreen.GetComponent<ActivateAllChildren>().Activate();
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
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
