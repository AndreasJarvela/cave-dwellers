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

    private Area selectedArea;

    public Grid grid;
    public TileHandler th;

    public AreaTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.th = GameObject.Find("GameManager").GetComponent<TileHandler>();
        this.selectedArea = Area.SLEEPING;
    }

    private void PlaceMarker(Vector3Int cellPosition)
    {
        switch (selectedArea)
        {
            case Area.SLEEPING:
                if (th.floor.GetTile(cellPosition) == th.floorTile && !th.GetAreaTile(cellPosition))
                {
                    th.area.SetTile(cellPosition, th.sleepingAreaTile);
                    GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new BedTask(cellPosition));
                }
                break;
            default:
                break;
        }

    }

    private void RemoveMarker(Vector3Int cellPosition)
    {
       
    }

    public void SelectArea(Area area)
    {
        this.selectedArea = area;
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
