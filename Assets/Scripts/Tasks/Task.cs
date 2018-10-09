using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : ITask
{

    protected bool taskAssigned;
    protected bool taskCompleted;
    protected Vector3Int taskPosition;

    public Task(Vector3Int taskPosition)
    {
        this.taskPosition = taskPosition;
        taskCompleted = false;
        taskAssigned = false;
    }

    public abstract void BeginTask(Dweller dweller);

    public abstract bool CheckCriteria();

    public abstract IAction GetCriteria();

    public abstract int GetEnergyCost();

    public abstract IAction Progress();

    public abstract bool TaskActive();

    public void SetTaskAssigned(bool assigned)
    {
        this.taskAssigned = assigned;
    }

    public bool TaskAssigned()
    {
        return taskAssigned;
    }

    public bool TaskCompleted()
    {
        return taskCompleted;
    }
}
