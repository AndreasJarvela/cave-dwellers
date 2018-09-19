using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dweller : MonoBehaviour, IDweller {

    private int health;
    private int energy;
    private string name;

    private IAstarAI ai;
    private IBehaviourState state;
    

    private bool isAssigned = false;


    void Start()
    {
        this.health = 100;
        this.energy = 100;
        this.name = "Dweller";
        ai = GetComponent<IAstarAI>();
        state = new FreeRoamState(ai);
    }

    public void Assign(bool assigned)
    {
        isAssigned = true;
    }

    public int GetHealth()
    {
        return health;
    }
        
    public string GetName()
    {
        return name;
    }

    public int GetEnergy()
    {
        return energy;
    }

    public bool IsAssigned()
    {
        return isAssigned;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetEnergy(int energy)
    {
        this.energy = energy;
    }


    void Update()
    {
        state.Update();
    }
 
}
