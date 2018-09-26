using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkingState : IBehaviourState
{
    private ITask assignedTask;
    private Dweller dweller;

    public WorkingState(Dweller dweller, ITask assignedTask)
    {
        this.assignedTask = assignedTask;
        this.dweller = dweller;
        assignedTask.BeginTask(dweller);
    }

    public IAction NextAction()
    {
        IAction action = assignedTask.GetCriteria();
        if (action != null)
        {
            return action;
        }
        
        if (!assignedTask.CheckCriteria())
        {
            return new NewStateAction(new FreeRoamState(dweller));
        }

        return assignedTask.Progress();
        
    }

}
