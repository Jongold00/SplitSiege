using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSocketManager : MonoBehaviour
{
    private Socket selectedSocket;
    public Socket SelectedSocket { get => selectedSocket; set => selectedSocket = value; }


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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        Socket.OnSocketSelected += HandleSocketSelected;
    }

    private void OnDisable()
    {
        Socket.OnSocketSelected -= HandleSocketSelected;
    }


    #endregion Singleton

    public void BuildTower(TowerDataSO towerToBuild)
    {
        if (ResourceManager.instance.CheckLegalTranscation(towerToBuild.cost, towerToBuild.faction))
        {
            SelectedSocket.AddTowerToSocket(towerToBuild);
            BuildTowerPopupMenu.instance.HidePopupMenu(gameObject);
        }
    }

    public void SellTower()
    {
        SelectedSocket.RemoveTowerFromSocket();
        // Maybe use a % of cost for sell value?
        ResourceManager.instance.UpdateResources(selectedSocket.CurrentlyPlacedTower.cost / 2, selectedSocket.CurrentlyPlacedTower.faction);
    }
    private void HandleSocketSelected(GameObject obj)
    {
        BuildTowerPopupMenu.instance.HidePopupMenu(obj);
        BuildTowerPopupMenu.instance.DisplayPopupMenuAtViewportOfObj(obj);
        SelectedSocket = obj.GetComponent<Socket>();
    }
}
