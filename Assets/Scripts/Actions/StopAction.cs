using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StopAction : IAction
{
    private bool done;

    public StopAction()
    {
        done = false;
    }

    public bool Completed()
    {
        return done;
    }

    public void Update(Dweller dweller)
    {
        IAstarAI ai = dweller.GetComponent<IAstarAI>();
        ai.isStopped = true;
        done = true;
    }
}
