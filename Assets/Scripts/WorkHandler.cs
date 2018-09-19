using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkHandler : MonoBehaviour {

    List<Dweller> dwellers = new List<Dweller>();
    List<ITask> tasks = new List<ITask>();


    public void AddTask(ITask newTask)
    {
        tasks.Add(newTask);
    }
	// Use this for initialization
	void Start () {
		
	}


    public void AddDweller(Dweller dweller)
    {
        dwellers.Add(dweller);
    }
	
	// Update is called once per frame
	void Update () {
        // TODO
		// Assign Dweller if there is a task and available dweller
	}
}
