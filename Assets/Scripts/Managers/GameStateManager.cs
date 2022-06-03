using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class GameStateManager : MonoBehaviour
{
    #region Singleton

    public static GameStateManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    #endregion Singleton


    public enum GameState{
        Building,
        Fighting,
        Won,
        Lost
    }

    Action<GameStateManager.GameState> listenGameStateChange;
    Action<UnitDataSO> listenEnemyReachedEnd;


    [SerializeField]
    private float buildDuration = 30;

    [SerializeField]
    int castleHealth = 10;


    private GameState currentGameState;

    public GameState GetState()
    {
        return currentGameState;
    }

    public void StartRound()
    {
        EventsManager.instance.GameStateChange(GameState.Fighting);
    }

    public void EndRound()
    {
        EventsManager.instance.GameStateChange(GameState.Building);
    }

    public void GameStateChanged(GameState set)
    {
        currentGameState = set;

        switch (currentGameState)
        {
            case GameState.Building:
                //StartCoroutine(StartBuildTimer(buildDuration));
                break;
        }
    }

    public void EnemyReachedEnd(UnitDataSO unitData)
    {
        castleHealth -= unitData.damageToCastle;

        if (castleHealth <= 0)
        {
            EventsManager.instance.GameStateChange(GameState.Lost);
        }
    }

    private IEnumerator StartBuildTimer(float duration)
    {
        int timer = (int)duration;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        StartRound();

    }


    public void Start()
    {
        listenGameStateChange += GameStateChanged;
        EventsManager.instance.SubscribeGameStateChange(listenGameStateChange);

        listenEnemyReachedEnd +=

        EventsManager.instance.GameStateChange(GameState.Building);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(listenGameStateChange);
    }




}
