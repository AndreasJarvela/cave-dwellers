using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public interface ITask
{
    IAction NextAction();
    bool CheckActivity();
    void UpdateActivity();
    Vector3Int GetTaskPosition();
    void CleanUp();
}
