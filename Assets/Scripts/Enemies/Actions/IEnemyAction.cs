using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAction
{
    void Update(Enemy enemy);
    bool Completed();

}
