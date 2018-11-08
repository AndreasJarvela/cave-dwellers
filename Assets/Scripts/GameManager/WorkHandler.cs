using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class WorkHandler : MonoBehaviour {

    List<IDweller> dwellers = new List<IDweller>();
    List<ITask> activeTasks = new List<ITask>();

    public void AddTask(ITask newTask)
    {
        activeTasks.Add(newTask);
    }

    public void RemoveTask(Vector3Int cellPosition)
    {
        /*
        for (int i = activeTasks.Count - 1; i >= 0; --i)
        {
            ITask task = activeTasks[i];
            if (ReferenceEquals(taskToRemove, task))
            {
                activeTasks.Clear();
                activeTasks.RemoveAt(i);
            }
        }
        */
    }

    public void AddDweller(IDweller dweller)
    {
        dwellers.Insert(0, dweller);
    }


    private void AssignDwellerToTask()
    {
        IDweller dweller = GetAvailableDweller();
        if(dweller != null)
        {
           UpdateCompletedTasks();
           ITask task = GetAvailableTask();
            if (task != null)
            {
                if (!TaskCanBeReached(task, dweller))
                {
                    MoveFrontTaskToLast();
                    return;
                }
                dweller.SetState(new WorkingState((Dweller)dweller, task));
            }
        }
       
    }

    private bool TaskCanBeReached(ITask task, IDweller dweller)
    {
        Dweller d = (Dweller)dweller;
        GraphNode dwellerNode = AstarPath.active.GetNearest(d.transform.position, NNConstraint.Default).node;
        GraphNode mineTaskNode = AstarPath.active.GetNearest(task.GetMovePosition(), NNConstraint.Default).node;
        return PathUtilities.IsPathPossible(dwellerNode, mineTaskNode);
    }

    private void UpdateCompletedTasks()
    {
        for (int i = activeTasks.Count - 1; i >= 0; --i)
        {
            ITask task = activeTasks[i];
            if (task.TaskCompleted()) activeTasks.RemoveAt(i);
        }
    }

    private void MoveFrontTaskToLast()
    {
        ITask task = activeTasks[0];
        activeTasks.RemoveAt(0);
        activeTasks.Add(task);
    }

    private ITask GetAvailableTask()
   {
        foreach (ITask activeTask in activeTasks)
        {
            if (activeTask.TaskActive() && !activeTask.TaskAssigned())
            {
                return activeTask;
            }
        }
        return null;
   }

    private IDweller GetAvailableDweller()
    {
        foreach (IDweller d in dwellers)
        {
            if (d.GetState() is FreeRoamState)
            {
                return d;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update ()
    {
        AssignDwellerToTask();
	}
}
