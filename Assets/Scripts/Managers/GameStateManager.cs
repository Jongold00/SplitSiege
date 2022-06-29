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
        }
        castleHealth = maxCastleHealth;
    }


    #endregion Singleton


    public enum GameState{
        Building,
        Fighting,
        Won,
        Lost
    }

    public string currentLevelName = "level1";

    Action<GameStateManager.GameState> listenGameStateChange;
    Action<UnitDataSO> listenEnemyReachedEnd;


    [SerializeField]
    private float buildDuration = 30;

    [SerializeField]
    int maxCastleHealth = 10;

    int castleHealth;

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
                StartCoroutine(StartBuildTimer(buildDuration));
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
            InGameUIManager.instance.buildPhaseTimer.text = timer.ToString();
        }
        StartRound();

    }


    public void Start()
    {
        listenGameStateChange += GameStateChanged;
        EventsManager.instance.SubscribeGameStateChange(listenGameStateChange);

        listenEnemyReachedEnd += EnemyReachedEnd;
        EventsManager.instance.SubscribeEnemyReachesEnd(listenEnemyReachedEnd);

        EventsManager.instance.GameStateChange(GameState.Building);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(listenGameStateChange);
        EventsManager.instance.UnsubscribeEnemyReachesEnd(listenEnemyReachedEnd);

    }

    public int GetNumStarsOnWin()
    {
        if (castleHealth <= 3)
        {
            return 1;
        }
        if (castleHealth <= 7)
        {
            return 2;
        }
        return 3;
    }

    public float GetPercentCastleHealth()
    {
        return (float)castleHealth / (float)maxCastleHealth;
    }



}
