using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IStructure {

    private Vector3 position;
    public void SetPosition(Vector3Int position)
    {
        this.position = position + new Vector3(0.5f, 0.5f, 0);
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(this.position.y * 1000f);
    }   

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
