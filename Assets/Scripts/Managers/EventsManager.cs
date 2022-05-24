using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{

    #region Singleton

    public static EventsManager instance;


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

    #region EnemyDeath

    private event Action<GameObject> onEnemyDeath;

    public void SubscribeEnemyDeath(Action<GameObject> func)
    {
        onEnemyDeath += func;
    }

    public void UnsubscribeEnemyDeath(Action<GameObject> func)
    {
        onEnemyDeath -= func;
    }

    public void EnemyDied(GameObject enemy)
    {
        if (onEnemyDeath == null)
        {
            return;
        }
        onEnemyDeath(enemy);
    }
    #endregion

    #region GameStateChange

    private event Action<GameStateManager.GameState> onGameStateChange;

    public void SubscribeGameStateChange(Action<GameStateManager.GameState> func)
    {
        onGameStateChange += func;
    }

    public void UnSubscribeGameStateChange(Action<GameStateManager.GameState> func)
    {
        onGameStateChange -= func;
    }

    public void GameStateChange(GameStateManager.GameState enemy)
    {
        if (onGameStateChange == null)
        {
            return;
        }
        onGameStateChange(enemy);
    }
    #endregion

}
