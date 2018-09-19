using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingState : IBehaviourState
{
    private ITask assignedTask;

    public WorkingState(ITask assignedTask)
    {
        this.assignedTask = assignedTask;
    }

    public IAction NextAction()
    {
        throw new System.NotImplementedException();
    }

}
