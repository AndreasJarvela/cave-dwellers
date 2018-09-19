using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MineTask : ITask
{
    private Queue<IAction> actionQueue;
    private Vector3Int cellPosition;

    private Vector3 targetPosition;

    private bool reachable;

    public Tilemap walls;
    public Tilemap floor;
    public Tilemap toolEffects;

    public TileBase miningEffect;
    public TileBase wall;
    public TileBase floorTile;

    public MineTask(TileBase miningEffect, TileBase wallTile, TileBase floorTile, Vector3Int cellPosition)
    {
        reachable = false;
        this.walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        this.toolEffects = GameObject.Find("ToolEffects").GetComponent<Tilemap>();
        this.floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        this.cellPosition = cellPosition;
        // TODO
        // Check activity
        // setActive
        // getAssigned
        // List Actions
        // Move -> (CheckRange) -> Mine (animation) -> (Lose Durability) -> Release -> (Update Tile) -> (Scan graph) Preferably optimized
        CheckBoarders();
        DisplayEffector();
        PopulateActionQueue();
    }

    private void DisplayEffector()
    {
        toolEffects.SetTile(cellPosition, miningEffect);
    }

    public void CheckBoarders()
    {

        for (int i = cellPosition.x - 1; i < cellPosition.x + 2; i++)
        {
            for (int j = cellPosition.y - 1; j < cellPosition.y + 2; j++)
            {
                
                if (floor.GetTile(new Vector3Int(i, j, 0)) == floorTile)
                {
                    reachable = true;
                }
                
            }
        }
    }

    private void PopulateActionQueue()
    {
        actionQueue.Enqueue(new MoveAction(cellPosition));
        actionQueue.Enqueue(new WaitAction(10f));
    }

    public void Done()
    {
        walls.SetTile(cellPosition, null);
        floor.SetTile(cellPosition, floorTile);
        toolEffects.SetTile(cellPosition, null);
        
    }

    public IAction NextAction()
    {
        return actionQueue.Dequeue();
    }

    public Dweller ReleaseDweller()
    {
        throw new System.NotImplementedException();
    }
}
