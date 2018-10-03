using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Dweller : MonoBehaviour, IDweller {

    private const int STARTING_HEALTH = 100;
    private const int STARTING_ENERGY = 100;
    private const int SLEEP_THRESHOLD = 0;

    private int health;
    private int energy;
    private string dwellerName;

    private IBehaviourState state;
    private IAction currentAction;
    private ITask currentTask;

    private Speakbubble bubble;
    // internal Action<Path> pathCallback;

    void Start()
    {
        this.health = STARTING_HEALTH;
        this.energy = STARTING_ENERGY;
        this.dwellerName = "Dweller";
        this.bubble = GetComponentInChildren<Speakbubble>();
        SetState(new FreeRoamState(this));
    }

    public void AssignTask(ITask task)
    {
        currentTask = task;
        task.SetTaskAssigned(true);
        SetState(new WorkingState(this, task));
    }

    public void SetState(IBehaviourState newState)
    {
        if (state is WorkingState && !currentTask.TaskCompleted())
        {
            currentTask.SetTaskAssigned(false);
        }
        state = newState;
        currentAction = state.NextAction();
    }

    public IBehaviourState GetState()
    {
        return state;
    }

    public int GetHealth()
    {
        return health;
    }
        
    public string GetName()
    {
        return dwellerName;
    }

    public int GetEnergy()
    {
        return energy;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetName(string name)
    {
        this.dwellerName = name;
    }

    public void SetEnergy(int energy)
    {
        this.energy = energy;

    }

    public void LoseEnergy(int energyToLose)
    {
        if (this.energy >= energyToLose)
        {
            SetEnergy(this.energy - energyToLose);
        }
        else if (this.energy < energyToLose)
        {
            SetEnergy(0);
        }
        
        if (GetEnergy() <= SLEEP_THRESHOLD && !(state is SleepyState))
        {
            SetState(new SleepyState(this));
        }
        
    }

    void Update()
    {

        if (currentAction != null)
        {
            currentAction.Update(this);
            if (currentAction.Completed())
            {
                currentAction = state.NextAction();
                if (currentAction == null)
                {
                    SetState(new FreeRoamState(this));
                }
            }
        }
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000f);
    }

    public Speakbubble GetSpeakbubble()
    {
        return bubble;
    }
}
