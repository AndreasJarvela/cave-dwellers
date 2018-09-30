using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructAction : IAction 
{

    private Vector3 position;

    private bool completed;

    private GameObject structure;

    public ConstructAction(GameObject structure, Vector3 position)
    {
        completed = false;
        this.structure = structure;
        this.position = position;
    }

    public bool Completed()
    {
        return completed;

    }

    public void Update(Dweller dweller)
    {
        GameObject bed = GameObject.Instantiate(structure, position, Quaternion.identity);
        if (bed != null)
        {
            completed = true;
        }
    }
}
