using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public interface IUnit {

   IAstarAI GetIAstarAI();
   Animator GetAnimator();
   Transform GetTransform();
   void SetState(IBehaviourState newState);
	
}
