using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    #region UI

    [SerializeField]
    private GameObject startRoundButton;
    [SerializeField]
    private TextMeshProUGUI timerText;
    #endregion UI

    public enum GameState{
        Building,
        Fighting
    }

    public int currentRound = 0;
    private GameState currentGameState;

    [SerializeField]
    private float buildDuration = 30;
    private int timer;
    public GameState GetState()
    {
        return currentGameState;
    }

    public void StartRound()
    {
        currentRound++;
        SpawningManager.instance.StartRound(currentRound-1);
        SetState(GameState.Fighting);
    }

    public void EndRound()
    {
        SetState(GameState.Building);
    }

    void SetState(GameState set)
    {
        currentGameState = set;
        switch (currentGameState)
        {
            case GameState.Building:
                startRoundButton.SetActive(true);
                StartCoroutine(StartBuildTimer(buildDuration));
                break;
            case GameState.Fighting:
                startRoundButton.SetActive(false);
                break;
        }
        ChangeMusic();
    }

    private IEnumerator StartBuildTimer(float duration)
    {
        timer = (int)duration;

        while (timer > 0)
        {
            timerText.text = Mathf.RoundToInt(timer).ToString();
            yield return new WaitForSeconds(1);
            timer--;
        }
        StartRound();

    }


    public void Start()
    {
        SetState(GameState.Building);
    }

    private void Update()
    {
     

        

    }



    #region Music

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


    #endregion

}
