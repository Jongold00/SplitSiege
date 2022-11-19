using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class TowerSocketManager : MonoBehaviour
{
    private Socket selectedSocket;
    public Socket SelectedSocket { get => selectedSocket; set => selectedSocket = value; }
    [SerializeField] GameObject buildParticlePrefab;
    [SerializeField] GameObject goldSplashParticlePrefab;
    [SerializeField] GameObject smokeSplashParticlePrefab;

    #region Singleton

    public static TowerSocketManager instance;


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
    private void OnEnable()
    {
        Socket.OnSocketSelected += HandleSocketSelected;
    }

    private void OnDisable()
    {
        Socket.OnSocketSelected -= HandleSocketSelected;
    }

    public void BuildTower(TowerDataSO towerToBuild)
    {
        if (ResourceManager.instance.CheckLegalTranscation(towerToBuild.cost))
        {
            ResourceManager.instance.UpdateResources(towerToBuild.cost * -1);

            GameObject placedTowerObj = SelectedSocket.AddTowerToSocket(towerToBuild);
            SpawnBuildParticlesOnObj(placedTowerObj);

            BuildTowerPopupMenu.instance.HidePopupMenu();
        }

    }

    public void SellTower()
    {
        ResourceManager.instance.UpdateResources(TowerBehavior.CurrentlySelectedTower.GetComponent<TowerBehavior>().GetTowerData().cost / 2);
        TowerStatsPopupMenu.instance.HidePopupMenu();
        TowerBehavior.CurrentlySelectedTower.GetComponent<TowerBehavior>().SocketTowerIsPlacedOn.RemoveTowerFromSocket();
        SpawnSellParticlesOnObj(TowerBehavior.CurrentlySelectedTower);
    }

    public void UpgradeTower()
    {
        TowerStatsPopupMenu.instance.HidePopupMenu();
        GameObject selectedTowerObj = TowerBehavior.CurrentlySelectedTower;
        TowerUpgrader towerUpgrader = selectedTowerObj.GetComponentInParent<TowerUpgrader>();
        GameObject upgradedTowerObj = towerUpgrader.SwitchCurrentTowerWithNextLevelTower();
        SpawnBuildParticlesOnObj(upgradedTowerObj);


    }
    private void HandleSocketSelected(GameObject obj)
    {
        SelectedSocket = obj.GetComponent<Socket>();
        BuildTowerPopupMenu.instance.HidePopupMenu();
        BuildTowerPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
    }

    private void SpawnBuildParticlesOnObj(GameObject obj)
    {
        ITowerBuilder build = obj.GetComponentInChildren<ITowerBuilder>();
        GameObject particleObj = Instantiate(buildParticlePrefab, obj.transform.position, buildParticlePrefab.transform.rotation);
        BuildParticleController buildParticle = particleObj.GetComponent<BuildParticleController>();
        buildParticle.Builder = build;
    }

    private void SpawnSellParticlesOnObj(GameObject obj)
    {
        Instantiate(goldSplashParticlePrefab, obj.transform.position, goldSplashParticlePrefab.transform.rotation);
    }
}