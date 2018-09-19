using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineTarget : MonoBehaviour {

    public Vector3Int cellposition;
    public TileBase floorTile;
    public Tilemap floor;

    public bool reachable = false;

	// Use this for initialization
	void Start () {
        checkBoarders();
	}

    private void checkBoarders()
    {

        for (int i = cellposition.x - 1; i < cellposition.x + 2; i++)
        {
            for (int j = cellposition.y - 1; j < cellposition.y + 2; j++)
            {
                if (floor.GetTile(new Vector3Int(i, j, 0)) == floorTile)
                {
                    reachable = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
