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

    StructureTool areaTool;
    // Use this for initialization
    void Start () {
        areaTool = new StructureTool();
        toolList = new List<ITool>();
        toolList.Add(new DefaultTool());
        toolList.Add(new MiningTool());
        toolList.Add(areaTool);
        SetTool(ToolType.DEFAULT);
    }

    public void SetStructureType(StructureTool.Structure structureType)
    {
        areaTool.SelectStructure(structureType);
    }


    public void SetTool(ToolType type)
    {
        if (activeTool != null)
        {
            activeTool.CleanUp();
        }
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
