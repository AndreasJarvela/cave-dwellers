using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public interface IAction {
    void Update(Dweller dweller);
    bool Completed();
}
