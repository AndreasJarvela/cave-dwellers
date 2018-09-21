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

        if (Input.mousePosition.y >= Screen.height * 0.97 || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.y <= Screen.height * 0.03 || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width * 0.97 || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if (Input.mousePosition.x <= Screen.width * 0.03 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed, Space.World);
        }
    }
}
