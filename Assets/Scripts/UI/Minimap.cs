using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    // Use this for initialization
    Texture2D texture;

    void Awake()
    {
        texture = new Texture2D(75, 75);
        texture.SetPixel(0, 0, Color.black);
        texture.Apply();
        GetComponent<Image>().material.mainTexture = texture;
    }

    void Start () {
 
    }

    public void DrawPixel(int x, int y, Color c)
    {
        texture.SetPixel(x, y, c);
    }

    public void ApplyChanges()
    {
        texture.Apply();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
