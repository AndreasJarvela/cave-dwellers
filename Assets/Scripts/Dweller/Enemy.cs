using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUnit
{
    public int health;
    private IBehaviourState state;
    private IAction currentAction;

    public Enemy()
    {

    }

    public Animator GetAnimator()
    {
        throw new System.NotImplementedException();
    }

    public IAstarAI GetIAstarAI()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

    public void SetState(IBehaviourState newState)
    {
        this.state = newState;
    }

    public void Update()
    {
        if (currentAction == null || currentAction.Completed())
        {
            currentAction = state.NextAction();
            if (currentAction == null)
            {
                //SetState();
            }
        }

        if (currentAction != null)
        {
            //currentAction.Update();
        }
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000f);
    }



}
