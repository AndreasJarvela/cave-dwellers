using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FreeRoamState : IBehaviourState {

    private Dweller dweller;
    private float roamingRadius;

    public FreeRoamState(Dweller dweller)
    {
        this.dweller = dweller;
        roamingRadius = 1f;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * roamingRadius;
        point += dweller.GetComponent<IAstarAI>().position;
        return point;
    }

    private bool justMoved = false;

    public IAction NextAction()
    {
        if (!justMoved)
        {
            justMoved = true;
            return new MoveAction(PickRandomPoint());
        }
        else
        {
            justMoved = false;
            return new WaitAction(1f);
        }
    }

}
