using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class MiningTool : ITool

{


    public Grid grid;

    

    public MiningTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    
    public void PlaceMarker(Vector3Int cellPosition)
    {
        TileHandler th = GameObject.Find("GameManager").GetComponent<TileHandler>();
        if (th.walls.GetTile(cellPosition) == th.wallTile && th.toolEffects.GetTile(cellPosition) != th.miningEffect)
        {
            GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new MineTask(cellPosition));
        }
    }

    public void RemoveMarker(Vector3Int cellPosition)
    {
        GameObject.Find("GameManager").GetComponent<WorkHandler>().RemoveTask(cellPosition);
    }

    public void CleanUp()
    {
      
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(pos);
            PlaceMarker(cellPosition);
        }

        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(pos);
            RemoveMarker(cellPosition);
        }
    }
}
