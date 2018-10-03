using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakbubble : MonoBehaviour {

    private SpriteRenderer bubble;

    public Sprite sleepingBubble;
    public Sprite foodBubble;
	// Use this for initialization
	void Start () {
        this.bubble = GetComponent<SpriteRenderer>();
	}

    public void DisplaySleeping()
    {
        bubble.sprite = sleepingBubble;
    }

    public void DisplayEating()
    {
        bubble.sprite = foodBubble;

    }


    public void ClearBubble()
    {
        bubble.sprite = null;
    }
}
