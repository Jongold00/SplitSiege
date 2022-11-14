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

    public TextMeshProUGUI buildPhaseTimer;

    [SerializeField]
    Image[] stars;


    [SerializeField]
    Image[] castleHealthMeters;

    [SerializeField]
    TextMeshProUGUI[] healthBarLabels;


    [SerializeField]
    Color fullHP;

    [SerializeField]
    Color noHP;

    float lerpValue;


    [SerializeField]
    TextMeshProUGUI[] resourceBar;

    float currentResource = 0;

    [SerializeField]
    TextMeshProUGUI[] waveCounter;

    float currentWave = 0;



    Action<GameStateManager.GameState> onGameStateChange;
    Action<float> onResourcesUpdated;

    [Range(0, 5)]
    [SerializeField] float buildMenuBottomOfScreenOutOfBoundsCorrection;
    
    [Range(0, 5)]
    [SerializeField] float buildMenuTopOfScreenOutOfBoundsCorrection; 


    private void Start()
    {
        onGameStateChange += ActivateUI;
        EventsManager.instance.SubscribeGameStateChange(onGameStateChange);

        onResourcesUpdated += OnResourceUpdate;
        EventsManager.instance.SubscribeResourceUpdate(onResourcesUpdated);

    }

    private void OnEnable()
    {
        PopupUI.OnPopupDisplayed += MoveRectTransInsideScreenBounds;
    }

    private void OnDisable()
    {
        PopupUI.OnPopupDisplayed -= MoveRectTransInsideScreenBounds;        
    }

    private void OnDestroy()
    {
        EventsManager.instance.UnSubscribeGameStateChange(onGameStateChange);
        EventsManager.instance.UnsubscribeResourceUpdate(onResourcesUpdated);


    }
    void ActivateUI(GameStateManager.GameState state)
    {
        print(state);
        switch (state)
        {
            case GameStateManager.GameState.Building:
                ToggleTab(0);
                WaveCounterComponent();
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
            case GameStateManager.GameState.Story:
                ToggleTab(4);
                break;
        }

    }

    public void Update()
    {
        CastleHealthUIComponent();
        ResourceBarComponent();
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



    void CastleHealthUIComponent()
    {
        foreach (Image curr in castleHealthMeters)
        {
            curr.fillAmount = GameStateManager.instance.GetPercentCastleHealth();
            //curr.color = Color.Lerp(noHP, fullHP, GameStateManager.instance.GetPercentCastleHealth());
        }

        foreach (TextMeshProUGUI curr in healthBarLabels)
        {
            curr.text = ((int)GameStateManager.instance.GetPercentCastleHealth() * 100).ToString() + " / 100";

        }
    }

    void OnResourceUpdate(float delta)
    {
        currentResource += delta;
    }

    void ResourceBarComponent()
    {
        foreach (TextMeshProUGUI curr in resourceBar)
        {
            curr.text = currentResource.ToString();
        }
    }
    

    void WaveCounterComponent()
    {
        currentWave++;
        foreach (TextMeshProUGUI curr in waveCounter)
        {
            curr.text = "Wave " + currentWave.ToString();
        }
    }

    void MoveRectTransInsideScreenBounds(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);

        bool bottomVisible = RendererExtensions.IsHalfFullyVisible(rectTransform, Camera.main, RectTransHalf.Bottom, out float distToMove);

        if (!bottomVisible)
        {
            Vector2 newPos = new Vector2(rectTransform.anchoredPosition.x, (rectTransform.anchoredPosition.y + distToMove) * buildMenuBottomOfScreenOutOfBoundsCorrection);
            rectTransform.anchoredPosition = newPos;
            return;
        }
        
        bool topVisible = RendererExtensions.IsHalfFullyVisible(rectTransform, Camera.main, RectTransHalf.Top, out distToMove);
        if (!topVisible)
        {
            Vector2 newPos = new Vector2(rectTransform.anchoredPosition.x, (rectTransform.anchoredPosition.y - distToMove) * buildMenuTopOfScreenOutOfBoundsCorrection);
            rectTransform.anchoredPosition = newPos;
            return;
        }
    }
}
