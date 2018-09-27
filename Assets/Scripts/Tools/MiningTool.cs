﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningTool : ITool

{


    public Grid grid;

    public MiningTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    
    public void PlaceMarker(Vector3Int cellPosition)
    {
        ToolHandler th = GameObject.Find("GameManager").GetComponent<ToolHandler>();
        if (th.walls.GetTile(cellPosition) == th.wall && th.toolEffects.GetTile(cellPosition) != th.miningEffect)
        {
            GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new MineTask(cellPosition));
        }
    }

    public void RemoveMarker(Vector3Int cellPosition)
    {
        GameObject.Find("GameManager").GetComponent<WorkHandler>().RemoveTask(cellPosition);
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