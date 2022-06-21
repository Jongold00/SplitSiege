using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawningManager : MonoBehaviour
{

    #region Singleton

    public static SpawningManager instance;

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
    int credits;

    [SerializeField]
    List<SpawningEvent> spawns;

    [SerializeField]
    List<SpawningRound> rounds;

    [SerializeField]
    Transform spawnLocation;

    SpawningRound currentRound;


    List<float> cooldowns = new List<float>();

    [SerializeField]
    float globalCD = 10.0f;

    bool spawning = false;

    Action<GameStateManager.GameState> listenGameStateChange;


    void ListenGameStateChange(GameStateManager.GameState newState)
    {

        switch(newState)
        {
            case GameStateManager.GameState.Building:
                spawning = false;
                break;

            case GameStateManager.GameState.Fighting:
                StartNewRound();
                break;

            case GameStateManager.GameState.Won:
                spawning = false;
                break;

            case GameStateManager.GameState.Lost:
                spawning = false;
                break;
        }
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(listenGameStateChange);

    }

    private void Start()
    {
        listenGameStateChange += ListenGameStateChange;
        EventsManager.instance.SubscribeGameStateChange(listenGameStateChange);

    }



    private void Update()
    {

        if (spawning)
        {

            HandleCooldowns();
            AttemptEvents();


            if (RoundIsWon()) // can't spawn anything more on this round
            {
                if (LevelIsWon()) // can't spawn anything more on this round and theres no more rounds after this one
                {
                    EventsManager.instance.GameStateChange(GameStateManager.GameState.Won);
                }

                else
                {
                    EventsManager.instance.GameStateChange(GameStateManager.GameState.Building);
                }

            }
        }
        
    }

    bool LevelIsWon()
    {
        return rounds.IndexOf(currentRound) >= rounds.Count - 1;
    }

    bool RoundIsWon()
    {
        return !currentRound.CanSpawnMore(credits) && UnitBehavior.allEnemies.Count <= 0;
    }

    void StartNewRound()
    {
        if (currentRound == null)
        {
            currentRound = rounds[0];
        }

        else
        {
            currentRound = rounds[rounds.IndexOf(currentRound) + 1];
        }



        credits = currentRound.credits;
        spawns = currentRound.spawns;
        globalCD = currentRound.globalCD;


        ClearCooldowns();

        spawning = true;
    }


    void ClearCooldowns()
    {
        cooldowns = new List<float>();
        for (int i = 0; i < spawns.Count; i++)
        {
            cooldowns.Add(0.0f);
        }
    }






    void AttemptEvents()
    {
        for (int i = 0; i < spawns.Count; i++)
        {
            if (OffCooldown(i) && credits >= spawns[i].GetCost())
            {
                InvokeEventCooldown(i);
                if (spawns[i].Success())
                {
                    credits -= spawns[i].GetCost();
                    StartCoroutine(SpawnEvent(spawns[i]));
                    InvokeGlobalCooldown();
                    return;
                }
            }
        }
    }


    IEnumerator SpawnEvent(SpawningEvent spawningEvent)
    {
        foreach (GameObject unit in spawningEvent.GetSpawns())
        {
            Instantiate(unit, spawnLocation.position, Quaternion.identity, null);

            float randomWait = UnityEngine.Random.Range(0.5f, 2.0f);
            yield return new WaitForSeconds(randomWait);
        }
    }



    void HandleCooldowns()
    {
        for (int i = 0; i < spawns.Count; i++)
        {
            if (cooldowns[i] > 0)
            {
                cooldowns[i] -= Time.deltaTime;
            }
        }
    }

    void InvokeGlobalCooldown()
    {
        for (int i = 0; i < spawns.Count; i++)
        {
            cooldowns[i] += globalCD;
        }
    }

    void InvokeEventCooldown(int index)
    {
        cooldowns[index] += spawns[index].GetCooldown();
    }

    bool OffCooldown(int index)
    {
        return cooldowns[index] <= 0;
    }

}
