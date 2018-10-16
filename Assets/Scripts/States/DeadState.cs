using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IBehaviourState {

    private Dweller dweller;

    public DeadState(Dweller dweller)
    {
        this.dweller = dweller;
    }

    public IAction NextAction()
    {
        return new StopAction();
    }

    public void OnEnter()
    {
        dweller.GetSpeakbubble().ClearBubble();
    }

    public void OnExit()
    {
        
    }

}
