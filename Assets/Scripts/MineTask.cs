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
    private Vector3 destination;

    public Tilemap walls;
    public Tilemap floor;
    public Tilemap toolEffects;

    public TileBase miningEffect;
    public TileBase wall;
    public TileBase floorTile;

    public MineTask(TileBase miningEffect, TileBase wallTile, TileBase floorTile, Vector3Int cellPosition)
    {
        Debug.Log(cellPosition);
        reachable = false;
        this.walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        this.toolEffects = GameObject.Find("ToolEffects").GetComponent<Tilemap>();
        this.floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        this.miningEffect = miningEffect;
        this.wall = wallTile;
        this.floorTile = floorTile;

        this.cellPosition = cellPosition;
        this.actionQueue = new Queue<IAction>();

        CheckBoarders();
        DisplayEffector();
    }



    private void DisplayEffector()
    {
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
            Vector3 pos = (Vector3)cellPosition + new Vector3(0, 0.5f, 0);
            destination = pos;
            reachable = true;
        }

        if ( floor.GetTile(right) == floorTile )
        {
            Vector3 pos = (Vector3)cellPosition + new Vector3(1f, 0.5f, 0);
            destination = pos;
            reachable = true;
        }

        if (floor.GetTile(top) == floorTile)
        {
            Vector3 pos = (Vector3)cellPosition + new Vector3(0.5f, 1f, 0);
            destination = pos;
            reachable = true;
        }

        if (floor.GetTile(bottom) == floorTile)
        {
            Vector3 pos = (Vector3)cellPosition + new Vector3(0.5f, 0, 0);
            destination = pos;
            reachable = true;
        }

        PopulateActionQueue();
    }

    private void PopulateActionQueue()
    {
        if (reachable)
        {
            actionQueue.Enqueue(new MoveAction(destination));
            actionQueue.Enqueue(new WaitAction(1f));
        }
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
            Vector3 centerNode = cellPosition + new Vector3(0.5f, 0.5f, 0);
            var node1 = AstarPath.active.GetNearest(centerNode).node;
            node1.Walkable = true;
            var gg = AstarPath.active.data.gridGraph;
            gg.GetNodes(node => gg.CalculateConnections((GridNodeBase)node));
            ctx.QueueFloodFill();
        }));
        return null;
    }

    public bool CheckActivity()
    {
        return reachable;
    }

    public void UpdateActivity()
    {
        CheckBoarders();
    }

    public Vector3Int GetTaskPosition()
    {
        return cellPosition;
    }

    public void CleanUp()
    {
        toolEffects.SetTile(cellPosition, null);
    }
}
