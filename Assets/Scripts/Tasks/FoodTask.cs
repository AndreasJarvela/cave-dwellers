using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTask : Task
{
    private const float VALID_DISTANCE_FROM_TASK = 0.1f;

    private Vector3 centerOfTask;
    private Queue<IAction> criteraQueue;

    private Dweller dweller;
    private PrefabHandler ph;
    private TileHandler th;

    public FoodTask(Vector3Int taskPosition) : base(taskPosition)
    {
        this.ph = GameObject.Find("GameManager").GetComponent<PrefabHandler>();
        this.th = GameObject.Find("GameManager").GetComponent<TileHandler>();
        this.taskPosition = taskPosition;
        this.centerOfTask = taskPosition + new Vector3(0.5f, 0.5f, 0);
        this.taskCompleted = false;
        this.taskAssigned = false;
        criteraQueue = new Queue<IAction>();
    }


    public override void BeginTask(Dweller dweller)
    {
        this.dweller = dweller;
        criteraQueue.Enqueue(new MoveAction(centerOfTask));
        criteraQueue.Enqueue(new StopAction());
    }

    public override bool CheckCriteria()
    {

        return Vector3.Distance(centerOfTask, dweller.transform.position) < VALID_DISTANCE_FROM_TASK;
    }

    public override IAction GetCriteria()
    {
        if (criteraQueue.Count > 0)
        {
            return criteraQueue.Dequeue();
        }
        return null;
    }

    public override int GetEnergyCost()
    {
        return 0;
    }

    private bool progress = false;
    private bool hasConstructed = false;

    private int progressVal = 0;

    public override IAction Progress()
    {
        if (!progress)
        {
            progress = true;
            return new WaitAction(1f);
        }
        else
        {
            progress = false;
            if (progressVal < 1)
            {
                progressVal += 1;
            }
            if (progressVal == 1)
            {
                if (!hasConstructed)
                {
                    hasConstructed = true;
                    return new ConstructAction(ph.foodPrefab, taskPosition);
                }
                taskCompleted = true;
                th.Structure.SetTile(taskPosition, null);
                return new NewStateAction(new FreeRoamState(dweller));
            }
            return new StopAction();
        }
    }

    public override bool TaskActive()
    {
        return true;
    }

    public override Vector3 GetMovePosition()
    {
        return centerOfTask;
    }
}
