using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class InGameUIManager : MonoBehaviour
{
    #region Singleton

    public static InGameUIManager instance;


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

    #endregion




    [SerializeField]
    GameObject[] tabs;

    [SerializeField]
    TextMeshProUGUI buildPhaseTimer;

    [SerializeField]
    Image[] stars;

    Action<GameStateManager.GameState> onGameStateChange;

    private void Start()
    {
        onGameStateChange += ActivateUI;
        EventsManager.instance.SubscribeGameStateChange(onGameStateChange);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(onGameStateChange);

    }
    void ActivateUI(GameStateManager.GameState state)
    {
        switch (state)
        {
            case GameStateManager.GameState.Building:
                ToggleTab(0);
                break;
            case GameStateManager.GameState.Fighting:
                ToggleTab(1);
                break;
            case GameStateManager.GameState.Won:
                ToggleTab(2);
                ActivateStars();
                break;
            case GameStateManager.GameState.Lost:
                ToggleTab(3);
                break;


        }

    }

    void ToggleTab(int turnOn)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == turnOn)
            {
                tabs[i].SetActive(true);
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }

    void ActivateStars()
    {
        for (int i = 0; i < GameStateManager.instance.GetNumStarsOnWin(); i++)
        {
            stars[i].color = Color.white;
        }
    }

}
