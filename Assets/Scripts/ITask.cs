using System.Collections;
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
    void BeginTask();

    /**
    * <summary>
    * Checks if dweller has met the criteria to begin task.
    * </summary>
    **/
    bool CheckCriteria(Dweller dweller);

    /**
     * <summary>
     * Given that the task has met it's starting criteria, returns the progress action
     * </summary>
     **/
    IAction NextAction(Dweller dweller);

    /**
     * <summary>
     * Returns true if task can be started or resumed.
     * </summary>
     **/
    bool TaskActive();


    /**
    * <summary>
    * Returns true if task is completed.
    * </summary>
    **/
    bool TaskCompleted();

}
