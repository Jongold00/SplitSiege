using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : OffensiveTower
{
    private List<NavNode> navNodesInRange;
    [SerializeField] GameObject spikeTrap;
    private bool trapPlaced = false;

    public override void Update()
    {
        if (!CanFire || trapPlaced)
        {
            return;
        }

        navNodesInRange = GetNavNodesInRange();
        PlaceObjectAtPosition(spikeTrap, navNodesInRange[Random.Range(0, navNodesInRange.Count)].gameObject.transform.position);
    }

    private void PlaceObjectAtPosition(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, obj.transform.rotation);
        trapPlaced = true;
    }

    private List<NavNode> GetNavNodesInRange()
    {
        List<NavNode> nodes = new List<NavNode>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            if (curr.TryGetComponent(out NavNode currentNavNode))
            {
                nodes.Add(currentNavNode);
            }
        }

        return nodes;
    }


}
