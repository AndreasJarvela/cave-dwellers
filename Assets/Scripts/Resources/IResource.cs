using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource {

    void IncreaseResource(int amount);
    bool SpendResource(int toSpend);
    int GetQuantity();
}
