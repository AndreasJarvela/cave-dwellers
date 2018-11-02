using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehaviourState
{
    void OnEnter();
    IEnemyAction NextAction();
    void OnExit();
}
