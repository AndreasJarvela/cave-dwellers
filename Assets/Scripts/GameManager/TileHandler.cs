using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour {

    public Tilemap toolEffects;
    public Tilemap walls;
    public Tilemap floor;
    public Tilemap Structure;


    public TileBase miningEffect;
    public TileBase wallTile;
    public TileBase floorTile;
    public TileBase bedTile;
    public TileBase foodTile;


    public Tilemap GetToolEffectsTilemap() { return toolEffects; }
    public Tilemap GetWallsTilemap() { return walls; }
    public Tilemap GetFloorTilemap() { return floor; }
    public Tilemap GetStructureTilemap() { return Structure; }


    public TileBase GetMiningEffectsTileBase() { return miningEffect; }
    public TileBase GetWallTileBase() { return wallTile; }
    public TileBase GetFloorTileBase() { return floorTile; }


    public bool HasStructureTile(Vector3Int cellPosition)
    {
        return Structure.GetTile(cellPosition) == bedTile || Structure.GetTile(cellPosition) == foodTile;
    }
}
