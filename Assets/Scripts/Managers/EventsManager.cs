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
            DontDestroyOnLoad(gameObject);

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

    public void GameStateChange(GameStateManager.GameState change)
    {
        if (onGameStateChange == null)
        {
            return;
        }
        onGameStateChange(change);
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

    #region EnemyReachedEnd

    private event Action<UnitDataSO> onEnemyReachesEnd;

    public void SubscribeEnemyReachesEnd(Action<UnitDataSO> func)
    {
        onEnemyReachesEnd += func;
    }

    public void UnsubscribeEnemyReachesEnd(Action<UnitDataSO> func)
    {
        onEnemyReachesEnd -= func;
    }

    public void EnemyReachesEnd(UnitDataSO data)
    {
        if (onEnemyReachesEnd == null)
        {
            return;
        }
        onEnemyReachesEnd(data);
    }
    #endregion

    #region TowerBuilt

    private event Action<TowerDataSO> onTowerBuilt;

    public void SubscribeTowerBuilt(Action<TowerDataSO> func)
    {
        onTowerBuilt += func;
    }

    public void UnsubscribeTowerBuilt(Action<TowerDataSO> func)
    {
        onTowerBuilt -= func;
    }

    public void TowerBuilt(TowerDataSO data)
    {
        if (onTowerBuilt == null)
        {
            return;
        }
        onTowerBuilt(data);
    }
    #endregion


}
