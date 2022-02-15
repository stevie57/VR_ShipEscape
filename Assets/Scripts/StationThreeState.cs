using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationThreeState : MonoBehaviour
{
    public abstract void ButtonNorth();
    public abstract void ButtonSouth();
    public abstract void ButtonWest();
    public abstract void ButtonEast();
    public abstract void ButtonConfirm();
    public abstract void StateTransitionIn(StationThreeController Station);
    public abstract void StateTransitionOut();
}
