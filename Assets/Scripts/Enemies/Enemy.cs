using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    private IEnemyBehaviourState state;
    private IEnemyAction currentAction;


    void Start () {
		
	}
	

	void Update () {
        if (currentAction == null || currentAction.Completed())
        {
            //CheckVitalNeeds();
            currentAction = state.NextAction();
            if (currentAction == null)
            {
                //SetState(new FreeRoamState(this));
            }
        }

        if (currentAction != null)
        {
            currentAction.Update(this);
        }

        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000f);
    }
}
