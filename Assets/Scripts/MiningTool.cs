using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningTool : MonoBehaviour {

    public Tilemap walls;
    public Tilemap toolEffects;

    public TileBase miningEffect;
    public TileBase wall;

    public Grid grid;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {


        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = grid.WorldToCell(pos);

        if (Input.GetMouseButton(0))
        {
            if (walls.GetTile(cellPosition) == wall)
            {
                toolEffects.SetTile(cellPosition, miningEffect);
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (toolEffects.GetTile(cellPosition) == miningEffect)
            {
                toolEffects.SetTile(cellPosition, null);
            }

        }

    }
}
