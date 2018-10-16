using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour{

    public Sprite fullHeart;
    public Sprite halfHeart;

    public Image firstHeart;
    public Image secondHeart;
    public Image thirdHeart;

    public Color alpha;
    public Color visible;

    public void Start()
    {
        Color alphaColor = Color.white;
        alphaColor.a = 0;
        alpha = alphaColor;
        Color visibleColor = Color.white;
        visible = visibleColor;
    }

    public void DisplayHealth (float normalisedHealth) {
        if (normalisedHealth == 0)
        {
            firstHeart.color = alpha;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (normalisedHealth <= 0.20)
        {
            firstHeart.sprite = halfHeart;
            firstHeart.color = visible;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (normalisedHealth <= 0.40)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (normalisedHealth <= 0.60)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.sprite = halfHeart;
            secondHeart.color = visible;
            thirdHeart.color = alpha;
        }
        else if (normalisedHealth <= 0.80)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.sprite = fullHeart;
            secondHeart.color = visible;
            thirdHeart.color = alpha;
        }
        else if (normalisedHealth < 1)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;

            secondHeart.sprite = fullHeart;
            secondHeart.color = visible;

            thirdHeart.sprite = halfHeart;
            thirdHeart.color = visible;

        }
        else
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.sprite = fullHeart;
            secondHeart.color = visible;
            thirdHeart.sprite = fullHeart;
            thirdHeart.color = visible;
        }
    }
}
