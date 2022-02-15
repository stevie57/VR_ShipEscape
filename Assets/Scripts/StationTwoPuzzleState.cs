using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationTwoPuzzleState : StationTwoState
{
    private StationTwoController _station;
    private bool _isJoystickActive;

    [SerializeField]
    private StationUIController _stationUIController;

    [Header("Fill Settings")]
    [SerializeField]
    private int _currentPlayerFill;
    [SerializeField]
    private float _fillIncrement = 0.05f;
    [SerializeField]
    private Image[] _solutionFills = new Image[2];
    [SerializeField]
    private Image[] _playerFills = new Image[2];
    [SerializeField]
    private float _fillAmount1 = 0.01f;
    [SerializeField]
    private float _fillAmount2 = 0.01f;
    [SerializeField]
    private float _threshold = 0.05f;

    public override void StateTransitionIn(StationTwoController Station)
    {
        _station = Station;
        _fillAmount1 = 0.01f;
        _fillAmount2 = 0.01f;
        SetFill(0, _fillAmount1);
        SetFill(1, _fillAmount2);
        _stationUIController.ActivatePuzzleUI();
    }

    private void SetFill(int playerFill, float fillAmount)
    {
        _playerFills[playerFill].fillAmount = fillAmount;
    }

    public override void ActivateJoyStick()
    {
        GrabbedJoystick();
    }

    private void GrabbedJoystick()
    {
        _isJoystickActive = true;
        StartCoroutine(GrabbedJoystickRoutine());
    }

    private IEnumerator GrabbedJoystickRoutine()
    {
        while (_isJoystickActive)
        {
            Image currentFillTarget = _playerFills[_currentPlayerFill];
            Vector2 joystickValue = _station.GetJoyStickValue();
            float newFillamount = currentFillTarget.fillAmount + (joystickValue.y * _fillIncrement);
            newFillamount = Mathf.Clamp(newFillamount, 0, 1);            
            currentFillTarget.fillAmount = newFillamount;
            yield return null;
        }
    }

    [ContextMenu("Increase Current Fill")]
    public void DebugFillIncrease()
    {
        Image currentFillTarget = _playerFills[_currentPlayerFill];
        float newFillamount = currentFillTarget.fillAmount + (.5f * _fillIncrement);
        newFillamount = Mathf.Clamp(newFillamount, 0, 1);
        currentFillTarget.fillAmount = newFillamount;
    }

    [ContextMenu("Decrease Current Fill")]
    public void DebugFillDecrease()
    {
        Image currentFillTarget = _playerFills[_currentPlayerFill];
        float newFillamount = currentFillTarget.fillAmount + -(.5f * _fillIncrement);
        newFillamount = Mathf.Clamp(newFillamount, 0, 1);
        currentFillTarget.fillAmount = newFillamount;
    }

    public override void DeactivateJoyStick()
    {
        ReleaseJoystick();
    }

    private void ReleaseJoystick()
    {
        _isJoystickActive = false;
        StopCoroutine(GrabbedJoystickRoutine());
    }

    [ContextMenu("Button Left")]
    public override void ButtonLeft()
    {
        _currentPlayerFill++;
        if (_currentPlayerFill > _playerFills.Length) _currentPlayerFill = 0;
    }

    [ContextMenu("Button Right")]
    public override void ButtonRight()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
        for(int i = 0; i < _solutionFills.Length; i++)
        {
            float maxFill = _solutionFills[i].fillAmount + _threshold;
            float minFill = _solutionFills[i].fillAmount - _threshold;

            if(_playerFills[i].fillAmount > maxFill || _playerFills[i].fillAmount < minFill)
            {
                print($"Fill amount at {i} is incorrect");
                return;
            }
        }

        _station.ShipStats.IncreaseEnergy(35f);
    }

    public override void StateTransitionOut()
    {

    }
}
