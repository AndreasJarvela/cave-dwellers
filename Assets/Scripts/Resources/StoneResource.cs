using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneResource : Resource {

    private Text stoneResourceText;


    public StoneResource(int startQuantity, int maxQuantity) : base(startQuantity, maxQuantity)
    {
        stoneResourceText = GameObject.FindGameObjectWithTag("ResourceText").GetComponent<Text>();
        UpdateText();
    }

    public override void UpdateText()
    {
        stoneResourceText.text = GetQuantity().ToString();
    }
}
