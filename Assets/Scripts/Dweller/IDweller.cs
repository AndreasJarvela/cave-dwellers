using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDweller {

    void SetEnergy(int energy);
    int GetEnergy();

    void SetHealth(int health);
    int GetHealth();

    void SetName(string name);
    string GetName();

    void SetState(IBehaviourState newState);
    IBehaviourState GetState();

    Speakbubble GetSpeakbubble();
    
}
