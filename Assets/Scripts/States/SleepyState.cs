using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SleepyState : IBehaviourState
{
    private Dweller dweller;
    private Queue<IAction> actions;
    private Bed foundBed;

    private bool justMoved;


    public SleepyState(Dweller dweller)
    {
        this.dweller = dweller;
        this.actions = new Queue<IAction>();
        this.foundBed = null;
        this.justMoved = true;
    }


    public Bed LookForABed()
    {
        GameObject[] beds = GameObject.FindGameObjectsWithTag("Bed");
        foreach (GameObject bed in beds)
        {
            if (!bed.GetComponent<Bed>().IsOccupied())
            {
                bed.GetComponent<Bed>().Occupy();
                return bed.GetComponent<Bed>();
            }
        }
        return null;
    }

    private void populateMoveToBed(Bed bed)
    {
        actions.Enqueue(new MoveAction(bed.GetPosition()));
    }

    private bool hasReachedBed()
    {
        if(foundBed != null)
            return Vector3.Distance(foundBed.GetPosition(), dweller.transform.position) < 0.1f;
        return false;
    }

    private Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * 1f;
        point += dweller.GetComponent<IAstarAI>().position;
        return point;
    }

    public void OnEnter()
    {

    }

    public IAction NextAction()
    {
        if (actions.Count > 0)
        {
            return actions.Dequeue();
        }

        if (hasReachedBed())
        {
            dweller.GainEnergy(10);
            if (dweller.GetEnergy() == dweller.GetMaxEnergy())
            {
                OnExit();
                return new NewStateAction(new FreeRoamState(dweller));
            }
            return new WaitAction(1f);
        }

        if (foundBed == null)
        {
            foundBed = LookForABed();
        }

        if (foundBed != null)
        {
            dweller.GetSpeakbubble().ClearBubble();
            populateMoveToBed(foundBed);
            return actions.Dequeue();
        }
        else
        {
            dweller.GetSpeakbubble().DisplaySleeping();
        }

        justMoved = !justMoved;
        if (justMoved)
        {
            dweller.LoseHealth(5);
            return new WaitAction(3f);
        }
        dweller.LoseHealth(5);
        return new MoveAction(PickRandomPoint());
    }

    public void OnExit()
    {
        if (foundBed != null)
        {
            foundBed.Free();
        }
    }
}
