using System.Collections;
using UnityEngine;
using TMPro;

public class StationOneBattleState : StationOneState
{
    private StationOneController _station;
    [SerializeField]
    private Sinewave _solution;
    [SerializeField]
    private Sinewave _playerWave;
    [SerializeField]
    private StationUIController _stationUI;

    [Header("Input Values")]
    [SerializeField]
    private float _amplitudeIncrement = 0.1f;
    [SerializeField]
    private float _frequencyIncrement = 0.1f;
    [SerializeField]
    private float _movementIncrement = 0.1f;

    [SerializeField, Tooltip("Easing player check")]
    float _amplitudeThreshold = 0.03f;
    private bool _isAmplitudeChanging = false;

    [SerializeField]
    private TextMeshProUGUI _textmesh;

    public override void StateTransitionIn(StationOneController StationOne)
    {
        _station = StationOne;
        _stationUI.ActivateBattleUI();
    }

    public override void ButtonNorth()
    {
        FrequencyIncrease();
    }

    public void FrequencyIncrease()
    {
        _playerWave.AdjustFrequency(_frequencyIncrement);
    }

    public override void ButtonSouth()
    {
        FrequencyDecrease();
    }

    public void FrequencyDecrease()
    {
        _playerWave.AdjustFrequency(-_frequencyIncrement);
    }

    public override void ButtonEast()
    {
        MovementDecrease();
    }
    public void MovementDecrease()
    {
        _playerWave.AdjustMovement(-_movementIncrement);
    }

    public override void ButtonWest()
    {
        MovementIncrease();
    }

    public void MovementIncrease()
    {
        _playerWave.AdjustMovement(_movementIncrement);
    }

    public override void SliderUp()
    {
        StartAmplitudeChange();
    }
    private void StartAmplitudeChange()
    {
        _isAmplitudeChanging = true;
        StartCoroutine(AmplitudeChangeRoutine());
    }

    private IEnumerator AmplitudeChangeRoutine()
    {
        while (_isAmplitudeChanging)
        {
            float value =  _station.GetSliderValue();
            float amplitudeChange = (value * _amplitudeIncrement);
            _playerWave.AdjustAmplitude(amplitudeChange);
            yield return null;
        }
    }

    public override void SliderDown()
    {
        StopAmplitudeChange();
    }

    private void StopAmplitudeChange()
    {
        _isAmplitudeChanging = false;
        StopCoroutine(AmplitudeChangeRoutine());
    }

    public override void ButtonConfirm()
    {
        CheckSettings();
    }

    [ContextMenu("Check Settings")]
    public void CheckSettings()
    {
        // in case I want to change this to be a solution check later
        bool isCorrect = true;
        float[] solutionSettings = _solution.GetValues();
        float[] playerSettings = _playerWave.GetValues();

        float playerAmplitude = playerSettings[0];

        if (playerAmplitude > solutionSettings[0] + _amplitudeThreshold ||
            playerAmplitude < solutionSettings[0] - _amplitudeThreshold)
        {
            _textmesh.text = "Amplitude is incorrect";
            return;
        }

        for (int i = 1; i < solutionSettings.Length; i++)
        {
            if (playerSettings[i] != solutionSettings[i])
            {
                isCorrect = false;
                _textmesh.text = $"Solution isn't correct at {playerSettings[i]}";
                return;
            }
        }
        _textmesh.text = "Solution is correct";
    }

    public override void StateTransitionOut()
    {
        StopAllCoroutines();
    }
}
