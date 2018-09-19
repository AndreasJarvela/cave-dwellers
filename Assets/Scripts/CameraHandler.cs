using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public float ScrollSpeed = 5;
    // Update is called once per frame
    void Update () {

        if (Input.mousePosition.y >= Screen.height * 0.97)
        {
            transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.y <= Screen.height * 0.03)
        {
            transform.Translate(Vector3.down * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width * 0.97)
        {
            transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.x <= Screen.width * 0.03)
        {
            transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed, Space.World);
        }
    }
}
