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
    [SerializeField] TowerDataSO testingLevel2Ballista;

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
            ITowerBuilder build = placedTowerObj.GetComponentInChildren<ITowerBuilder>();

            GameObject particleObj = Instantiate(buildParticlePrefab, placedTowerObj.transform.position, buildParticlePrefab.transform.rotation);
            BuildParticleController buildParticle = particleObj.GetComponent<BuildParticleController>();

            buildParticle.Builder = build;

            BuildTowerPopupMenu.instance.HidePopupMenu();
        }

    }

    public void SellTower()
    {
        Debug.Log("sell tower");
        ResourceManager.instance.UpdateResources(TowerBehavior.CurrentlySelectedTower.GetComponent<TowerBehavior>().GetTowerData().cost / 2);
        TowerStatsPopupMenu.instance.HidePopupMenu();
        TowerBehavior.CurrentlySelectedTower.GetComponent<TowerBehavior>().SocketTowerIsPlacedOn.RemoveTowerFromSocket();
    }

    public void UpgradeTower()
    {
        Debug.Log("Upgrade tower!");
        TowerStatsPopupMenu.instance.HidePopupMenu();
        TowerUpgrader towerUpgrader = TowerBehavior.CurrentlySelectedTower.GetComponentInParent<TowerUpgrader>();
        Debug.Log(towerUpgrader);
        towerUpgrader.SwitchCurrentTowerWithNextLevelTower();


    }
    private void HandleSocketSelected(GameObject obj)
    {
        SelectedSocket = obj.GetComponent<Socket>();
        BuildTowerPopupMenu.instance.HidePopupMenu();
        BuildTowerPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
    }
}