using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ResourceManager : MonoBehaviour
{

    #region Singleton

    public static ResourceManager instance;

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

    #region UI

    [SerializeField]
    TextMeshProUGUI goldText;

    [SerializeField]
    TextMeshProUGUI popText;


    void UpdateUI()
    {
        goldText.text = gold.ToString();
        popText.text = population.ToString();
    }

    #endregion


    public int gold;
    public int population;

    [SerializeField]
    private int PassiveGoldRate;
    private int PassivePopulationRate;

    public int startingGold = 500;
    public int startingPop = 500;

    private void Start()
    {
        UpdateResources(startingGold, 0);
        UpdateResources(startingPop, 1);
    }

    public void UpdateResources(int delta, int resource)
    {
        switch (resource)
        {
            case 0:
                gold += delta;
                break;

            case 1:
                population += delta;
                break;
            default:
                break;
        }

        UpdateUI();
    }
        public bool CheckLegalTranscation(int cost, int resource) 
    {
        switch (resource)
        {
            case 0:
                return (cost <= gold);
            case 1:
                return (cost <= population);
            default:
                return false;
        }
    }

    public void PassiveUpdate()
    {

        switch (GameStateManager.instance.GetState())
        {
            case GameStateManager.GameState.Building:
                break;
            case GameStateManager.GameState.Fighting:
                gold += PassiveGoldRate;
                population += PassivePopulationRate;
                break;
            default:
                break;
        }

        UpdateUI();
    }

    
}
