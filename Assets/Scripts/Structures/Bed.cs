using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IStructure {

    private bool occupied;
    private Vector3 position;

    public void Occupy()
    {
        occupied = true;
    }

    public void Free()
    {
        occupied = false;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }

    public void SetPosition(Vector3Int position)
    {

        this.position = position  + new Vector3(0.5f, 0.5f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
