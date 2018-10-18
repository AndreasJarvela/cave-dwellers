using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStateAction : IAction {

    private IBehaviourState stateToEnter;
    private bool completed;

    public NewStateAction(IBehaviourState stateToEnter)
    {
        this.stateToEnter = stateToEnter;
        this.completed = false;
    }

    public bool Completed()
    {
        return completed;
    }

    public void Update(Dweller dweller)
    {
        dweller.SetState(stateToEnter);
        completed = true;
    }

}
