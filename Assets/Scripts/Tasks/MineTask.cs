using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

class MineTask : Task
{
    private const float TIME_BETWEEN_PROGRESS = 1f;
    private const float VALID_DISTANCE_FROM_TASK = 0.8f;

    private const int REQUIRED_PROGRESS = 30;
    private const int PROGRESS_INCREASE_RATE = 10;

    private const int ENERGY_COST = 5;

    private const int RESOURCE_GAINED = 1;

    private TileHandler tileHandler;

    private Queue<IAction> criteraQueue;
    private Vector3 centerOfTask;
    private Vector3 targetPosition;
    private List<Vector3> targetPositions;

    private bool progressTask;

    private int progress;
    private Dweller dweller;

    private ResourceManager rm;

    public MineTask(Vector3Int taskPosition) : base(taskPosition)
    {
        tileHandler = GameObject.Find("GameManager").GetComponent<TileHandler>();
        rm = GameObject.Find("GameManager").GetComponent<ResourceManager>();
        criteraQueue = new Queue<IAction>();
        targetPositions = new List<Vector3>();
        this.taskPosition = taskPosition;
        this.centerOfTask = taskPosition + new Vector3(0.5f, 0.5f, 0);
        tileHandler.GetToolEffectsTilemap().SetTile(taskPosition, tileHandler.GetMiningEffectsTileBase());
        progressTask = false;
        progress = 0;
        
    }

    public override void BeginTask(Dweller dweller)
    {
        this.dweller = dweller;
        progressTask = false;
        /*
        GraphNode dwellerNode = AstarPath.active.GetNearest(dweller.transform.position, NNConstraint.Default).node;
        GraphNode mineTaskNode = AstarPath.active.GetNearest(targetPosition, NNConstraint.Default).node;

        if (PathUtilities.IsPathPossible(dwellerNode, mineTaskNode))
        {
            Debug.Log("Possible!");
        }
        */
        criteraQueue.Enqueue(new MoveAction(targetPosition));
        criteraQueue.Enqueue(new StopAction());
    }

    public override IAction GetCriteria()
    {
        if (criteraQueue.Count > 0)
        {
            return criteraQueue.Dequeue();
        }
        return null;
    }

    public override bool CheckCriteria()
    {
        return Vector3.Distance(centerOfTask, dweller.transform.position) < VALID_DISTANCE_FROM_TASK;
    }

    public override IAction Progress()
    {
        //dweller.LoseEnergy(GetEnergyCost());

        if (progressTask)
        {
            rm.IncreaseResource(ResourceManager.ResourceType.STONE, RESOURCE_GAINED);
            progress += PROGRESS_INCREASE_RATE;
            if (progress == REQUIRED_PROGRESS)
            {
                taskCompleted = true;
            }
            progressTask = false;
            
            if (taskCompleted)
            {
                UpdatePathfindingForNode();
                Done();
                
                return null;
            }
            return new StopAction();
        }
        else
        {
            progressTask = true;
            return new WaitAction(TIME_BETWEEN_PROGRESS);
        }
    }

    private void UpdatePathfindingForNode()
    {
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx => {
            var node1 = AstarPath.active.GetNearest(centerOfTask).node;
            node1.Walkable = true;
            var gg = AstarPath.active.data.gridGraph;
            gg.GetNodes(node => gg.CalculateConnections((GridNodeBase)node));
            //ctx.QueueFloodFill();
        }));
    }

    public void Done()
    {
        tileHandler.GetWallsTilemap().SetTile(taskPosition, null);
        tileHandler.GetFloorTilemap().SetTile(taskPosition, tileHandler.GetFloorTileBase());
        tileHandler.GetToolEffectsTilemap().SetTile(taskPosition, null);
    }

    public override bool TaskActive()
    {
        Vector3Int left = new Vector3Int(taskPosition.x - 1, taskPosition.y, taskPosition.z);
        Vector3Int right = new Vector3Int(taskPosition.x + 1, taskPosition.y, taskPosition.z);
        Vector3Int top = new Vector3Int(taskPosition.x, taskPosition.y + 1, taskPosition.z);
        Vector3Int bottom = new Vector3Int(taskPosition.x, taskPosition.y - 1, taskPosition.z);

        Tilemap floor = tileHandler.GetFloorTilemap();
        TileBase floorTile = tileHandler.GetFloorTileBase();

        bool active = false;
      
        
        if (floor.GetTile(left) == floorTile)
        {
            Vector3 pos = (Vector3)taskPosition + new Vector3(0, 0.5f, 0);
            targetPosition = pos;
            active = true;
        }

        if (floor.GetTile(right) == floorTile)
        {
            Vector3 pos = (Vector3)taskPosition + new Vector3(1f, 0.5f, 0);
            targetPosition = pos;
            active = true;
        }

        if (floor.GetTile(top) == floorTile)
        {
            Vector3 pos = (Vector3)taskPosition + new Vector3(0.5f, 1f, 0);
            targetPosition = pos;
            active = true;
        }

        if (floor.GetTile(bottom) == floorTile)
        {
            Vector3 pos = (Vector3)taskPosition + new Vector3(0.5f, 0, 0);
            targetPosition = pos;
            active = true;
        }



        return active;
    }

    public override int GetEnergyCost()
    {
        return ENERGY_COST;
    }

    public override Vector3 GetMovePosition()
    {
        return targetPosition;
    }
}
