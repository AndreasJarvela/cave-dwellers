using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Dweller : MonoBehaviour, IDweller {

    private const int STARTING_HEALTH = 120;
    private const int MAX_ENERGY = 100;
    private const int MAX_HUNGER = 100;
    private const int SLEEP_THRESHOLD = 1;
    private const int HUNGER_THRESHOLD = 1;

    private int health;
    private int energy;
    private int hunger;
    private string dwellerName;
    private bool dead;

    private IBehaviourState state;
    private IAction currentAction;
    private ITask currentTask;

    private Speakbubble bubble;
    // internal Action<Path> pathCallback;

    void Start()
    {
        this.health = STARTING_HEALTH;
        this.energy = MAX_ENERGY;
        this.hunger = MAX_HUNGER;
        this.dwellerName = "Dweller";
        this.dead = false;
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

    public int GetHunger()
    {
        return hunger;
    }

    public float GetEnergyNormalised()
    {
        return (float)energy / (float)MAX_ENERGY;
    }

    public float GetHungerNormalised()
    {
        return (float)GetHunger() / (float)MAX_HUNGER;
    }

    public float GetHealthNormalised()
    {
        return (float)health / (float)STARTING_HEALTH;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void Eat(int amount)
    {
        if (GetHunger() + amount >= MAX_HUNGER)
        {
            hunger = MAX_HUNGER;
        }
        else
        {
            hunger += amount;
        }
    }

    public void Starve(int amount)
    {
        if (GetHunger() >= amount)
        {
            hunger = hunger - amount;
        }
        else if (GetHunger() < amount)
        {
            hunger = 0;
        }
    }

    public void GainHealth(int healthToGain)
    {
        if (GetHealth() + healthToGain >= STARTING_HEALTH)
        {
            SetHealth(MAX_ENERGY);
        }
        else
        {
            SetHealth(GetHealth() + healthToGain);
        }

    }

    public void LoseHealth(int healthToLose)
    {
        if (GetHealth() >= healthToLose)
        {
            SetHealth(GetHealth() - healthToLose);
        }
        else if (GetHealth() < healthToLose)
        {
            dead = true;
            SetHealth(0);
        }
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

    public int GetMaxHunger()
    {
        return MAX_HUNGER;
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

    public void CheckVitalNeeds()
    {
        if (dead)
        {
            GetComponent<Animator>().SetTrigger("Dead");
            SetState(new DeadState(this));
            return;
        }

        if (GetEnergy() <= SLEEP_THRESHOLD && !(state is SleepyState))
        {
            SetState(new SleepyState(this));
            return;
        }

        if (GetHunger() <= HUNGER_THRESHOLD && !(state is HungryState))
        {
            SetState(new HungryState(this));
            return;
        }
    }

    void Update()
    {
        if (currentAction == null  || currentAction.Completed())
        {
            Starve(1);
            CheckVitalNeeds();
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
