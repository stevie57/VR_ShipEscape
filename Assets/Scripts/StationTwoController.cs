using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.UI;

public class StationTwoController : MonoBehaviour
{
    [SerializeField]
    private StationTwoState _currentState;
    [SerializeField]
    private StationTwoState _battleState;
    [SerializeField]
    private StationTwoState _puzzleState;
    [SerializeField]
    private PhysicsGadgetJoystick _joystick;
    public ShipStatsSO ShipStats;

    private void OnEnable()
    {
        ShipStats.OnValueDecreased += PuzzleState;
        ShipStats.FullEnergy += BattleState;
    }

    private void OnDisable()
    {
        ShipStats.OnValueDecreased -= PuzzleState;
        ShipStats.FullEnergy -= BattleState; 
    }


    private void Start()
    {
        SetState(_battleState);
    }

    private void SetState(StationTwoState newState)
    {
        if (_currentState != null) _currentState.StateTransitionOut();

        _currentState = newState;
        _currentState.StateTransitionIn(this);
    }
    private void PuzzleState(float shipEnergy)
    {
        if (shipEnergy <= 0)
        {
            Invoke("ActivatePuzzleState", 1.5f);
        }
    }

    private void BattleState()
    {
        SetState(_battleState);
    }

    private void ActivatePuzzleState()
    {
        SetState(_puzzleState);
    }

    public void ActivateJoyStick()
    {
        _currentState.ActivateJoyStick();
    }

    public void DeactivateJoyStick()
    {
        _currentState.DeactivateJoyStick();
    }

    public void ButtonRight()
    {
        _currentState.ButtonRight();
    }

    public void ButtonLeft()
    {
        _currentState.ButtonLeft();
    }

    public Vector2 GetJoyStickValue()
    {
        return _joystick.GetValue();
    }
}
