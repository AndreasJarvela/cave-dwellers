using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MenuInitialize : MonoBehaviour
{


    public const int width = 150;
    public const int height = 150;

    private TileHandler th;

    private Minimap minimap;


    // Use this for initialization
    void Start()
    {
        th = GetComponent<TileHandler>();
        //minimap = GameObject.Find("UI").GetComponentInChildren<Minimap>();
        //InitializeMap();
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
        foreach (GameObject d in GameObject.FindGameObjectsWithTag("Dweller"))
        {
            GetComponent<WorkHandler>().AddDweller(d.GetComponent<Dweller>());
        }

    }

    void InitializeMap()
    {
        int[,] map = new int[width, height];
        for (int x = -map.GetUpperBound(0) / 2; x < map.GetUpperBound(0) / 2; x++)
        {
            for (int y = -map.GetUpperBound(1) / 2; y < map.GetUpperBound(1) / 2; y++)
            {
                th.walls.SetTile(new Vector3Int(x, y, 0), th.wallTile);
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
    public void OnStartGame_Click()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void OnExitGame_Click()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
