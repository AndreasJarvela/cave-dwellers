using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class WorkHandler : MonoBehaviour {

    List<Dweller> dwellers = new List<Dweller>();

    Queue<ITask> activeTasks = new Queue<ITask>();
    List<ITask> inactiveTasks = new List<ITask>();

    // Use this for initialization
    void Start()
    {

    }

    public void AddTask(ITask newTask)
    {
        if (newTask.checkActivity())
        {
            activeTasks.Enqueue(newTask);
        }
        else
        {
            inactiveTasks.Add(newTask);
        }
    }

    public void AddDweller(Dweller dweller)
    {
        dwellers.Add(dweller);
    }


    private void AssignDwellerToTask()
    {
        foreach(Dweller d in dwellers)
        {
            if (d.GetState() is FreeRoamState)
            {
                ITask task = activeTasks.Dequeue();
                d.AssignTask(task);
                break;
            }
        }
    }

    public bool IsInactiveTaskAvailable()
    {
        return inactiveTasks.Count > 0;
    }

    public bool IsTaskAvailable()
    {
        return activeTasks.Count > 0;
    }

    public bool IsDwellerAvailable()
    {
        return dwellers.Count > 0;
    }

    // Update is called once per frame
    void Update () {
        if (IsInactiveTaskAvailable())
        {
            UpdateInactiveTasks();
        }

        if (IsTaskAvailable() && IsDwellerAvailable())
        {
            AssignDwellerToTask();
        }
	}

    private void UpdateInactiveTasks()
    {
        foreach (ITask task in inactiveTasks)
        {
            task.updateActivity();
            if (task.checkActivity())
            {
                inactiveTasks.Remove(task);
                activeTasks.Enqueue(task);
            }
        }
    }
}
