using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledState : IBehaviourState
{

    private Queue<IAction> actions;
    private Dweller dweller;

    public ControlledState(DefaultTool tool, Dweller dweller)
    {
        actions = new Queue<IAction>();
        this.dweller = dweller;
    }

    public void EnqueueAction(IAction newAction)
    {
        actions.Enqueue(newAction);
    }


    public IAction NextAction()
    {
        if (actions.Count > 0)
        {
            return actions.Dequeue();
        }

        return new StopAction();
    }
}
