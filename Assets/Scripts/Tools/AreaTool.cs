using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTool : ITool
{

    public enum Mode
    {
        SINGLE
    }
    public Grid grid;

    public AreaTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    private void PlaceMarker(Vector3Int cellPosition)
    {

    }

    private void RemoveMarker(Vector3Int cellPosition)
    {
       
    }

    public void Update()
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
