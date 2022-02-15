using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StationThreeController : MonoBehaviour
{
    [SerializeField]
    private StationThreeState _currentState;
    [SerializeField]
    private StationThreeState _battleState;
    [SerializeField]
    private StationThreeState _puzzleState;
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

    private void SetState(StationThreeState state)
    {
        if (_currentState != null)
            _currentState.StateTransitionOut();

        _currentState = state;
        _currentState.StateTransitionIn(this);
    }

    private void PuzzleState(float shipEnergy)
    {
        if (shipEnergy <= 0)
        {
            Invoke("ActivatePuzzleState", 1.5f);
        }
    }

    private void ActivatePuzzleState()
    {
        SetState(_puzzleState);
    }
    private void BattleState()
    {
        SetState(_battleState);
    }

    public void ButtonNorth()
    {
        _currentState.ButtonNorth();
    }

    public void ButtonEast()
    {
        _currentState.ButtonEast();
    }

    public void ButtonSouth()
    {
        _currentState.ButtonSouth();
    }

    public void ButtonWest()
    {
        _currentState.ButtonWest();
    }

    public void ConfirmButton()
    {
        _currentState.ButtonConfirm();
    }
}
