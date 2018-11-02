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
        return null;
        /*
        justMoved ? return new WaitAction(Random.Range(1f, 3f)) : return ;
        if (!justMoved)
        {
            justMoved = true;
            return new MoveAction(PickRandomPoint());
        }
        else
        {
            justMoved = false;
            return new WaitAction(Random.Range(1f, 3f));
        }
        */
    }

    public void OnExit()
    {

    }
}
