using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    // Use this for initialization
    Texture2D texture;
    void Start () {
        texture = new Texture2D(150, 150);
        GetComponent<Image>().material.mainTexture = texture;
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
