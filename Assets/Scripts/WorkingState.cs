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
        assignedTask.BeginTask();
    }

    public IAction NextAction()
    {

        return assignedTask.NextAction(dweller);
        
    }

}
