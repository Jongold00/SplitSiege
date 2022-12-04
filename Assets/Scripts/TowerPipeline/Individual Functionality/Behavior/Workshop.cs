using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Workshop : OffensiveTower
{
    private List<NavNode> navNodesInRange;
    [SerializeField] GameObject spikeTrap;
    private bool trapPlaced = false;
    private static List<Trap> placedTraps = new List<Trap>();

    public override void Update()
    {
        if (!CanFire || trapPlaced)
        {
            return;
        }

        navNodesInRange = GetAvailableNavNodesInRange();

        if (navNodesInRange.Count == 0)
        {
            return;
        }

        NavNode navNodeToPlaceOn = navNodesInRange[Random.Range(0, navNodesInRange.Count)];
        GameObject placedObj = PlaceObjectAtPosition(spikeTrap, navNodeToPlaceOn.gameObject.transform.position);
        Trap trap = placedObj.GetComponentInChildren<Trap>();
        trap.NavNodePlacedOn = navNodeToPlaceOn;
        trap.OnTrapCollision += HandleTrapCollision;
        trap.TimeToBuildTrap = offensiveTowerData.GetFireRate();
        placedTraps.Add(trap);
    }

    private GameObject PlaceObjectAtPosition(GameObject obj, Vector3 pos)
    {
        GameObject placedObj = Instantiate(obj, pos, obj.transform.rotation);
        trapPlaced = true;
        return placedObj;
    }

    private List<NavNode> GetAvailableNavNodesInRange()
    {
        List<NavNode> nodes = new List<NavNode>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, offensiveTowerData.range);
        foreach (Collider curr in hitColliders)
        {
            if (curr.TryGetComponent(out NavNode currentNavNode))
            {
                if (placedTraps.Any(x => x.NavNodePlacedOn == currentNavNode))
                {
                    // This node already has a trap placed on it, shouldn't be added to list of available nodes
                    continue;
                }
                nodes.Add(currentNavNode);
            }
        }

        return nodes;
    }

    private void HandleTrapCollision(GameObject obj, Trap trap)
    {
        UnitBehavior unitBehavior = obj.GetComponent<UnitBehavior>();
        unitBehavior.TakeDamage(offensiveTowerData.GetDamage());
        unitBehavior.AttachStatusEffect(new Stun(2));
        trap.Explode();
    }
}
