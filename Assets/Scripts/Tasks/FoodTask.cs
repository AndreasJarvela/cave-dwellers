using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTask : Task
{
    public FoodTask(Vector3Int taskPosition) : base(taskPosition)
    {


    }


    public override void BeginTask(Dweller dweller)
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckCriteria()
    {
        throw new System.NotImplementedException();
    }

    public override IAction GetCriteria()
    {
        throw new System.NotImplementedException();
    }

    public override int GetEnergyCost()
    {
        throw new System.NotImplementedException();
    }

    public override IAction Progress()
    {
        throw new System.NotImplementedException();
    }

    public override bool TaskActive()
    {
        throw new System.NotImplementedException();
    }
}
