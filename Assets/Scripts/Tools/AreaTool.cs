using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class AreaTool : ITool
{

    public enum Area
    {
        SLEEPING, FOOD
    }

    private Area selectedArea;

    public Grid grid;
    public TileHandler th;
    private ResourceManager rm;

    public AreaTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.th = GameObject.Find("GameManager").GetComponent<TileHandler>();
        this.selectedArea = Area.SLEEPING;
        this.rm = GameObject.Find("GameManager").GetComponent<ResourceManager>();

    }

    private void PlaceMarker(Vector3Int cellPosition)
    {
        switch (selectedArea)
        {
            case Area.SLEEPING:


                if (th.floor.GetTile(cellPosition) == th.floorTile && !th.HasAreaTile(cellPosition))
                {
                    if (!rm.SpendResource(ResourceManager.ResourceType.STONE, 15))
                    {
                        return;
                    }
                    th.area.SetTile(cellPosition, th.sleepingAreaTile);
                    GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new BedTask(cellPosition));
                }
                break;
            case Area.FOOD:
                if (th.floor.GetTile(cellPosition) == th.floorTile && !th.HasAreaTile(cellPosition))
                {
                    if (!rm.SpendResource(ResourceManager.ResourceType.STONE, 5))
                    {
                        return;
                    }
                    th.area.SetTile(cellPosition, th.foodAreaTile);
                    GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new FoodTask(cellPosition));
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
