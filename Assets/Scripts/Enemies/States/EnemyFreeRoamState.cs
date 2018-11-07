using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFreeRoamState : IEnemyBehaviourState
{


    private Enemy enemy;
    private float roamingRadius;
    private bool justMoved;

    public EnemyFreeRoamState(Enemy enemy)
    {
        this.enemy = enemy;
        roamingRadius = 1f;
        justMoved = false;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * roamingRadius;
        point += enemy.GetComponent<IAstarAI>().position;
        return point;
    }

    

    public void OnEnter()
    {

    }

    public IEnemyAction NextAction()
    {
        
        if (!justMoved)
        {
            justMoved = true;
            return new EnemyMoveAction(PickRandomPoint());
        }
        else
        {
            justMoved = false;
            return new EnemyWaitAction(Random.Range(1f, 3f));
        }
        
    }

    public void OnExit()
    {

    }
}
