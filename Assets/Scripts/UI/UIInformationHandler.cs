using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationHandler : MonoBehaviour {

    private Dweller selected;

    public Text textName;
    public Image selectedSprite;
    public Health health;
    public Slider energySlider;
    public Slider foodSlider;
	// Use this for initialization
	void Start () {
        selected = null;
    }

    public void ListenToDweller(Dweller selected)
    {
        this.selected = selected;
        selectedSprite.sprite = selected.GetComponent<SpriteRenderer>().sprite;
        textName.text = selected.GetName();
        health.DisplayHealth(selected.GetHealthNormalised());
    }
	
	// Update is called once per frame
	void Update () {
        if (selected != null)
        {
            energySlider.value = selected.GetEnergyNormalised();
            foodSlider.value = selected.GetHungerNormalised();
            health.DisplayHealth(selected.GetHealthNormalised());
        }
	}
}
