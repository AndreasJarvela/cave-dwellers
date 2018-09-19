using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourState {

    IAction NextAction();
}
