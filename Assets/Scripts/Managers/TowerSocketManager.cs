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
    [SerializeField] DisableObjOnBackgroundClick disableObjOnBackgroundClick;

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
            ITowerBuilder build = placedTowerObj.GetComponent<ITowerBuilder>();

            GameObject particleObj = Instantiate(buildParticlePrefab, placedTowerObj.transform.position, buildParticlePrefab.transform.rotation);
            BuildParticleController buildParticle = particleObj.GetComponent<BuildParticleController>();

            buildParticle.Builder = build;

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
        disableObjOnBackgroundClick.SetAllObjsAndThisToInactive();
        BuildTowerPopupMenu.instance.HidePopupMenu();
        BuildTowerPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
        SelectedSocket = obj.GetComponent<Socket>();
    }
}
