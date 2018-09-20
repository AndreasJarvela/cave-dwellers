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
    private Vector3Int destination;

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
        this.miningEffect = miningEffect;
        this.wall = wallTile;
        this.floorTile = floorTile;

        this.cellPosition = cellPosition;
        this.actionQueue = new Queue<IAction>();
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
        Debug.Log("Displaying!");
        toolEffects.SetTile(cellPosition, miningEffect);
    }

    public void CheckBoarders()
    {
        Vector3Int left = new Vector3Int(cellPosition.x - 1, cellPosition.y, cellPosition.z);
        Vector3Int right = new Vector3Int(cellPosition.x + 1, cellPosition.y, cellPosition.z);
        Vector3Int top = new Vector3Int(cellPosition.x, cellPosition.y + 1, cellPosition.z);
        Vector3Int bottom = new Vector3Int(cellPosition.x, cellPosition.y - 1, cellPosition.z);

        if (floor.GetTile(left) == floorTile)
        {
            destination = left;
            reachable = true;
        }

        if ( floor.GetTile(right) == floorTile )
        {
            destination = right;
            reachable = true;
        }

        if (floor.GetTile(top) == floorTile)
        {
            destination = top;
            reachable = true;
        }

        if (floor.GetTile(bottom) == floorTile)
        {
            Vector3 pos = (Vector3)cellPosition + new Vector3(-0.6f, 0, 0);
            Debug.Log(pos);
            destination = bottom;
            reachable = true;
        }
    }

    private void PopulateActionQueue()
    {
        actionQueue.Enqueue(new MoveAction(cellPosition));
        actionQueue.Enqueue(new WaitAction(1f));

    }

    public void Done()
    {
        walls.SetTile(cellPosition, null);
        floor.SetTile(cellPosition, floorTile);
        toolEffects.SetTile(cellPosition, null);
    }


    public IAction NextAction()
    {
        if(actionQueue.Count > 0)
        {
             return actionQueue.Dequeue();
        }
        Done();
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx => {
            var node1 = AstarPath.active.GetNearest(cellPosition).node;
            node1.Walkable = true;
            var gg = AstarPath.active.data.gridGraph;
            gg.GetNodes(node => gg.CalculateConnections((GridNodeBase)node));
            ctx.QueueFloodFill();
        }));
        return null;
    }

    public Dweller ReleaseDweller()
    {
        throw new System.NotImplementedException();
    }

    public bool checkActivity()
    {
        return reachable;
    }

    public void updateActivity()
    {
        CheckBoarders();
    }
}
