using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMoveAction : IEnemyAction
{
    private bool completed;
    private bool startAction;
    private Vector3 targetPosition;
    private IAstarAI ai;

    public EnemyMoveAction(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        this.startAction = true;
        this.completed = false;
        ai = null;
    }

    public bool Completed()
    {
        return completed;
    }

    public void Update(Enemy enemy)
    {
        if (ai == null)
        {
            ai = enemy.GetComponent<IAstarAI>();
        }

        if (startAction)
        {
            ai.destination = targetPosition;
            ai.SearchPath();
            startAction = false;
        }

        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            completed = true;
        }
    }
}
