using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Socket : MonoBehaviour
{
    private TowerDataSO currentlyPlacedTower;
    public TowerDataSO CurrentlyPlacedTower { get => currentlyPlacedTower; private set => currentlyPlacedTower = value; }
    public static event Action<GameObject> OnSocketSelected;
    [SerializeField] HoverDetector popupMenuHoverDetector;

    public void OnMouseDown()
    {
        Debug.Log(popupMenuHoverDetector.IsHovering);
        if (!popupMenuHoverDetector.IsHovering)
        {

            OnSocketSelected?.Invoke(gameObject);

        }
    }

    public GameObject AddTowerToSocket(TowerDataSO objToSpawn)
    {
        EventsManager.instance.TowerBuilt(objToSpawn);

        GameObject obj = Instantiate(objToSpawn.prefab, transform.position, objToSpawn.prefab.transform.rotation);
        Vector3 newPos = obj.transform.position;
        obj.transform.position = newPos;

        CurrentlyPlacedTower = objToSpawn;

        gameObject.SetActive(false);

        return obj;
    }

    public void RemoveTowerFromSocket()
    {
        Destroy(currentlyPlacedTower.prefab);
    }
}
