using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class WorkHandler : MonoBehaviour {

    List<Dweller> dwellers = new List<Dweller>();

    List<ITask> activeTasks = new List<ITask>();
    List<ITask> inactiveTasks = new List<ITask>();

    // Use this for initialization
    void Start()
    {

    }

    public void AddTask(ITask newTask)
    {
 
        if (newTask.TaskActive())
        {
            activeTasks.Add(newTask);
        }
        else
        {
            inactiveTasks.Add(newTask);
        }
        
    }

    public void AddDweller(Dweller dweller)
    {
        dwellers.Insert(0, dweller);
    }


    private void AssignDwellerToTask()
    {
        foreach(Dweller d in dwellers)
        {
            if (d.GetState() is FreeRoamState)
            {
                ITask task = activeTasks[0];
                d.AssignTask(task);
                activeTasks.RemoveAt(0);
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
        /*
        if (IsInactiveTaskAvailable())
        {
            UpdateInactiveTasks();
        }
        */
        
        if (IsTaskAvailable() && IsDwellerAvailable())
        {
            AssignDwellerToTask();
        }
	}

    private void UpdateInactiveTasks()
    {
        /*
        for (int i = inactiveTasks.Count - 1; i >= 0; i--)
        {
            ITask task = inactiveTasks[i];
            task.UpdateActivity();
            if (task.CheckActivity())
            {
                inactiveTasks.Remove(task);
                activeTasks.Add(task);
            }
        }
        */
    }
}
