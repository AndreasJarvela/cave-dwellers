using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningTool : ITool {

    public Tilemap walls;
    public Tilemap toolEffects;

    public TileBase miningEffect;
    public TileBase wall;

    public Grid grid;

    public MiningTool(Tilemap walls, Tilemap toolEffects, TileBase miningEffect, TileBase wall, Grid grid)
    {
        this.walls = walls;
        this.toolEffects = toolEffects;
        this.miningEffect = miningEffect;
        this.wall = wall;
        this.grid = grid;
    }
    
    public void PlaceMarker(Vector3Int cellPosition)
    {
        if (walls.GetTile(cellPosition) == wall)
        {
            toolEffects.SetTile(cellPosition, miningEffect);
        }

    }

    public void RemoveMarker(Vector3Int cellPosition)
    {
        if (toolEffects.GetTile(cellPosition) == miningEffect)
        {
            toolEffects.SetTile(cellPosition, null);
        }

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
