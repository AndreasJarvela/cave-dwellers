using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTool : ITool {

    private Dweller selected;

    public DefaultTool()
    {
        selected = null;
    }


    void ITool.Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject dweller in GameObject.FindGameObjectsWithTag("Dweller"))
            {



                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                BoxCollider2D collider = dweller.GetComponent<BoxCollider2D>();
                Vector2 test = new Vector2();
                test.x = pos.x;
                test.y = pos.y;
                if (collider.bounds.Contains(test))
                {
                    selected = dweller.GetComponent<Dweller>();
                    Debug.Log("Works");
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selected != null)
        {
            Debug.Log("Forcing an action!");
            selected.ForceAction(new MoveAction(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }
}
