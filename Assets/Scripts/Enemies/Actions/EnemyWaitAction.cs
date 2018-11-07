using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitAction : IEnemyAction
{
    private bool completed;
    private bool startAction;

    private float timeToWait;
    private float targetTime;

    public EnemyWaitAction(float timeToWait)
    {
        this.timeToWait = timeToWait;
        this.startAction = true;
    }

    public bool Completed()
    {
        return completed;
    }

    public void Update(Enemy enemy)
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
