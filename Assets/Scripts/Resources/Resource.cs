using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : IResource
{
    private int quantity;
    private int maxQuantity;

    public Resource(int startQuantity, int maxQuantity)
    {
        quantity = startQuantity;
        this.maxQuantity = maxQuantity;
    }

    public void IncreaseResource(int amount)
    {
        if (quantity + amount > maxQuantity)
        {
            quantity = maxQuantity;
        }
        else
        {
            quantity += amount;
        }
        UpdateText();
    }

    public bool SpendResource(int toSpend)
    {
        if (quantity - toSpend < 0)
        {
            return false;
        }

        quantity -= toSpend;
        UpdateText();
        return true;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public abstract void UpdateText();

}
