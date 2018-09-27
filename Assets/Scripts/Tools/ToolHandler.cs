﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolHandler : MonoBehaviour {

    public Tilemap walls;
    public Tilemap toolEffects;
    public TileBase miningEffect;
    public TileBase wall;
    public TileBase floor;

    public Grid grid;

    public enum ToolType
    {
        DEFAULT, MINE, AREA
    }

    List<ITool> toolList;

    ITool activeTool = null;
    // Use this for initialization
    void Start () {
        toolList = new List<ITool>();
        toolList.Add(new DefaultTool());
        toolList.Add(new MiningTool());
        toolList.Add(new AreaTool());
        setTool(ToolType.DEFAULT);
    }


    public void setTool(ToolType type)
    {
        activeTool = toolList[(int)type];
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setTool(ToolType.DEFAULT);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setTool(ToolType.MINE);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setTool(ToolType.AREA);
        }
        if (activeTool != null)
        {
            activeTool.Update();
        }
	}
}