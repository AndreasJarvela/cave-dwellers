using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTask : ITask
{

    private const float VALID_DISTANCE_FROM_TASK = 0.1f;

    private Vector3Int taskPosition;
    private Vector3 centerOfTask;

    private Queue<IAction> criteraQueue;

    private bool taskAssigned;
    private bool taskCompleted;
    private Dweller dweller;

    private PrefabHandler ph;

    public BedTask(Vector3Int cellPosition)
    {
        this.ph = GameObject.Find("GameManager").GetComponent<PrefabHandler>();
        this.taskPosition = cellPosition;
        this.centerOfTask = cellPosition + new Vector3(0.5f, 0.5f, 0);
        this.taskCompleted = false;
        this.taskAssigned = false;
        criteraQueue = new Queue<IAction>();
    }

    public void BeginTask(Dweller dweller)
    {
        this.dweller = dweller;
        criteraQueue.Enqueue(new MoveAction(centerOfTask));
        criteraQueue.Enqueue(new StopAction());
    }

    public bool CheckCriteria()
    {
        return Vector3.Distance(centerOfTask, dweller.transform.position) < VALID_DISTANCE_FROM_TASK;
    }

    public IAction GetCriteria()
    {
        if (criteraQueue.Count > 0)
        {
            return criteraQueue.Dequeue();
        }
        return null;
    }

    private bool progress = false;
    private bool hasConstructed = false;

    private int progressVal = 0;

    public IAction Progress()
    {
        if (!progress)
        {
            progress = true;
            return new WaitAction(1f);
        }
        else
        {
            progress = false;
            if (progressVal < 2)
            {
                progressVal += 1;
            }
            if (progressVal == 2)
            {
                if (!hasConstructed)
                {
                    hasConstructed = true;
                    return new ConstructAction(ph.bedPrefab, centerOfTask);
                }
                taskCompleted = true;
                return new NewStateAction(new FreeRoamState(dweller));
            }
            return new StopAction();
        }
    }

    public bool TaskActive()
    {
        return true;
    }

    public bool TaskAssigned()
    {
        return taskAssigned;
    }

    public void SetTaskAssigned(bool assigned)
    {
        this.taskAssigned = assigned;
    }

    public bool TaskCompleted()
    {
        return taskCompleted;
    }

    public int GetEnergyCost()
    {
        return 0;
    }
}
