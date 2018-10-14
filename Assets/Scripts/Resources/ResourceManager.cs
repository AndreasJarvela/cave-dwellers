using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public enum ResourceType
    {
        STONE
    }

    private StoneResource sr;
	// Use this for initialization
	void Start () {
        sr = new StoneResource(0, 2000);
	}

    public bool SpendResource(ResourceType resourceToSpend, int cost)
    {
        switch (resourceToSpend)
        {
            case ResourceType.STONE:
                return sr.SpendResource(cost);
            
        }
        return false;
    }

    public void IncreaseResource(ResourceType resourceToSpend, int amount)
    {
        switch (resourceToSpend)
        {
            case ResourceType.STONE:
                sr.IncreaseResource(amount);
                break;

        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
