using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {

    private Dweller sleeping;
    private bool occupied;
	// Use this for initialization
	void Start () {
        this.occupied = false;
	}

    public void Sleep(Dweller dweller)
    {
        this.sleeping = dweller;
        Occupy();
    }

    public void Occupy()
    {
        occupied = true;
    }

    public bool IsOccupied()
    {
        return occupied;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
