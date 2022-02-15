using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StationTwoState : MonoBehaviour
{
    public abstract void StateTransitionIn(StationTwoController Station);
    public abstract void ButtonRight();
    public abstract void ButtonLeft();
    public abstract void ActivateJoyStick();
    public abstract void DeactivateJoyStick();
    public abstract void StateTransitionOut();
}
