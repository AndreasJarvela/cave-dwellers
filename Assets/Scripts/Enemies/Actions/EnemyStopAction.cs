using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyStopAction : IEnemyAction
{
    private bool completed;

    public EnemyStopAction()
    {
        completed = false;
    }

    public bool Completed()
    {
        return completed;
    }

    public void Update(Enemy enemy)
    {
        IAstarAI ai = enemy.GetComponent<IAstarAI>();
        ai.destination = enemy.transform.position;
        ai.SearchPath();
        completed = true;
    }
}
