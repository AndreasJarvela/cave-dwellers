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

    public void DisplayHealth (float health) {
        if (health == 0)
        {
            firstHeart.color = alpha;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (health <= 0.15)
        {
            firstHeart.sprite = halfHeart;
            firstHeart.color = visible;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (health <= 0.30)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.color = alpha;
            thirdHeart.color = alpha;
        }
        else if (health <= 0.45)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.sprite = halfHeart;
            secondHeart.color = visible;
            thirdHeart.color = alpha;
        }
        else if (health <= 0.60)
        {
            firstHeart.sprite = fullHeart;
            firstHeart.color = visible;
            secondHeart.sprite = fullHeart;
            secondHeart.color = visible;
            thirdHeart.color = alpha;
        }
        else if (health <= 0.75)
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
