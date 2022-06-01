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

    #region MusicVolumeChange

    private event Action<float> onMusicVolumeChange;

    public void SubscribeMusicVolumeChange(Action<float> func)
    {
        onMusicVolumeChange += func;
    }

    public void UnSubscribeMusicVolumeChange(Action<float> func)
    {
        onMusicVolumeChange -= func;
    }

    public void MusicVolumeChange(float value)
    {
        if (onMusicVolumeChange == null)
        {
            return;
        }
        onMusicVolumeChange(value);
    }
    #endregion

    #region SFXVolumeChange

    private event Action<float> onSFXVolumeChange;

    public void SubscribeSFXVolumeChange(Action<float> func)
    {
        onSFXVolumeChange += func;
    }

    public void UnSubscribeSFXVolumeChange(Action<float> func)
    {
        onSFXVolumeChange -= func;
    }

    public void SFXVolumeChange(float value)
    {
        if (onMusicVolumeChange == null)
        {
            return;
        }
        onSFXVolumeChange(value);
    }
    #endregion


}
