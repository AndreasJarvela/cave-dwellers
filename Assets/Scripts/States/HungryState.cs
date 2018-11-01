using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HungryState : IBehaviourState
{

    /**
     * 
     * Find food, if none show food bubble
     * Walk to food
     * Take food from object if close enough, show animation
     * eat until food is empty or hunger is satisfied
     * 
     **/

    private Queue<IAction> actions;
    private Dweller dweller;
    private Food foundFood;

    private bool justMoved;


    public HungryState(Dweller dweller)
    {
        this.dweller = dweller;
        this.actions = new Queue<IAction>();
        this.foundFood = null;
        this.justMoved = true;
    }

    public Food LookForFood()
    {
        GameObject[] foodPiles = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject food in foodPiles)
        {
            if (!food.GetComponent<Food>().IsEmpty())
            {
                return food.GetComponent<Food>();
            }
        }
        return null;
    }

    private void populateMoveToFood(Food food)
    {
        actions.Enqueue(new MoveAction(food.GetPosition()));
    }

    private bool HasReachedFood()
    {
        if (foundFood != null)
            return Vector3.Distance(foundFood.GetPosition(), dweller.transform.position) < 0.1f;
        return false;
    }

    private Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * 1f;
        point += dweller.GetComponent<IAstarAI>().position;
        return point;
    }
    public bool PlayAnimation = true;
    public IAction NextAction()
    {
        if (actions.Count > 0)
        {
            return actions.Dequeue();
        }

        if (HasReachedFood())
        {

            dweller.Eat(foundFood.TakeFood());
            if (dweller.GetHunger() == dweller.GetMaxHunger() || foundFood.IsEmpty())
            {
                OnExit();
                return new NewStateAction(new FreeRoamState(dweller));
            }

            if (PlayAnimation)
            {
                dweller.GetComponent<Animator>().SetTrigger("Eat");
                PlayAnimation = false;
            }
            return new WaitAction(1f);
        }

        if (foundFood == null)
        {
            foundFood = LookForFood();
        }

        if (foundFood != null)
        {
            dweller.GetSpeakbubble().ClearBubble();
            populateMoveToFood(foundFood);
            return actions.Dequeue();
        }
        else
        {
            dweller.GetSpeakbubble().DisplayEating();
        }

        justMoved = !justMoved;
        if (justMoved)
        {
            dweller.LoseHealth(5);
            return new WaitAction(1f);
        }
        dweller.LoseHealth(5);
        return new MoveAction(PickRandomPoint());
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        dweller.GetComponent<Animator>().SetTrigger("Idle");
    }
}
