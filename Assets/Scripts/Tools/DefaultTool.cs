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

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Dweller")
                {
                    if (selected != null && !ReferenceEquals(hit.collider.gameObject, selected.gameObject))
                    {
                        selected.SetState(new FreeRoamState(selected));
                    }
                    selected = hit.collider.gameObject.GetComponent<Dweller>();
                    GameObject.FindGameObjectWithTag("UIInformation").GetComponent<UIInformationHandler>().ListenToDweller(selected);
                }
            }

        }

        if (Input.GetMouseButtonDown(1) && selected != null)
        {
            ControlledState state = new ControlledState(this, selected);
            state.EnqueueAction(new StopAction());
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            state.EnqueueAction(new MoveAction(mousePos));
            selected.SetState(state);
        }
    }
}
