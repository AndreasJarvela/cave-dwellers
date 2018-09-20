using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public interface ITask
{
    Dweller ReleaseDweller();
    IAction NextAction();
    bool checkActivity();
    void updateActivity();

}
