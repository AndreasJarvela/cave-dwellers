using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningTool : ITool {

    public Tilemap walls;
    public Tilemap floor;
    public Tilemap toolEffects;
    public TileBase miningEffect;
    public TileBase wall;
    public TileBase floorTile;

    public Grid grid;

    public MiningTool(TileBase miningEffect, TileBase wall, TileBase floorTile)
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        this.toolEffects = GameObject.Find("ToolEffects").GetComponent<Tilemap>();
        this.floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        this.miningEffect = miningEffect;
        this.wall = wall;
        this.floorTile = floorTile;
    }
    
    public void PlaceMarker(Vector3Int cellPosition)
    {
        // TODO
        // Create a MineTask on cellposition

        if (walls.GetTile(cellPosition) == wall && toolEffects.GetTile(cellPosition) != miningEffect)
        {
            GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new TestMineTask(cellPosition));
        }

    }

    public void RemoveMarker(Vector3Int cellPosition)
    {
        /*
        if (toolEffects.GetTile(cellPosition) == miningEffect)
        {
            ITask task = GameObject.Find("GameManager").GetComponent<WorkHandler>().GetTask(cellPosition);
            GameObject.Find("GameManager").GetComponent<WorkHandler>().RemoveTask(task);
        }

    */
    }


    void ITool.Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(pos);
            PlaceMarker(cellPosition);

        }

        if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(pos);
            RemoveMarker(cellPosition);
        }
    }
}
