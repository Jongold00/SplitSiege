using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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
    GameObject buildPhaseUI;

    [SerializeField]
    TextMeshProUGUI buildPhaseTimer;

    [SerializeField]
    GameObject fightPhaseUI;

    Action<GameStateManager.GameState> onGameStateChange;

    private void Start()
    {
        onGameStateChange += ActivateUI;
        EventsManager.instance.SubscribeGameStateChange(onGameStateChange);
    }

    void ActivateUI(GameStateManager.GameState state)
    {
        switch (state)
        {
            case GameStateManager.GameState.Building:
                buildPhaseUI.SetActive(true);
                fightPhaseUI.SetActive(false);
                break;
            case GameStateManager.GameState.Fighting:
                buildPhaseUI.SetActive(false);
                fightPhaseUI.SetActive(true);
                break;

        }

    }
}
