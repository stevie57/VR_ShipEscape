using System.Collections.Generic;
using UnityEngine;

public abstract class StationOneState : MonoBehaviour
{
    public abstract void ButtonNorth();
    public abstract void ButtonSouth();
    public abstract void ButtonWest();
    public abstract void ButtonEast();
    public abstract void ButtonConfirm();
    public abstract void SliderUp();
    public abstract void SliderDown();
    public abstract void StateTransitionIn(StationOneController StationOne);
    public abstract void StateTransitionOut();
}
