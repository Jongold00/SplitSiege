using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSocketManager : MonoBehaviour
{
    private Socket selectedSocket;
    public Socket SelectedSocket { get => selectedSocket; set => selectedSocket = value; }
    [SerializeField] GameObject buildParticlePrefab;


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
        if (ResourceManager.instance.CheckLegalTranscation(towerToBuild.cost, towerToBuild.faction))
        {
            GameObject placedTowerObj = SelectedSocket.AddTowerToSocket(towerToBuild);
            Build build = placedTowerObj.GetComponent<Build>();

            GameObject particleObj = Instantiate(buildParticlePrefab, placedTowerObj.transform.position, buildParticlePrefab.transform.rotation);
            BuildParticle buildParticle = particleObj.GetComponent<BuildParticle>();

            buildParticle.Build = build;

            BuildTowerPopupMenu.instance.HidePopupMenu();
        }
    }

    public void SellTower()
    {
        SelectedSocket.RemoveTowerFromSocket();
        ResourceManager.instance.UpdateResources(selectedSocket.CurrentlyPlacedTower.cost / 2, selectedSocket.CurrentlyPlacedTower.faction);
    }
    private void HandleSocketSelected(GameObject obj)
    {
        BuildTowerPopupMenu.instance.HidePopupMenu();
        BuildTowerPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
        SelectedSocket = obj.GetComponent<Socket>();
    }
}
