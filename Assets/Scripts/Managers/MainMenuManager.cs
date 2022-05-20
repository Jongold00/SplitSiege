using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(sceneName);
    }
}
