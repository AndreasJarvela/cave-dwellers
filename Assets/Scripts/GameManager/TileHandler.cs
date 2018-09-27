using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour {

    public Tilemap toolEffects;
    public Tilemap walls;
    public Tilemap floor;
    public Tilemap area;


    public TileBase miningEffect;
    public TileBase wallTile;
    public TileBase floorTile;
    public TileBase sleepingAreaTile;

    public Tilemap GetToolEffectsTilemap() { return toolEffects; }
    public Tilemap GetWallsTilemap() { return walls; }
    public Tilemap GetFloorTilemap() { return floor; }
    public Tilemap GetAreaTilemap() { return area; }


    public TileBase GetMiningEffectsTileBase() { return miningEffect; }
    public TileBase GetWallTileBase() { return wallTile; }
    public TileBase GetFloorTileBase() { return floorTile; }
    public TileBase GetSleepingAreaTileBase() { return sleepingAreaTile; }


    public void ClearTiles(Vector3Int cellPosition)
    {
        floor.SetTile(cellPosition, null);
        walls.SetTile(cellPosition, null);
        toolEffects.SetTile(cellPosition, null);
    }
}
