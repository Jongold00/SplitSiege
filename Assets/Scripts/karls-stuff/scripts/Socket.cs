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

    private void OnMouseDown()
    {
        OnSocketSelected?.Invoke(gameObject);
    }

    public void AddTowerToSocket(TowerDataSO objToSpawn)
    {
        if (CurrentlyPlacedTower != null)
        {
            return;
        }

        GameObject obj = Instantiate(objToSpawn.prefab, transform.position, objToSpawn.prefab.transform.rotation);
        Vector3 newPos = obj.transform.position;

        // Offset should be specific to tower
        // newPos.y += 4.5f;

        obj.transform.position = newPos;

        CurrentlyPlacedTower = objToSpawn;

        gameObject.SetActive(false);
    }

    public void RemoveTowerFromSocket()
    {
        Destroy(currentlyPlacedTower.prefab);
    }
}
