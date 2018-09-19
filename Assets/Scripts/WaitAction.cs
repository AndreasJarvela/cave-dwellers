using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaitAction : IAction
{

    private bool completed;
    private bool startAction;

    private float timeToWait;
    private float targetTime;

    public WaitAction(float timeToWait)
    {
        this.timeToWait = timeToWait;
        this.startAction = true;
    }

    public bool Completed()
    {
        return completed;
    }

    public void Update(Dweller dweller)
    {
        if (startAction)
        {
            targetTime = Time.time + timeToWait;
            startAction = false;
        }

        if (Time.time > targetTime)
        {
            completed = true;
        }
    }
}
