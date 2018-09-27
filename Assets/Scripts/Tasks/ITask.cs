﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public interface ITask
{
    /**
     * <summary>
     * Queues the next actions that this task will return in order for starting criteria to be met.
     * </summary>
     **/
    void BeginTask(Dweller dweller);

    /**
    * <summary>
    * Checks if dweller has met the criteria to begin task.
    * </summary>
    **/
    bool CheckCriteria();

    /**
    * <summary>
    * Returns the next action required by each new dweller that gets assigned to this task
    * I.E. Moving to task location
    * </summary>
    **/
    IAction GetCriteria();

    /**
     * <summary>
     * Given that the task has met it's starting criteria, returns the progress action
     * </summary>
     **/
    IAction Progress();

    /**
     * <summary>
     * Returns true if task can be started or resumed.
     * </summary>
     **/
    bool TaskActive();

    /**
     * <summary>
     *  Returns true if task is assigned.
     * </summary>
     */
    bool TaskAssigned();

    /**
     * <summary>
     *  Set if task is assigned to a IDweller
     * </summary>
     */
    void SetTaskAssigned(bool assigned);

    /**
    * <summary>
    * Returns true if task is completed.
    * </summary>
    **/
    bool TaskCompleted();


}