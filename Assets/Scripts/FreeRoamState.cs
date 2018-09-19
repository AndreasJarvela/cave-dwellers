using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FreeRoamState : IBehaviourState {

    private bool timeset = false;
    private bool beingForced = false;
    private float time = 0;
    private float roamingRadius = 2f;

    IAstarAI ai;


    public FreeRoamState(IAstarAI ai)
    {
        this.ai = ai;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * roamingRadius;
        point += ai.position;
        return point;
    }

    public void Update()
    {

        if (!timeset && !ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
            time = Time.time + 5f;
            timeset = true;
        }

        if (timeset && Time.time > time)
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
            timeset = false;
        }

    }


}
