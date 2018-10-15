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

    public GameObject areaToggleGroup;
    public Toggle sleepingAreaButton;
    public Toggle foodAreaButton;

    private ToolHandler th;

    

    void Awake()
    {

    }
	// Use this for initialization
	void Start () {
        th = GetComponent<ToolHandler>();
        areaToggleGroup.SetActive(false);

        miningToolButton.onValueChanged.AddListener(delegate { ToolButtonClicked(ToolHandler.ToolType.MINE); });
        areaToolButton.onValueChanged.AddListener(delegate { ToolButtonClicked(ToolHandler.ToolType.AREA); });
        defaultToolButton.onValueChanged.AddListener(delegate { ToolButtonClicked(ToolHandler.ToolType.DEFAULT); });

        sleepingAreaButton.onValueChanged.AddListener(delegate { AreaButtonClicked(AreaTool.Area.SLEEPING); });
        foodAreaButton.onValueChanged.AddListener(delegate { AreaButtonClicked(AreaTool.Area.FOOD); });
    }

    public void ToolButtonClicked(ToolHandler.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolHandler.ToolType.DEFAULT:
                areaToggleGroup.SetActive(false);
                th.SetTool(ToolHandler.ToolType.DEFAULT);
                break;
            case ToolHandler.ToolType.MINE:
                areaToggleGroup.SetActive(false);
                th.SetTool(ToolHandler.ToolType.MINE);
                break;
            case ToolHandler.ToolType.AREA:
                areaToggleGroup.SetActive(true);
                th.SetTool(ToolHandler.ToolType.AREA);
                break;

        }
    }

    public void AreaButtonClicked(AreaTool.Area areaType)
    {
        switch (areaType)
        {
            case AreaTool.Area.SLEEPING:
                th.SetAreaType(AreaTool.Area.SLEEPING);
                break;
            case AreaTool.Area.FOOD:
                th.SetAreaType(AreaTool.Area.FOOD);
                break;
        }
    }
	
    // Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            th.SetTool(ToolHandler.ToolType.DEFAULT);
            defaultToolButton.isOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            th.SetTool(ToolHandler.ToolType.MINE);
            miningToolButton.isOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            th.SetTool(ToolHandler.ToolType.AREA);
            areaToolButton.isOn = true;
        }
    }
}
