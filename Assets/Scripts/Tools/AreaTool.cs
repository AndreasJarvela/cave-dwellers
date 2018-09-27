using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaTool : ITool
{

    public enum Area
    {
        SLEEPING
    }
    public Grid grid;
    public TileHandler th;

    public AreaTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.th = GameObject.Find("GameManager").GetComponent<TileHandler>();
    }

    private void PlaceMarker(Vector3Int cellPosition)
    {
        th.GetAreaTilemap().SetTile(cellPosition, th.GetSleepingAreaTileBase());
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
