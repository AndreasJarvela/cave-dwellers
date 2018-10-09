using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Dweller : MonoBehaviour, IDweller {

    private const int STARTING_HEALTH = 100;
    private const int MAX_ENERGY = 100;
    private const int SLEEP_THRESHOLD = 1;

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
        this.energy = MAX_ENERGY;
        this.dwellerName = "Dweller";
        this.bubble = GetComponentInChildren<Speakbubble>();
        SetState(new FreeRoamState(this));
    }

    public void SetCurrentTask(ITask task)
    {
        currentTask = task;
    }

    public void SetState(IBehaviourState newState)
    {
        if (state is WorkingState && !currentTask.TaskCompleted())
        {
            currentTask.SetTaskAssigned(false);
        }
        if (state != null)
            state.OnExit();
        state = newState;
        state.OnEnter();
        CancelCurrentAction();
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

    public int GetMaxEnergy()
    {
        return MAX_ENERGY;
    }

    public void GainEnergy(int energyToGain)
    {
        if (GetEnergy() + energyToGain >= MAX_ENERGY)
        {
            SetEnergy(MAX_ENERGY);
        }
        else
        {
            SetEnergy(GetEnergy() + energyToGain);
        }
    }

    public void LoseEnergy(int energyToLose)
    {
        if (GetEnergy() >= energyToLose)
        {
            SetEnergy(GetEnergy() - energyToLose);
        }
        else if (GetEnergy() < energyToLose)
        {
            SetEnergy(0);
        }
            

        
    }

    public void CancelCurrentAction()
    {
        currentAction = null;
    }

    void Update()
    {
        if (currentAction == null  || currentAction.Completed())
        {
            if (GetEnergy() <= SLEEP_THRESHOLD && !(state is SleepyState))
            {
                SetState(new SleepyState(this));
            }
            currentAction = state.NextAction();
            if (currentAction == null)
            {
                SetState(new FreeRoamState(this));
            }
        }

        if (currentAction != null)
        {
            currentAction.Update(this);
        }
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000f);
    }

    public Speakbubble GetSpeakbubble()
    {
        return bubble;
    }
}
