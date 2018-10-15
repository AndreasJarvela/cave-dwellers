using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructAction : IAction 
{

    private Vector3Int position;

    private bool completed;

    private GameObject structure;

    public ConstructAction(GameObject structure, Vector3Int position)
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
        
        GameObject struc = GameObject.Instantiate(structure, position + new Vector3(0.5f, 0f ,0f), Quaternion.identity);
        struc.GetComponent<IStructure>().SetPosition(position);
        struc.transform.parent = GameObject.Find("Structures").transform;
        if (struc != null)
        {
            completed = true;
        }
    }
}
