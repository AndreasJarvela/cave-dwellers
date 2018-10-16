using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IStructure {

    private Vector3 position;

    private const int MAX_FOOD_RESOURCES = 300;
    private const int ONE_BITE = 20;
    private int currentFoodSupply;

    public Sprite lowSupply;
    public Sprite mediumSupply;
    public Sprite highSupply;

    public void SetPosition(Vector3Int position)
    {
        this.position = position + new Vector3(0.5f, 0.5f, 0);
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(this.position.y * 1000f);
    }   

    // Use this for initialization
    void Start () {
        currentFoodSupply = MAX_FOOD_RESOURCES;
    }

    public bool IsEmpty()
    {
        return currentFoodSupply <= 0;
    }

    public int TakeFood()
    {
        currentFoodSupply -= ONE_BITE;
        if (currentFoodSupply <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (currentFoodSupply <= 100)
        {
            GetComponent<SpriteRenderer>().sprite = lowSupply;

        }
        else if (currentFoodSupply <= 200)
        {
            GetComponent<SpriteRenderer>().sprite = mediumSupply;

        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = highSupply;
        }
        
        return ONE_BITE;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public Vector3 GetPosition()
    {
        return position + new Vector3(-0.15f, 0.1f, 0);
    }
}
