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


    [SerializeField]
    private float buildDuration = 30;




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

        EventsManager.instance.GameStateChange(GameState.Building);
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(listenGameStateChange);
    }



    #region Music
    /*
    [SerializeField]
    private AudioClip themeLayer1;
    [SerializeField]
    private AudioClip themeLayer2;
    [SerializeField]
    private AudioClip themeLayer3;

    [SerializeField]
    private AudioSource audioSource1;

    [SerializeField]
    private AudioSource audioSource2;

    [SerializeField]
    float layer1MaxVolume;
    [SerializeField]
    float layer2MaxVolume;
    [SerializeField]
    float layer3MaxVolume;

    float timeInTheme;

    void ChangeMusic()
    {
        timeInTheme = audioSource1.time;
        switch(currentGameState)
        {
            case GameState.Building:
                audioSource2.clip = themeLayer1;
                break;
            case GameState.Fighting:
                audioSource2.clip = themeLayer2;
                break;
            default:
                print("error, invalid game state");
                break;
        }

        SwapAudioSources(timeInTheme);
        CrossFade(currentGameState);


    }

    void SwapAudioSources(float time)
    {
        AudioClip tempClip = audioSource1.clip;
        audioSource1.clip = audioSource2.clip;
        audioSource2.clip = tempClip;

        float tempVol = audioSource1.volume;
        audioSource1.volume = audioSource2.volume;
        audioSource2.volume = tempVol;

        audioSource1.time = time;
        audioSource2.time = time;



        audioSource1.Play();
        audioSource2.Play();
    }

    void CrossFade(GameState gameState)
    {
        switch (currentGameState)
        {
            case GameState.Building:
                StartCoroutine(AudioFadeOut.FadeOut(audioSource2, 0.1f));
                StartCoroutine(AudioFadeIn.FadeIn(audioSource1, 0.1f, layer1MaxVolume)); break;
            case GameState.Fighting:
                StartCoroutine(AudioFadeOut.FadeOut(audioSource2, 0.1f));
                StartCoroutine(AudioFadeIn.FadeIn(audioSource1, 0.1f, layer2MaxVolume)); break;
            default:
                print("error, invalid game state");
                break;
        }

        

    }

    */
    #endregion


}
