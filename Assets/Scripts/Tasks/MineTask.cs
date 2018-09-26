using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MineTask : ITask
{
    private const float TIME_BETWEEN_PROGRESS = 0.5f;
    private const float VALID_DISTANCE_FROM_TASK = 0.8f;

    private const int REQUIRED_PROGRESS = 20;
    private const int PROGRESS_INCREASE_RATE = 20;

    private TileHandler tileHandler;

    private Queue<IAction> criteraQueue;

    private Vector3Int taskPosition;
    private Vector3 centerOfTask;
    private Vector3 targetPosition;

    private bool progressTask;
    private bool taskCompleted;
    private bool taskAssigned;

    private int progress;
    private Dweller dweller;

    public MineTask(Vector3Int taskPosition)
    {
        tileHandler = GameObject.Find("GameManager").GetComponent<TileHandler>();
        criteraQueue = new Queue<IAction>();
        this.taskPosition = taskPosition;
        this.centerOfTask = taskPosition + new Vector3(0.5f, 0.5f, 0);
        tileHandler.GetToolEffectsTilemap().SetTile(taskPosition, tileHandler.GetMiningEffectsTileBase());
        progressTask = false;
        taskCompleted = false;
        taskAssigned = false;
        progress = 0;
    }

    public void BeginTask(Dweller dweller)
    {
        this.dweller = dweller;
        progressTask = false;
        /*
        GraphNode dwellerNode = AstarPath.active.GetNearest(dweller.transform.position, NNConstraint.Default).node;
        GraphNode mineTaskNode = AstarPath.active.GetNearest(targetPosition, NNConstraint.Default).node;

        if(PathUtilities.IsPathPossible(dwellerNode, mineTaskNode))
        {

        }
        */
        criteraQueue.Enqueue(new MoveAction(targetPosition));
        criteraQueue.Enqueue(new StopAction());
    }

    public IAction GetCriteria()
    {
        if (criteraQueue.Count > 0)
        {
            return criteraQueue.Dequeue();
        }
        return null;
    }

    public bool CheckCriteria()
    {
        return Vector3.Distance(centerOfTask, dweller.transform.position) < VALID_DISTANCE_FROM_TASK;
    }

    IAction ITask.Progress()
    {
        if (progressTask)
        {
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
                return new NewStateAction(new FreeRoamState(dweller));
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
            ctx.QueueFloodFill();
        }));
    }

    public void Done()
    {
        tileHandler.GetWallsTilemap().SetTile(taskPosition, null);
        tileHandler.GetFloorTilemap().SetTile(taskPosition, tileHandler.GetFloorTileBase());
        tileHandler.GetToolEffectsTilemap().SetTile(taskPosition, null);
    }

    public bool TaskActive()
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

    public bool TaskCompleted()
    {
        return taskCompleted;
    }

    public bool TaskAssigned()
    {
        return taskAssigned;
    }

    public void SetTaskAssigned(bool assigned)
    {
        this.taskAssigned = assigned;
    }
}
