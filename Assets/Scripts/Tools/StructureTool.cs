using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class StructureTool : ITool
{

    public enum Structure
    {
        BED, FOOD
    }

    private Structure selectedStructure;

    public Grid grid;
    public TileHandler th;
    private ResourceManager rm;

    public StructureTool()
    {
        this.grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.th = GameObject.Find("GameManager").GetComponent<TileHandler>();
        this.selectedStructure = Structure.BED;
        this.rm = GameObject.Find("GameManager").GetComponent<ResourceManager>();

    }

    private void PlaceMarker(Vector3Int cellPosition)
    {
        switch (selectedStructure)
        {
            case Structure.BED:
                if (th.floor.GetTile(cellPosition) == th.floorTile && !th.HasStructureTile(cellPosition))
                {
                    if (!rm.SpendResource(ResourceManager.ResourceType.STONE, 15))
                    {
                        return;
                    }
                    th.Structure.SetTile(cellPosition, th.bedTile);
                    th.toolEffects.SetTile(cellPosition, null);
                    GameObject.Find("GameManager").GetComponent<WorkHandler>().AddTask(new BedTask(cellPosition));
                }
                break;
            case Structure.FOOD:
                if (th.floor.GetTile(cellPosition) == th.floorTile && !th.HasStructureTile(cellPosition))
                {
                    if (!rm.SpendResource(ResourceManager.ResourceType.STONE, 5))
                    {
                        return;
                    }
                    th.Structure.SetTile(cellPosition, th.foodTile);
                    th.toolEffects.SetTile(cellPosition, null);
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

    public void SelectStructure(Structure structure)
    {
        this.selectedStructure = structure;
    }

    Vector3Int lastPosition;

    public void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = grid.WorldToCell(pos);

        if (th.floor.GetTile(cellPosition) == th.floorTile && !th.HasStructureTile(cellPosition))
        {
            switch (selectedStructure)
            {
                case Structure.BED:
                    th.toolEffects.SetTile(cellPosition, th.bedTile);
                    break;
                case Structure.FOOD:
                    th.toolEffects.SetTile(cellPosition, th.foodTile);
                    break;
            }
        }


        if (lastPosition != cellPosition)
        {
            th.toolEffects.SetTile(lastPosition, null);
        }
        lastPosition = cellPosition;

  

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaceMarker(cellPosition);
        }

        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RemoveMarker(cellPosition);
        }
    }

    public void CleanUp()
    {
        th.toolEffects.SetTile(lastPosition, null);
    }
}
