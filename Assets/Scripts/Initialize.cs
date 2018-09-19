using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Initialize : MonoBehaviour {


    public const int width = 150;
    public const int height = 150;

    public Tilemap walls;
    public Tilemap floor;

    public TileBase wallTile;
    public TileBase floorTile;


    // Use this for initialization
    void Start() {
        initializeMap();
        StartCoroutine(ScanGraph());
    }

    private IEnumerator ScanGraph()
    {
        yield return new WaitForSeconds(0);
        AstarPath.active.Scan();
    }


    void initializeMap()
    {
        int[,] map = new int[width, height];
        for (int x = -map.GetUpperBound(0) / 2; x < map.GetUpperBound(0) / 2; x++)
        {
            for (int y = -map.GetUpperBound(1) / 2; y < map.GetUpperBound(1) / 2; y++)
            {
                walls.SetTile(new Vector3Int(x, y, 0), wallTile);
            }
        }

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                floor.SetTile(new Vector3Int(i, j, 0), floorTile);
                walls.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
