using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyState : IBehaviourState
{
    private Dweller dweller;

    public SleepyState(Dweller dweller)
    {
        this.dweller = dweller;
        DisplaySleepy();
    }

    public void DisplaySleepy()
    {
        dweller.GetSpeakbubble().DisplaySleeping();
    }

    public IAction NextAction()
    {
        Debug.Log("Stopaction returned!");
        return new StopAction();
        
    }
}
