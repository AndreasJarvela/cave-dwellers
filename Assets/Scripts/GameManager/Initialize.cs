using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Initialize : MonoBehaviour {


    public const int width = 150;
    public const int height = 150;

    private TileHandler th;

    private Minimap minimap;


    // Use this for initialization
    void Start() {
        th = GetComponent<TileHandler>();
        //minimap = GameObject.Find("UI").GetComponentInChildren<Minimap>();
        bool[,] newMap = InitialiseRandomMap();
        for (int i = 0; i < 3; ++i)
        {
            newMap = DoSimulationStep(newMap);
        }
        
        InitializeMap(newMap);
        PopulateWorkHandlerWithCurrentDwellers();
        StartCoroutine(ScanGraph());
    }

    private IEnumerator ScanGraph()
    {
        yield return new WaitForSeconds(0);
        AstarPath.active.Scan();
    }


    void PopulateWorkHandlerWithCurrentDwellers()
    {
        foreach(GameObject d in GameObject.FindGameObjectsWithTag("Dweller"))
        {
            GetComponent<WorkHandler>().AddDweller(d.GetComponent<Dweller>());
        }


    }

    float chanceToStartAlive = 0.55f;

    public bool[,] InitialiseRandomMap()
    {
        bool[,] map = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (UnityEngine.Random.Range(0f, 1f) < chanceToStartAlive)
                {
                    map[x,y] = true;
                }
            }
        }
        return map;
    }

    int deathLimit = 3;
    // 2 before
    int birthLimit = 2;

    bool[,] DoSimulationStep(bool[,] oldMap)
    {
        bool[,] newMap = new bool[width, height];
        //Loop over each row and column of the map
        for (int x = 0; x < oldMap.GetUpperBound(0); x++)
        {
            for (int y = 0; y < oldMap.GetUpperBound(1); y++)
            {
                int nbs = countAliveNeighbours(oldMap, x, y);
                //The new value is based on our simulation rules
                //First, if a cell is alive but has too few neighbours, kill it.
                if (oldMap[x,y])
                {
                    if (nbs < deathLimit)
                    {
                        newMap[x,y] = false;
                    }
                    else
                    {
                        newMap[x,y] = true;
                    }
                } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born'
                else
                {
                    if (nbs > birthLimit)
                    {
                        newMap[x,y] = true;
                    }
                    else
                    {
                        newMap[x,y] = false;
                    }
                }
            }
        }
        return newMap;
    }

    private int countAliveNeighbours(bool[,] oldMap, int x, int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                if (i == j)
                {

                }
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x > oldMap.GetUpperBound(0) || neighbour_y > oldMap.GetUpperBound(1))
                {
                    count += 1;
                }
                else if (oldMap[neighbour_x, neighbour_y])
                {
                    count += 1;
                }
            }
        }
        return count;
    }

    void InitializeMap(bool[,] mapReference)
    {
        int[,] map = new int[width, height];
        for (int x = -map.GetUpperBound(0) / 2; x < map.GetUpperBound(0) / 2; x++)
        {
            for (int y = -map.GetUpperBound(1) / 2; y < map.GetUpperBound(1) / 2; y++)
            {
                if (mapReference[x + map.GetUpperBound(0) / 2, y + map.GetUpperBound(1) / 2])
                {
                    th.walls.SetTile(new Vector3Int(x, y, 0), th.wallTile);
                }
                else
                {
                    th.floor.SetTile(new Vector3Int(x, y, 0), th.floorTile);
                }
            }
        }

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                th.floor.SetTile(new Vector3Int(i, j, 0), th.floorTile);
                th.walls.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
        
    }


    // Update is called once per frame
    void Update()
    {

    }

}
