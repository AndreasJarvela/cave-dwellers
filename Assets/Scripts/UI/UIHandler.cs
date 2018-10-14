using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    /**
     * <summary>
     * Tool buttons
     * </summary>
     */
    public Toggle miningToolButton;
    public Toggle areaToolButton;
    public Toggle defaultToolButton;

    private ToolHandler th;

    void Awake()
    {

    }
	// Use this for initialization
	void Start () {
        th = GetComponent<ToolHandler>();
        miningToolButton.onValueChanged.AddListener(delegate { toolButtonClicked(ToolHandler.ToolType.MINE); });
        areaToolButton.onValueChanged.AddListener(delegate { toolButtonClicked(ToolHandler.ToolType.AREA); });
        defaultToolButton.onValueChanged.AddListener(delegate { toolButtonClicked(ToolHandler.ToolType.DEFAULT); });
    }

    public void toolButtonClicked(ToolHandler.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolHandler.ToolType.DEFAULT:
                th.setTool(ToolHandler.ToolType.DEFAULT);
                break;
            case ToolHandler.ToolType.MINE:
                th.setTool(ToolHandler.ToolType.MINE);
                break;
            case ToolHandler.ToolType.AREA:
                th.setTool(ToolHandler.ToolType.AREA);
                break;

        }
    }
	
    // Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            th.setTool(ToolHandler.ToolType.DEFAULT);
            defaultToolButton.isOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            th.setTool(ToolHandler.ToolType.MINE);
            miningToolButton.isOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            th.setTool(ToolHandler.ToolType.AREA);
            areaToolButton.isOn = true;
        }
    }
}
