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
                dweller.AssignTask(task);
            }
        }
       
    }

    private void UpdateCompletedTasks()
    {
        for (int i = activeTasks.Count - 1; i >= 0; --i)
        {
            ITask task = activeTasks[i];
            if (task.TaskCompleted()) activeTasks.RemoveAt(i);
        }
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
