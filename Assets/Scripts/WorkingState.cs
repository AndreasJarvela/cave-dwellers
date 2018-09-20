using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingState : IBehaviourState
{
    private ITask assignedTask;
    private Dweller dweller;

    public WorkingState(Dweller dweller, ITask assignedTask)
    {
        this.assignedTask = assignedTask;
        this.dweller = dweller;
    }

    public IAction NextAction()
    {
        IAction nextAction = assignedTask.NextAction();
        if (nextAction != null)
        {
            return nextAction;
        }
        return new NewStateAction(new FreeRoamState(dweller));
    }

}
