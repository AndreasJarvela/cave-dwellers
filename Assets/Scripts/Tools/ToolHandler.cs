using System.Collections;
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

    AreaTool areaTool;
    // Use this for initialization
    void Start () {
        areaTool = new AreaTool();
        toolList = new List<ITool>();
        toolList.Add(new DefaultTool());
        toolList.Add(new MiningTool());
        toolList.Add(areaTool);
        SetTool(ToolType.DEFAULT);
    }

    public void SetAreaType(AreaTool.Area areaType)
    {
        areaTool.SelectArea(areaType);
    }


    public void SetTool(ToolType type)
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
