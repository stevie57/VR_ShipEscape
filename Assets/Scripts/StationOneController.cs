using System.Collections;
using UnityEngine;
using Autohand;

public class StationOneController : MonoBehaviour
{ 
    [SerializeField]
    private StationOneState _currentState;
    [SerializeField]
    private PhysicsGadgetConfigurableLimitReader _slider;
    [SerializeField]
    private StationOneBattleState _battleState;
    [SerializeField]
    private StationOnePuzzleState _puzzleState;
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

    private void SetState( StationOneState state)
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

    public void SliderUp()
    {
        _currentState.SliderUp();
    }

    public void SliderDown()
    {
        _currentState.SliderDown();
    }

    public float GetSliderValue()
    {
        return _slider.GetValue();
    }
}
