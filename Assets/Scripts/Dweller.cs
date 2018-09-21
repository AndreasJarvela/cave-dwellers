﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Dweller : MonoBehaviour, IDweller {

    private int health;
    private int energy;
    private string dwellerName;

    private IBehaviourState state;
    private IAction currentAction;
    internal Action<Path> pathCallback;

    void Start()
    {
        this.health = 100;
        this.energy = 100;
        this.dwellerName = "Dweller";
        SetState(new FreeRoamState(this));
    }

    public void AssignTask(ITask task)
    {
        SetState(new WorkingState(this, task));
    }

    public void SetState(IBehaviourState newState)
    {
        state = newState;
    }

    public IBehaviourState GetState()
    {
        return state;
    }

    public void ForceAction(IAction forced)
    {
        currentAction = forced;
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


    void Update()
    {
        if (currentAction == null)
        {
            currentAction = state.NextAction();
        }

        currentAction.Update(this);
        if(currentAction.Completed())
        {
            currentAction = state.NextAction();
        }
    }


}