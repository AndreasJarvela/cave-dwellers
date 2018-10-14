﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolHandler : MonoBehaviour {

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
        
        if (activeTool != null)
        {
            activeTool.Update();
        }
	}
}
